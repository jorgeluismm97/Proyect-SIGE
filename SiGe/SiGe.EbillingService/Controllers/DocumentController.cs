﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SiGe.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocumentService _documentService;
        private readonly IDocumentDetailService _documentDetailService;
        private readonly IDocumentElectronicService _documentElectronicService;
        private readonly IDocumentTypeService _documentTypeService;
        private readonly IDocumentTypeBranchOfficeSerieService _documenttypeBranchOfficeSerieService;
        private readonly IMethodPaymentService _methodPaymentService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly ICompanyService _companyService;
        private readonly ICompanyCertificateService _companyCertificateService;

        public DocumentController(IDocumentDetailService documentDetailService, IDocumentElectronicService documentElectronicService, ICompanyCertificateService companyCertificateService,ICompanyService companyService,IDocumentService documentService, IDocumentTypeService documentTypeService, IDocumentTypeBranchOfficeSerieService documentTypeBranchOfficeSerieService, IMethodPaymentService methodPaymentService, IProductService productService, ICustomerService customerService)
        {
            _documentService = documentService;
            _documentDetailService = documentDetailService;
            _documentElectronicService = documentElectronicService;
            _documentTypeService = documentTypeService;
            _documenttypeBranchOfficeSerieService = documentTypeBranchOfficeSerieService;
            _methodPaymentService = methodPaymentService;
            _productService = productService;
            _customerService = customerService;
            _companyService = companyService;
            _companyCertificateService = companyCertificateService;
        }


        public async Task<IActionResult> Index()
        {
            var document = await _documentService.GetByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);

            return View(document);
        }

        // GET: Serie/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var documentTypes = await _documentTypeService.GetAllAsync();
            var series = await _documenttypeBranchOfficeSerieService.GetByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);
            var methodPayment = await _methodPaymentService.GetAllAsync();
            var product = await _productService.GetByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);
            var customer = await _customerService.GetByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);

            var document = new DocumentCreateViewModel
            {
                DocumentTypes = documentTypes,
                Series = series,
                MethodPayments = methodPayment,
                Products = product,
                Customers = customer
            };

            return View(document);
        }



        //POST: Serie/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentCreateViewModel documentCreateViewModel)
        {

            var companyModel = await  _companyService.GetByIdAsync(HttpContext.Session.GetInt32("companyId").Value);
            var companyCertificateModel = await _companyCertificateService.GetByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);
            var serie = await _documenttypeBranchOfficeSerieService.GetByIdAsync(documentCreateViewModel.DocumentTypeBranchOfficeSerieId);

            ParameterUTL.StrRegistroUnicoContribuyente = companyModel.IdentityDocumentNumber;
            ParameterUTL.StrRazonSocial = companyModel.BusinessName;
            foreach (var item in companyCertificateModel)
            {
                ParameterUTL.StrCertificado = item.Value;
                ParameterUTL.StrCertificadoPassword = item.Password;
            }

            
            ParameterUTL.StrUsuarioSol = "MODDATOS";
            ParameterUTL.StrClaveSol = "MODDATOS";
            ParameterUTL.StrCadenaConexionFactura = "https://e-beta.sunat.gob.pe:443/ol-ti-itcpfegem-beta/billService";


            HttpClient ebillingClient = new HttpClient();

            ebillingClient.BaseAddress = new Uri(ParameterUTL.ebillingService);
            ebillingClient.DefaultRequestHeaders.Accept.Clear();
            ebillingClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ElectronicDocument documento = new ElectronicDocument();

            var documentDetail = new List<DocumentDetail>();

            string StrNombreArchivo = "";
            string StrArchivoValorPdf = "";
            string StrArchivoValorXml = "";
            string StrArchivoValorRespuestaZip = "";

            DateTime IssueDate = DateTime.Now;
            string summaryValue = "";
            string responseCode = "";
            string responseMessage = "";
            string numberTicketCDR = "";
            string stringQR = "";
            string fileName = "";


            var IntIdTipoComprobante = Convert.ToInt32(documentCreateViewModel.DocumentTypeId);
            var TipoComprobanteBe = await Task.Run(async () => { return await _documentTypeService.GetByIdAsync(IntIdTipoComprobante); });

            documento.Company.IdentityDocumentNumber = ParameterUTL.StrRegistroUnicoContribuyente;
            documento.Company.BusinessName = ParameterUTL.StrRazonSocial;

            documento.DocumentType = TipoComprobanteBe.Code;

            documento.Customer.Code = documentCreateViewModel.Customer.IdentityDocumentNumber.Length == 0 ? "0" : (documentCreateViewModel.Customer.IdentityDocumentNumber.Length == 11 ? "6" : "1");
            documento.Customer.IdentityDocumentNumber = documentCreateViewModel.Customer.IdentityDocumentNumber.Length == 0 ? "00000000" : documentCreateViewModel.Customer.IdentityDocumentNumber;
            documento.Customer.BusinessName = documentCreateViewModel.Customer.BusinessName.Length == 0 ? "Varios" : documentCreateViewModel.Customer.BusinessName.Trim();
            documento.Customer.Address = documentCreateViewModel.Customer.Address;

            documento.IssueDate = documentCreateViewModel.IssueDate;
            documento.Document = $"{serie.Serie}-{("00000000" + documentCreateViewModel.Number).Substring(("00000000" + documentCreateViewModel.Number).Length - 8, 8)}";
            //documento.RelatedDocument.DocumentType = StrCodigoDocumentoReferencia;
            //documento.RelatedDocument.Document = StrDocumentoReferencia;
            documento.LettersAmount = UtilitarianUTL.ConvertirLetra(documentCreateViewModel.Total.ToString());

            documento.TaxedAmount = Convert.ToDecimal(documentCreateViewModel.SubTotal);

            documento.AllowanceAmount = 0.00M;
            documento.UnaffectedAmount = 0.00M;
            documento.ExoneratedAmount = 0.00M;
            documento.GlobalDiscount = 0.00M;
            documento.GlobalDiscountAmount = 0.00M;

            documento.TaxAmount = Convert.ToDecimal(documentCreateViewModel.Tax);
            documento.Amount = Convert.ToDecimal(documentCreateViewModel.Total);

            documento.Items.Clear();

            int IntContador = 0;
            decimal DecPrecioTotal = 0;
            decimal DecPrecioTotalSinImpuesto = 0;
            decimal DecImpuesto = 0;

            foreach (DocumentCreateViewModelDetail detailDocument in documentCreateViewModel.Details)
            {
                ElectronicDocumentDetail documentoDetalle = new ElectronicDocumentDetail();

                var detail = new DocumentDetail();

                IntContador++;
                documentoDetalle.Correlative = IntContador;
                documentoDetalle.Code = Convert.ToString(detailDocument.Code);
                documentoDetalle.Description = Convert.ToString(detailDocument.Description);
                documentoDetalle.IsExonerate = false;
                documentoDetalle.IsUnaffected = false;
                documentoDetalle.NetSaleValue = 0.00M;
                documentoDetalle.Discount = 0.00M;
                documentoDetalle.DiscountAmount = 0.00M;
                documentoDetalle.Quantity = Convert.ToInt32(detailDocument.Quantity);

                documentoDetalle.UnitPrice = Math.Round(Convert.ToDecimal(detailDocument.UnitPrice), 2);
                DecPrecioTotal = DecPrecioTotal + (documentoDetalle.UnitPrice * documentoDetalle.Quantity);

                documentoDetalle.ReferencePrice = Math.Round(Convert.ToDecimal(detailDocument.UnitPrice) / 1.18M, 2);
                DecPrecioTotalSinImpuesto = DecPrecioTotalSinImpuesto + Math.Round(((Convert.ToDecimal(detailDocument.UnitPrice) / 1.18M) * documentoDetalle.Quantity), 2);

                //documentoDetalle.ImpuestoUnitario = Math.Round(documentoDetalle.UnitPrice - documentoDetalle.ReferencePrice, 2);
                DecImpuesto = DecImpuesto + Math.Round(((Convert.ToDecimal(detailDocument.UnitPrice) - (Convert.ToDecimal(detailDocument.UnitPrice) / 1.18M)) * documentoDetalle.Quantity), 2);

                documentoDetalle.SalesValue = DecPrecioTotalSinImpuesto;
                documentoDetalle.TaxAmount = DecImpuesto;
                //documentoDetalle.PrecioTotal = Math.Round(Convert.ToDecimal(Dgvr.Cells["ColDetPrecioTotal"].Value), 2); ;

                documento.Items.Add(documentoDetalle);

                detail.Code = documentoDetalle.Code;
                detail.Description = documentoDetalle.Description;
                detail.Quantity = documentoDetalle.Quantity;
                detail.Unit = "";
                detail.UnitPrice = documentoDetalle.UnitPrice;

                documentDetail.Add(detail);

                DecPrecioTotal = 0;
                DecPrecioTotalSinImpuesto = 0;
                DecImpuesto = 0;
            }

            string StrCadenaApi = "";
            switch (Convert.ToInt32(documentCreateViewModel.DocumentTypeId))
            {
                case 8:
                    StrCadenaApi = "api/v1/GenerateCreditNote";
                    break;
                default:
                    StrCadenaApi = "api/v1/GenerateInvoice";
                    break;
            }
            //Generando comprobante electrónico . . ./
            var response = await Task.Run(async () =>
            {
                return await ebillingClient.PostAsJsonAsync(StrCadenaApi, documento);
            });

            //Obteniendo comprobante electrónico . . ."
            var respuesta = await Task.Run(async () =>
            {
                return await response.Content.ReadAsAsync<DocumentResponse>();
            });

            var firmadoRequest = new SignedRequest
            {
                StringXmlUnsigned = respuesta.StringXmlUnsigned,
                DigitalCertificate = ParameterUTL.StrCertificado,
                CertificatePassword = UtilitarianUTL.DesencriptarCadena(ParameterUTL.StrCertificadoPassword),
                SingleNode = false
            };

            //Firmando comprobante electrónico . . .
            var jsonFirmado = await Task.Run(async () =>
            {
                return await ebillingClient.PostAsJsonAsync("api/v1/Sign", firmadoRequest);
            });

            //Obteniendo comprobante electrónico firmado . . .
            var firmadoResponse = await Task.Run(async () =>
            {
                return await jsonFirmado.Content.ReadAsAsync<SignedResponse>();
            });


            summaryValue = firmadoResponse.SignatureSummary;
            stringQR = ParameterUTL.StrRegistroUnicoContribuyente + "|" +
                                documento.DocumentType +
                                "|" + serie.Serie +
                                "|" + ("00000000" + documentCreateViewModel.Number).Substring(("00000000" + documentCreateViewModel.Number).Length - 8, 8) +
                                "|" + documento.TaxAmount +
                                "|" + documento.Amount +
                                "|" + documento.IssueDate.ToString("yyyy-MM-dd") +
                                "|" + documento.Customer.Code +
                                "|" + documento.Customer.IdentityDocumentNumber +
                                "|" + summaryValue;
            StrArchivoValorXml = firmadoResponse.StringXmlSigned;

            var enviarDocumentoRequest = new SendDocumentRequest
            {
                Ruc = ParameterUTL.StrRegistroUnicoContribuyente,
                UserSol = ParameterUTL.StrUsuarioSol,
                PasswordSol = ParameterUTL.StrClaveSol,
                EndPointUrl = ParameterUTL.StrCadenaConexionFactura,
                Document = documento.Document,
                DocumentType = documento.DocumentType,
                StringXmlSigned = firmadoResponse.StringXmlSigned
            };

            //Enviando Comprobante Electrónico a la SUNAT . . .
            var jsonEnvioDocumento = await Task.Run(async () =>
            {
                return await ebillingClient.PostAsJsonAsync("api/v1/SendDocument", enviarDocumentoRequest);
            });

            //Obteniendo Respuesta de la SUNAT . . .
            var respuestaEnvio = await Task.Run(async () =>
            {
                return await jsonEnvioDocumento.Content.ReadAsAsync<SendDocumentResponse>();
            });

            var sendDocumentResponse = (SendDocumentResponse)respuestaEnvio;
            StrNombreArchivo = sendDocumentResponse.FileName;

            if (sendDocumentResponse.Success && !string.IsNullOrEmpty(sendDocumentResponse.StringZipCdr))
            {
                StrArchivoValorRespuestaZip = sendDocumentResponse.StringZipCdr;
                IssueDate = sendDocumentResponse.IssueDate;
                responseCode = sendDocumentResponse.CodeResponse;
                responseMessage = sendDocumentResponse.MessageResponse;
                numberTicketCDR = sendDocumentResponse.TicketCdr;
                fileName = sendDocumentResponse.FileName;
            }
            else
            {
                    IssueDate = DateTime.Now;
                    responseCode = string.IsNullOrEmpty(sendDocumentResponse.CodeResponse) ? "-" : sendDocumentResponse.CodeResponse;
                    responseMessage = string.IsNullOrEmpty(sendDocumentResponse.MessageResponse) ? "-" : sendDocumentResponse.MessageResponse;
                    numberTicketCDR = string.IsNullOrEmpty(sendDocumentResponse.TicketCdr) ? "-" : sendDocumentResponse.TicketCdr;
                    fileName = string.IsNullOrEmpty(sendDocumentResponse.FileName) ? "-" : sendDocumentResponse.FileName;
            }

            var documentModel = new DocumentModel();
            var documentElectronicModel = new DocumentElectronicModel();
            var documentDetailModel = new DocumentDetailModel();

            documentModel.DocumentId = 0;
            documentModel.DocumentTypeId = Convert.ToInt32(documentCreateViewModel.DocumentTypeId);
            documentModel.CustomerId = documentCreateViewModel.Customer.CustomerId;
            documentModel.Serie = Convert.ToString(serie.Serie);
            documentModel.Number = Convert.ToInt32(documentCreateViewModel.Number);
            documentModel.Observation = "";
            documentModel.SubTotal = Convert.ToDecimal(documentCreateViewModel.SubTotal);
            documentModel.Tax = Convert.ToDecimal(documentCreateViewModel.Tax);
            documentModel.Total = Convert.ToDecimal(documentCreateViewModel.Total);
            documentModel.IssueDate = documentCreateViewModel.IssueDate;
            documentModel.BranchOfficeId = 1;
            documentModel.MethodPaymentId = Convert.ToInt32(documentCreateViewModel.MethodPaymentId);
            documentModel.UserId = HttpContext.Session.GetInt32("userId").Value;
            documentModel.CreatorUser = "";
            documentModel.UpdaterUser = "";
            documentModel.CreateDate = DateTime.Now;
            documentModel.UpdateDate = DateTime.Now;
            documentModel.Status = true;
            documentModel.Removed = false;

            await Task.Run(async () => {
                await _documentService.AddAsync(documentModel);
            });


            documentElectronicModel.DocumentElectronicId = 0;
            documentElectronicModel.DocumentId = documentModel.DocumentId;
            documentElectronicModel.IssueDate = DateTime.Now;
            documentElectronicModel.SummaryValue = summaryValue;
            documentElectronicModel.ResponseCode = responseCode;
            documentElectronicModel.ResponseMessage = responseMessage;
            documentElectronicModel.NumberTicketCDR = numberTicketCDR;
            documentElectronicModel.StringQR = stringQR;
            documentElectronicModel.FileName = StrNombreArchivo;
            documentElectronicModel.CreatorUser = "";
            documentElectronicModel.UpdaterUser = "";
            documentElectronicModel.CreateDate = DateTime.Now;
            documentElectronicModel.UpdateDate = DateTime.Now;
            documentElectronicModel.Status = true;
            documentElectronicModel.Removed = false;
            
            await Task.Run(async () => {
                await _documentElectronicService.AddAsync(documentElectronicModel);
            });


            if (true)
            {
                foreach (var item in documentCreateViewModel.Details)
                {
                    documentDetailModel.DocumentDetailId = 0;
                    documentDetailModel.DocumentId = documentModel.DocumentId;
                    documentDetailModel.ProductId = Convert.ToInt32(item.ProductId);
                    documentDetailModel.Quantity = Convert.ToInt32(item.Quantity);
                    documentDetailModel.UnitPrice = Convert.ToDecimal(item.UnitPrice);
                    documentDetailModel.CreatorUser = "";
                    documentDetailModel.UpdaterUser = "";
                    documentDetailModel.CreateDate = DateTime.Now;
                    documentDetailModel.UpdateDate = DateTime.Now;
                    documentDetailModel.Status = true;
                    documentDetailModel.Removed = false;

                    await Task.Run(async () => {
                        await _documentDetailService.AddAsync(documentDetailModel);
                    });


                }
            }

            return View(documentCreateViewModel);
        }

        ////POST: Serie/Create
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> AddCustomer(int id)
        {
            var x = await _customerService.GetByIdAsync(id);

            return Json(x);

        }

        ////POST: Serie/Create
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> AddProduct(int id)
        {
            var x = await _productService.GetByIdAsync(id);

            return Json(x);

        }

        ////POST: Serie/Create
        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(DocumentCreateViewModel documentCreateViewModel)
        //{

        //    return View(documentCreateViewModel);
        //}



        // POST: Serie/Create
        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(DocumentCreateViewModel documentCreateViewModel)
        //{
        //    ElectronicDocument documento = new ElectronicDocument();

        //    var documentDetail = new List<DocumentDetail>();

        //    string StrNombreArchivo = "";
        //    string StrArchivoValorPdf = "";
        //    string StrArchivoValorXml = "";
        //    string StrArchivoValorRespuestaZip = "";

        //    DateTime IssueDate = DateTime.Now;
        //    string summaryValue = "";
        //    string responseCode = "";
        //    string responseMessage = "";
        //    string numberTicketCDR = "";
        //    string stringQR = "";
        //    string fileName = "";


        //    var IntIdTipoComprobante = Convert.ToInt32(documentModel.DocumentTypeId);
        //    var TipoComprobanteBe = await Task.Run(async () => { return await TipoComprobanteBs.GetByIdAsync(IntIdTipoComprobante); });

        //    documento.Company.IdentityDocumentNumber = ParameterUTL.StrRegistroUnicoContribuyente;
        //    documento.Company.BusinessName = ParameterUTL.StrRazonSocial;

        //    documento.DocumentType = TipoComprobanteBe.Code;

        //    documento.Customer.Code = ChkComprobanteNumeroDocumentoIdentidad.CheckState == CheckState.Unchecked ? "0" : (TxtComprobanteNumeroDocumentoIdentidad.TextLength == 0 ? "0" : (TxtComprobanteNumeroDocumentoIdentidad.TextLength == 11 ? "6" : "1"));
        //    documento.Customer.IdentityDocumentNumber = TxtComprobanteNumeroDocumentoIdentidad.TextLength == 0 ? "00000000" : TxtComprobanteNumeroDocumentoIdentidad.Text;
        //    documento.Customer.BusinessName = TxtComprobanteRazonSocial.TextLength == 0 ? "Varios" : TxtComprobanteRazonSocial.Text.Trim();
        //    documento.Customer.Address = TxtComprobanteDireccion.Text;

        //    documento.IssueDate = DtpComprobanteFechaEmision.Value;
        //    documento.Document = $"{CmbComprobanteSerie.Text}-{("00000000" + TxtComprobanteNumero.Text).Substring(("00000000" + TxtComprobanteNumero.Text).Length - 8, 8)}";
        //    documento.RelatedDocument.DocumentType = StrCodigoDocumentoReferencia;
        //    documento.RelatedDocument.Document = StrDocumentoReferencia;
        //    documento.LettersAmount = UtilitarianUTL.ConvertirLetra(TxtComprobanteTotal.Text);

        //    documento.TaxedAmount = Convert.ToDecimal(TxtComprobanteSubTotal.Text);

        //    documento.AllowanceAmount = 0.00M;
        //    documento.UnaffectedAmount = 0.00M;
        //    documento.ExoneratedAmount = 0.00M;
        //    documento.GlobalDiscount = 0.00M;
        //    documento.GlobalDiscountAmount = 0.00M;

        //    documento.TaxAmount = Convert.ToDecimal(TxtComprobanteImpuesto.Text);
        //    documento.Amount = Convert.ToDecimal(TxtComprobanteTotal.Text);

        //    documento.Items.Clear();

        //    int IntContador = 0;
        //    decimal DecPrecioTotal = 0;
        //    decimal DecPrecioTotalSinImpuesto = 0;
        //    decimal DecImpuesto = 0;

        //    foreach (DataGridViewRow Dgvr in DgvComprobanteDetalle.Rows)
        //    {
        //        ElectronicDocumentDetail documentoDetalle = new ElectronicDocumentDetail();

        //        var detail = new DocumentDetail();

        //        IntContador++;
        //        documentoDetalle.Correlative = IntContador;
        //        documentoDetalle.Code = Convert.ToString(Dgvr.Cells["ColDetCodigo"].Value);
        //        documentoDetalle.Description = Convert.ToString(Dgvr.Cells["ColDetDescripcion"].Value);
        //        documentoDetalle.IsExonerate = false;
        //        documentoDetalle.IsUnaffected = false;
        //        documentoDetalle.NetSaleValue = 0.00M;
        //        documentoDetalle.Discount = 0.00M;
        //        documentoDetalle.DiscountAmount = 0.00M;
        //        documentoDetalle.Quantity = Convert.ToInt32(Dgvr.Cells["ColDetCantidad"].Value);

        //        documentoDetalle.UnitPrice = Math.Round(Convert.ToDecimal(Dgvr.Cells["ColDetPrecioUnitario"].Value), 2);
        //        DecPrecioTotal = DecPrecioTotal + (documentoDetalle.UnitPrice * documentoDetalle.Quantity);

        //        documentoDetalle.ReferencePrice = Math.Round(Convert.ToDecimal(Dgvr.Cells["ColDetPrecioUnitario"].Value) / 1.18M, 2);
        //        DecPrecioTotalSinImpuesto = DecPrecioTotalSinImpuesto + Math.Round(((Convert.ToDecimal(Dgvr.Cells["ColDetPrecioUnitario"].Value) / 1.18M) * documentoDetalle.Quantity), 2);

        //        //documentoDetalle.ImpuestoUnitario = Math.Round(documentoDetalle.UnitPrice - documentoDetalle.ReferencePrice, 2);
        //        DecImpuesto = DecImpuesto + Math.Round(((Convert.ToDecimal(Dgvr.Cells["ColDetPrecioUnitario"].Value) - (Convert.ToDecimal(Dgvr.Cells["ColDetPrecioUnitario"].Value) / 1.18M)) * documentoDetalle.Quantity), 2);

        //        documentoDetalle.SalesValue = DecPrecioTotalSinImpuesto;
        //        documentoDetalle.TaxAmount = DecImpuesto;
        //        //documentoDetalle.PrecioTotal = Math.Round(Convert.ToDecimal(Dgvr.Cells["ColDetPrecioTotal"].Value), 2); ;

        //        documento.Items.Add(documentoDetalle);

        //        detail.Code = documentoDetalle.Code;
        //        detail.Description = documentoDetalle.Description;
        //        detail.Quantity = documentoDetalle.Quantity;
        //        detail.Unit = "";
        //        detail.UnitPrice = documentoDetalle.UnitPrice;

        //        documentDetail.Add(detail);

        //        DecPrecioTotal = 0;
        //        DecPrecioTotalSinImpuesto = 0;
        //        DecImpuesto = 0;
        //    }

        //    string StrCadenaApi = "";
        //    switch (Convert.ToInt32(CmbComprobanteTipoComprobante.SelectedValue))
        //    {
        //        case 8:
        //            StrCadenaApi = "api/v1/GenerateCreditNote";
        //            break;
        //        default:
        //            StrCadenaApi = "api/v1/GenerateInvoice";
        //            break;
        //    }
        //    //Generando comprobante electrónico . . ./
        //    var response = await Task.Run(async () => {
        //        return await ebillingClient.PostAsJsonAsync(StrCadenaApi, documento);
        //    });

        //    //Obteniendo comprobante electrónico . . ."
        //    var respuesta = await Task.Run(async () => {
        //        return await response.Content.ReadAsAsync<DocumentResponse>();
        //    });

        //    var firmadoRequest = new SignedRequest
        //    {
        //        StringXmlUnsigned = respuesta.StringXmlUnsigned,
        //        DigitalCertificate = ParameterUTL.StrCertificado,
        //        CertificatePassword = UtilitarianUTL.DesencriptarCadena(ParameterUTL.StrCertificadoPassword),
        //        SingleNode = false
        //    };

        //    //Firmando comprobante electrónico . . .
        //    var jsonFirmado = await Task.Run(async () => {
        //        return await ebillingClient.PostAsJsonAsync("api/v1/Sign", firmadoRequest);
        //    });

        //    //Obteniendo comprobante electrónico firmado . . .
        //    var firmadoResponse = await Task.Run(async () => {
        //        return await jsonFirmado.Content.ReadAsAsync<SignedResponse>();
        //    });


        //    summaryValue = firmadoResponse.SignatureSummary;
        //    stringQR = ParameterUTL.StrRegistroUnicoContribuyente + "|" +
        //                        documento.DocumentType +
        //                        "|" + CmbComprobanteSerie.Text +
        //                        "|" + ("00000000" + TxtComprobanteNumero.Text).Substring(("00000000" + TxtComprobanteNumero.Text).Length - 8, 8) +
        //                        "|" + documento.TaxAmount +
        //                        "|" + documento.Amount +
        //                        "|" + documento.IssueDate.ToString("yyyy-MM-dd") +
        //                        "|" + documento.Customer.Code +
        //                        "|" + documento.Customer.IdentityDocumentNumber +
        //                        "|" + summaryValue;
        //    StrArchivoValorXml = firmadoResponse.StringXmlSigned;

        //    var enviarDocumentoRequest = new SendDocumentRequest
        //    {
        //        Ruc = ParameterUTL.StrRegistroUnicoContribuyente,
        //        UserSol = ParameterUTL.StrUsuarioSol,
        //        PasswordSol = ParameterUTL.StrClaveSol,
        //        EndPointUrl = ParameterUTL.StrCadenaConexionFactura,
        //        Document = documento.Document,
        //        DocumentType = documento.DocumentType,
        //        StringXmlSigned = firmadoResponse.StringXmlSigned
        //    };

        //    //Enviando Comprobante Electrónico a la SUNAT . . .
        //    var jsonEnvioDocumento = await Task.Run(async () => {
        //        return await ebillingClient.PostAsJsonAsync("api/v1/SendDocument", enviarDocumentoRequest);
        //    });

        //    //Obteniendo Respuesta de la SUNAT . . .
        //    var respuestaEnvio = await Task.Run(async () => {
        //        return await jsonEnvioDocumento.Content.ReadAsAsync<SendDocumentResponse>();
        //    });

        //    var sendDocumentResponse = (SendDocumentResponse)respuestaEnvio;
        //    StrNombreArchivo = sendDocumentResponse.FileName;

        //    if (sendDocumentResponse.Success && !string.IsNullOrEmpty(sendDocumentResponse.StringZipCdr))
        //    {
        //        StrArchivoValorRespuestaZip = sendDocumentResponse.StringZipCdr;
        //        IssueDate = sendDocumentResponse.IssueDate;
        //        responseCode = sendDocumentResponse.CodeResponse;
        //        responseMessage = sendDocumentResponse.MessageResponse;
        //        numberTicketCDR = sendDocumentResponse.TicketCdr;
        //        fileName = sendDocumentResponse.FileName;
        //    }
        //    else
        //    {

        //        if (UtilitarianUTL.NotifyAlert($"Se ha detectado una incoherencia : {sendDocumentResponse.MessageError} ¿Desea guardar el comprobante?", ParameterUTL.StrNombreModulo))
        //        {
        //            IssueDate = DateTime.Now;
        //            responseCode = string.IsNullOrEmpty(sendDocumentResponse.CodeResponse) ? "-" : sendDocumentResponse.CodeResponse;
        //            responseMessage = string.IsNullOrEmpty(sendDocumentResponse.MessageResponse) ? "-" : sendDocumentResponse.MessageResponse;
        //            numberTicketCDR = string.IsNullOrEmpty(sendDocumentResponse.TicketCdr) ? "-" : sendDocumentResponse.TicketCdr;
        //            fileName = string.IsNullOrEmpty(sendDocumentResponse.FileName) ? "-" : sendDocumentResponse.FileName;
        //        }
        //    }
        //}
    }
}
