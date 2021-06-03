using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGe.Controllers
{
    public class CompanyCredentialController : Controller
    {

        private readonly ICompanyCredentialService _companyCredentialService;

        public CompanyCredentialController(ICompanyCredentialService companyCredentialService)
        {
            _companyCredentialService = companyCredentialService;
        }

        public async Task<IActionResult> Index()
        {
            var credentials = await _companyCredentialService.GetByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);

            return View(credentials);
        }


        // GET: CompanyCredential/Create
        [Authorize]
        public IActionResult Create()
        {
            return View(new CompanyCredentialModel());
        }

        // POST: CompanyCredential/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyCredentialModel companyCredentialModel)
        {
            companyCredentialModel.CompanyId = HttpContext.Session.GetInt32("companyId").Value;
            await _companyCredentialService.AddAsync(companyCredentialModel);
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

            var companyModel = await _companyCredentialService.GetByIdAsync(id.Value);

            return View(companyModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyCredentialModel companyCredentialModel)
        {
            if (companyCredentialModel.CompanyCredentialId == 0)
            {
                return NotFound();
            }

            companyCredentialModel.PasswordSOL = UtilitarianUTL.EncriptarCadena(companyCredentialModel.PasswordSOL);
            companyCredentialModel.UpdaterUser = "";
            companyCredentialModel.UpdateDate = DateTime.Now;

            await _companyCredentialService.UpdateAsync(companyCredentialModel);

            return RedirectToAction(nameof(Index));
        }

    }
}
