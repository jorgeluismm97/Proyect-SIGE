using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGe.Controllers
{
    public class ReportController : Controller
    {
        private readonly IDocumentService _documentService;

        public ReportController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public IActionResult Index()
        {
            var reportViewModel = new ReportViewModel
            {
                startingDate = DateTime.Now,
                endingDate = DateTime.Now
            };

            return View(reportViewModel);
        }

        public async Task<IActionResult> Details(ReportViewModel reportViewModel)
        {
            var result = await _documentService.GetByCompanyIdDateAsync(HttpContext.Session.GetInt32("companyId").Value, reportViewModel.startingDate,reportViewModel.endingDate);

            return View(result);
        }
    }
}
