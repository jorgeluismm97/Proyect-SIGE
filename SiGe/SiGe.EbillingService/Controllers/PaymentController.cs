using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SiGe.Models.PaypalHelper;
using System;
using System.Threading.Tasks;

namespace SiGe.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IPaymentOperationService _paymentOperationService;
        private readonly IConfiguration _configuration;
        public PaymentController(IPaymentService paymentService, IPaymentOperationService paymentOperationService, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _paymentOperationService = paymentOperationService;
            _configuration = configuration;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        // GET: Instructors/Create
        [Authorize]
        public IActionResult Create(int? id)
        {

            var paymentModel = new PaymentModel();
            paymentModel.CompanyId = id.Value;
            return View(paymentModel);
        }

        // POST: Instructors/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentModel paymentModel)
        {

            var payment = await _paymentService.GetByCompanyIdDateAsync(paymentModel.CompanyId, DateTime.Now.AddDays(1));
            if(payment.Count>0)
            {
                ModelState.AddModelError("PaymentPlanId", "Esta empresa tiene una suscripción activa.");
            }
            else
            {
                paymentModel.IssueDate = DateTime.Now;
                paymentModel.StartingDate = DateTime.Now;
                paymentModel.EndingDate = paymentModel.PaymentPlanId == 1 ? DateTime.Now.AddMonths(1) : DateTime.Now.AddYears(1);
                await _paymentService.AddAsync(paymentModel);
                HttpContext.Session.SetInt32("paymentId", paymentModel.PaymentId);
                var paypalAPI = new PaypalAPI(_configuration);
                string url = await paypalAPI.getRedirectURLToPaypal(paymentModel.PaymentPlanId == 1 ? 50 : 500, "USD");


                if (url == null)
                {
                    return View("Error");
                }
                return Redirect(url);
            }

            return View(paymentModel);
            //return RedirectToAction("Index", "Company");
        }


        public async Task<IActionResult> Success([FromQuery(Name ="paymentId")] string paymentId, [FromQuery(Name = "PayerID")] string payerID)
        {

            var paypalAPI = new PaypalAPI(_configuration);
            PaypalPaymentExecutedResponse result = await paypalAPI.executedPayment(paymentId, payerID);
            ViewBag.result = result;

            var paymentOperation = new PaymentOperationModel();

            paymentOperation.PaymentOperationId = 0;
            paymentOperation.PaymentId = HttpContext.Session.GetInt32("paymentId").Value;
            paymentOperation.StringPaymentId = paymentId;
            paymentOperation.StringPayerId = payerID;

            await _paymentOperationService.AddAsync(paymentOperation);

            return View("Success");
            //paymentModel.IssueDate = DateTime.Now;
            //paymentModel.StartingDate = DateTime.Now;
            //paymentModel.EndingDate = paymentModel.PaymentPlanId == 1 ? DateTime.Now.AddMonths(1) : DateTime.Now.AddYears(1);
            //await _paymentService.AddAsync(paymentModel);
            //return RedirectToAction("Index", "Company");
        }
    }
}
