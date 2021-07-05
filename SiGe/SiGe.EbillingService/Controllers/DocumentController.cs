using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Runtime.InteropServices.ComTypes;
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
        private readonly INoteService _noteService;
        private readonly INoteDetailService _noteDetailService;
        private readonly INoteDetailDocumentDetailService _noteDetailDocumentDetailService;
        private readonly IIdentityDocumentTypeService _identityDocumentTypeService;
        private readonly IBranchOfficeService _branchOfficeService;

        public DocumentController(IBranchOfficeService branchOfficeService, IIdentityDocumentTypeService identityDocumentTypeService,INoteDetailDocumentDetailService noteDetailDocumentDetailService, INoteDetailService noteDetailService,INoteService noteService,IDocumentDetailService documentDetailService, IDocumentElectronicService documentElectronicService, ICompanyCertificateService companyCertificateService,ICompanyService companyService,IDocumentService documentService, IDocumentTypeService documentTypeService, IDocumentTypeBranchOfficeSerieService documentTypeBranchOfficeSerieService, IMethodPaymentService methodPaymentService, IProductService productService, ICustomerService customerService)
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
            _noteService = noteService;
            _noteDetailService = noteDetailService;
            _noteDetailDocumentDetailService = noteDetailDocumentDetailService;
            _identityDocumentTypeService = identityDocumentTypeService;
            _branchOfficeService = branchOfficeService;
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

            var documentTypeId = documentTypes.Select(x => x.DocumentTypeId).First();

            var series = await _documenttypeBranchOfficeSerieService.GetByCompanyIdDocumentTypeIdAsync(HttpContext.Session.GetInt32("companyId").Value, documentTypeId);

            var serie = series.Select(x => x.Serie).First();

            var number = await _documentService.GetNewNumberByDocumentTypeIdSerieIndicatorAsync(HttpContext.Session.GetInt32("companyId").Value, documentTypeId,serie);

            var methodPayment = await _methodPaymentService.GetAllAsync();
            var product = await _productService.GetProductQuantityByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);
            var customer = await _customerService.GetByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);

            var document = new DocumentCreateViewModel
            {
                DocumentTypes = documentTypes,
                Series = series,
                Number = number.ToString(),
                IssueDate = DateTime.Now,
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

            if(ModelState.IsValid)
            {
                var companyModel = await _companyService.GetByIdAsync(HttpContext.Session.GetInt32("companyId").Value);
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
                var noteModel = new NoteModel();
                var noteDetailModel = new NoteDetailModel();
                var noteDetailDocumentDetailModel = new NoteDetailDocumentDetailModel();


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

                noteModel.NoteId = 0;
                noteModel.NoteTypeId = 8;
                noteModel.BranchOfficeId = 1;
                noteModel.CustomerId = documentCreateViewModel.Customer.CustomerId;
                noteModel.IssueDate = documentCreateViewModel.IssueDate;
                noteModel.Number = await Task.Run(async () => { return await _noteService.GetNewNumberByActionTypeAsync(1); });
                noteModel.Description = "Nota de Salida Por Venta de Mercaderia" +" | " + Convert.ToString(serie.Serie) + " - " + ("00000000" + documentCreateViewModel.Number).Substring(("00000000" + documentCreateViewModel.Number).Length - 8, 8);
                noteModel.CreatorUser = "";
                noteModel.UpdaterUser = "";
                noteModel.CreateDate = DateTime.Now;
                noteModel.UpdateDate = DateTime.Now;
                noteModel.Status = true;
                noteModel.Removed = false;

                await Task.Run(async () =>
                { return await _noteService.AddAsync(noteModel); }
                );

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

                        noteDetailModel.NoteDetailId = 0;
                        noteDetailModel.NoteId = noteModel.NoteId;
                        noteDetailModel.ProductId = item.ProductId;
                        noteDetailModel.Quantity = item.Quantity;
                        noteDetailModel.UnitPrice = item.UnitPrice;
                        noteDetailModel.CreatorUser = "";
                        noteDetailModel.UpdaterUser = "";
                        noteDetailModel.CreateDate = DateTime.Now;
                        noteDetailModel.UpdateDate = DateTime.Now;
                        noteDetailModel.Status = true;
                        noteDetailModel.Removed = false;

                        await Task.Run(async () => { return await _noteDetailService.AddAsync(noteDetailModel); });

                        noteDetailDocumentDetailModel.NoteDetailDocumentDetailId = 0;
                        noteDetailDocumentDetailModel.DocumentDetailId = documentDetailModel.DocumentDetailId;
                        noteDetailDocumentDetailModel.NoteDetailId = noteDetailModel.NoteDetailId;

                        await Task.Run(async () => { return await _noteDetailDocumentDetailService.AddAsync(noteDetailDocumentDetailModel); });
                    }
                }
            }

            return RedirectToAction("Index", "Document");
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
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> GetSerie(int id)
        {
            var series = await _documenttypeBranchOfficeSerieService.GetByCompanyIdDocumentTypeIdAsync(HttpContext.Session.GetInt32("companyId").Value, id);

            return Json(series);

        }

        ////POST: Serie/Create
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> GetNumber(int id, string serie)
        {
            var number = await _documentService.GetNewNumberByDocumentTypeIdSerieIndicatorAsync(HttpContext.Session.GetInt32("companyId").Value, id, serie);

            return Json(number);

        }

        // GET: Document/Details/id
        [Authorize]
        public async Task<IActionResult> Details( int id)
        {
            var documentElectronic = await _documentElectronicService.GetByDocumentIdAsync(id);

            return View(documentElectronic);
        }

        // GET: Document/Print/id
        [Authorize]
        public async Task<IActionResult> Print(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ParameterUTL.toolsService);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var IntIdComprobante = id;
            var documentDetail = new List<DocumentDetail>();

            var EmpresaBe = await Task.Run(async () => { return await _companyService.GetByIdAsync(1); });
            var SucursalBe = await Task.Run(async () => { return await _branchOfficeService.GetByIdAsync(1); });
            var ComprobanteBe = await Task.Run(async () => { return await _documentService.GetByIdAsync(IntIdComprobante); });
            var ComprobanteElectronicoBe = await Task.Run(async () => { return await _documentElectronicService.GetByDocumentIdAsync(IntIdComprobante); });
            var TipoComprobanteBe = await Task.Run(async () => { return await _documentTypeService.GetByIdAsync(ComprobanteBe.DocumentTypeId); });
            var FormaPagoBe = await Task.Run(async () => { return await _methodPaymentService.GetByIdAsync(ComprobanteBe.MethodPaymentId); });
            var ClienteProveedorBe = await Task.Run(async () => { return await _customerService.GetByIdAsync(ComprobanteBe.CustomerId); });
            var detailDoc = await Task.Run(async () => { return await _documentDetailService.GetDocumentDetailProductByDocumentIdAsync(IntIdComprobante); });
            var TipoDocumentoIdentidadBe = await Task.Run(async () => { return await _identityDocumentTypeService.GetByIdAsync(ClienteProveedorBe.IdentityDocumentTypeId); });

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img","20449200854.png");
            ParameterUTL.StrRegistroUnicoContribuyente = EmpresaBe.IdentityDocumentNumber;
            ParameterUTL.StrRazonSocial = EmpresaBe.BusinessName;
            foreach (var item  in detailDoc)
            {
                var detail = new DocumentDetail();
                detail.Code = Convert.ToString(item.Code);
                detail.Description = Convert.ToString(item.Description);
                detail.Quantity = Convert.ToInt32(item.Quantity);
                detail.Unit = "";
                detail.UnitPrice = Math.Round(Convert.ToDecimal(item.UnitPrice), 2);

                documentDetail.Add(detail);
            }

            var documentoPdfRequest = new PdfRequest
            {
                Logo = Convert.ToBase64String(System.IO.File.ReadAllBytes(path)),
                Color = ParameterUTL.StrColorReport,
                Document = new DocumentHeader
                {
                    DocumentType = Convert.ToInt32(TipoComprobanteBe.DocumentTypeId),
                    Denomination = TipoComprobanteBe.Description,
                    Document = $"{ComprobanteBe.Serie}-{("00000000" + ComprobanteBe.Number).Substring(("00000000" + ComprobanteBe.Number).Length - 8, 8)}"
                },
                DocumentReference = new DocumentHeader
                {
                    DocumentType = 0,
                    Denomination = "fgdg",
                    Document = "gfgfg"
                },
                Company = new Company
                {
                    RUC = ParameterUTL.StrRegistroUnicoContribuyente,
                    BusinessName = ParameterUTL.StrRazonSocial,
                    Observation = "Servicio de Facturación para Mypes.",
                    Web = "ventas@gmail.com"
                },
                BranchOffice = new BranchOffice
                {
                    Name = SucursalBe.Name,
                    Address = SucursalBe.Address,
                    Cellular = SucursalBe.Cellular,
                    Telephone = SucursalBe.Telephone,
                    Email = ParameterUTL.StrRegistroUnicoContribuyente.Equals("20602631029") ? "segurimas.peru@hotmail.com" : "casaxperu@hotmail.com"
                },
                CustomerProvider = new CustomerProvider
                {
                    IdentityDocumentNumber = Convert.ToString(ClienteProveedorBe.IdentityDocumentNumber),
                    BusinessName = Convert.ToString(ClienteProveedorBe.BusinessName),
                    Address = ClienteProveedorBe.Address
                },
                IssueDate = ComprobanteBe.IssueDate,
                ExpiredDate = ComprobanteBe.IssueDate,
                DocumentDetails = documentDetail,
                TaxableAmount = ComprobanteBe.SubTotal,
                TaxAmount = ComprobanteBe.Tax,
                Amount = ComprobanteBe.Total,
                IsElectronic = (ComprobanteElectronicoBe == null ? false : true),
                MethodPayment = FormaPagoBe.Description,
                DocumentElectronic = new DocumentElectronic
                {
                    SummaryValue = ComprobanteElectronicoBe == null ? "" : ComprobanteElectronicoBe.SummaryValue,
                    StringQR = ComprobanteElectronicoBe == null ? "" : ComprobanteElectronicoBe.StringQR
                },
                CurrentAccount = new CurrentAccount
                {
                    FinancialEntity = "",
                    Denomination = "",
                    CurrentAccountNumber = "",
                    InterbankAccountCode = ""
                },
                AdditionalData = new AdditionalData
                {
                    AdditionalPrice = "",
                    ExpiratedDate = "",
                    Observation = ComprobanteBe.Observation
                }
            };

            var jsonEnvioPdfDocumento = await client.PostAsJsonAsync("api/v1/GenerateDocument", documentoPdfRequest);

            var Rpta = await jsonEnvioPdfDocumento.Content.ReadAsAsync<PdfResponse>();

            var documentoPdfResponse = (PdfResponse)Rpta;

            if (documentoPdfResponse.Success)
            {
                var StrArchivoValorPdf =  Convert.FromBase64String(documentoPdfResponse.Value);
                Stream stream = new MemoryStream(StrArchivoValorPdf);

                Response.Headers.Add("Content-Disposition", new ContentDisposition
                {
                    FileName = $"{ComprobanteBe.Serie}-{("00000000" + ComprobanteBe.Number).Substring(("00000000" + ComprobanteBe.Number).Length - 8, 8)}" + ".pdf",
                    Inline = true // false = prompt the user for downloading; true = browser to try to show the file inline
                }.ToString());

                return File(stream, "application/pdf");

            }
            else
            {
                return RedirectToAction("Index", "Document");
            }
            
        }

        // GET: Document/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentElectronic = await _documentElectronicService.GetByDocumentIdAsync(id.Value);
            if (documentElectronic == null)
            {
                return NotFound();
            }

            return View(documentElectronic);
        }

        // POST: Persons/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var NotaBe = new NoteModel();
            var NotaDetalleBe = new NoteDetailModel();
            var NotaDetalleComprobanteDetalleBe = new NoteDetailDocumentDetailModel();

            var documentModel = await _documentService.GetByIdAsync(id);


            if (documentModel != null)
            {
                var IntIdComprobante = Convert.ToInt32(id);

                var ComprobanteBe = await Task.Run(async () =>
                {
                    return await _documentService.GetByIdAsync(IntIdComprobante);
                });

                if (!ComprobanteBe.Status)
                {
                    ModelState.AddModelError("DocumentId", "Este registro no se puede anular, ya se encuentra anulado");
                    var documentElec= await _documentElectronicService.GetByDocumentIdAsync(id);
                    return View(documentElec);
                }
                

                ComprobanteBe.UpdaterUser = "";
                ComprobanteBe.UpdateDate = DateTime.Now;
                ComprobanteBe.Status = false;

                await Task.Run(async () => {
                    return await _documentService.UpdateAsync(ComprobanteBe);
                });

                NotaBe.NoteId = 0;
                NotaBe.NoteTypeId = 3;
                NotaBe.BranchOfficeId = 1;
                NotaBe.CustomerId = ComprobanteBe.CustomerId;
                NotaBe.IssueDate = DateTime.Now;

                var number = await Task.Run(async () => {
                    return await _noteService.GetNewNumberByActionTypeAsync(0);
                });

                NotaBe.Number = number;
                NotaBe.Description =  "Ingreso por Anulación de Comprobante" + " | Anulación de : " + Convert.ToString($"{ComprobanteBe.Serie}-{("00000000" + ComprobanteBe.Number).Substring(("00000000" + ComprobanteBe.Number).Length - 8, 8)}");
                NotaBe.CreatorUser = "";
                NotaBe.UpdaterUser = "";
                NotaBe.CreateDate = DateTime.Now;
                NotaBe.UpdateDate = DateTime.Now;
                NotaBe.Status = true;
                NotaBe.Removed = false;

                await Task.Run(async () => {
                    await _noteService.AddAsync(NotaBe);
                });

                var ComprobanteDetalle = await Task.Run(async () =>
                {
                    return await _documentDetailService.GetByDocumentIdAsync(IntIdComprobante);
                });

                foreach (var detalle in ComprobanteDetalle)
                {
                    detalle.UpdaterUser = "";
                    detalle.UpdateDate = DateTime.Now;
                    detalle.Status = false;
                    await Task.Run(async () => {
                        return await _documentDetailService.UpdateAsync(detalle);
                    });

                    NotaDetalleBe.NoteDetailId = 0;
                    NotaDetalleBe.NoteId = NotaBe.NoteId;
                    NotaDetalleBe.ProductId = detalle.ProductId;
                    NotaDetalleBe.Quantity = detalle.Quantity;
                    NotaDetalleBe.UnitPrice = detalle.UnitPrice;
                    NotaDetalleBe.CreatorUser = "";
                    NotaDetalleBe.UpdaterUser = "";
                    NotaDetalleBe.CreateDate = DateTime.Now;
                    NotaDetalleBe.UpdateDate = DateTime.Now;
                    NotaDetalleBe.Status = true;
                    NotaDetalleBe.Removed = false;


                    await Task.Run(async () => {
                        await _noteDetailService.AddAsync(NotaDetalleBe);
                    });

                    NotaDetalleComprobanteDetalleBe.NoteDetailDocumentDetailId = 0;
                    NotaDetalleComprobanteDetalleBe.NoteDetailId = NotaDetalleBe.NoteDetailId;
                    NotaDetalleComprobanteDetalleBe.DocumentDetailId = detalle.DocumentDetailId;

                    await Task.Run(async () => {
                        await _noteDetailDocumentDetailService.AddAsync(NotaDetalleComprobanteDetalleBe);
                    });
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("DocumentId", "Este registro no se puede anular, por que no existe");
            }
            var documentElectronic = await _documentElectronicService.GetByDocumentIdAsync(id);
            return View(documentElectronic);
        }

    }
}
