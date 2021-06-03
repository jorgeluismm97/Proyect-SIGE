using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SiGe.Controllers
{
    public class CompanyCertificateController : Controller
    {
        private readonly ICompanyCertificateService _companyCertificateService;

        public CompanyCertificateController(ICompanyCertificateService companyCertificateService)
        {
            _companyCertificateService = companyCertificateService;
        }

        public async Task<IActionResult> Index()
        {
            var credentials = await _companyCertificateService.GetByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);

            return View(credentials);
        }


        // GET: CompanyCredential/Create
        [Authorize]
        public IActionResult Create()
        {
            return View(new CompanyCertificateViewModel());
        }

        // POST: CompanyCredential/Create
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CompanyCertificateViewModel companyCertificateViewModel)
        {
            var value = "";
            using (var memoryStream = new MemoryStream())
            {
                await companyCertificateViewModel.File.CopyToAsync(memoryStream);

                value = Convert.ToBase64String(memoryStream.ToArray());
            }

            var companyCertificateModel = new CompanyCertificateModel
            {
                CompanyCertificateId = companyCertificateViewModel.CompanyCertificateId,
                CompanyId = HttpContext.Session.GetInt32("companyId").Value,
                Value = value,
                Password = UtilitarianUTL.EncriptarCadena(companyCertificateViewModel.Password),
                ExpiredDate = companyCertificateViewModel.ExpiredDate
            };

            ;
            await _companyCertificateService.AddAsync(companyCertificateModel);
            return RedirectToAction(nameof(Index));
        }

        // GET: CompanyCredentials/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyModel = await _companyCertificateService.GetByIdAsync(id.Value);

            return View(companyModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyCertificateModel companyCertificateModel)
        {
            if (companyCertificateModel.CompanyCertificateId == 0)
            {
                return NotFound();
            }

            companyCertificateModel.Password = UtilitarianUTL.EncriptarCadena(companyCertificateModel.Password);
            companyCertificateModel.UpdaterUser = "";
            companyCertificateModel.UpdateDate = DateTime.Now;

            await _companyCertificateService.UpdateAsync(companyCertificateModel);

            return RedirectToAction(nameof(Index));
        }
    }
}
