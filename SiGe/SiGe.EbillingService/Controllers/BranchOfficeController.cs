using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGe.Controllers
{
    public class BranchOfficeController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IBranchOfficeService _branchOfficeService;

        public BranchOfficeController(IPersonService personService, IBranchOfficeService branchOfficeService)
        {
            _personService = personService;
            _branchOfficeService = branchOfficeService;
        }

        public async Task<IActionResult> Index()
        {
            var branchOfficeModel = await _branchOfficeService.GetByCompanyId(HttpContext.Session.GetInt32("companyId").Value);

            return View(branchOfficeModel);
        }

        // GET: BranchOffice/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var persons = await _personService.GetByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);

            var branchOfficeViewModel = new BranchOfficeViewModel
            {
                personModels = persons
            };


            return View(branchOfficeViewModel);
        }

        // POST: BranchOffice/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BranchOfficeViewModel branchOfficeViewModel)
        {
            var branchOffice = await _branchOfficeService.GetByCode(branchOfficeViewModel.branchOfficeModel.Code);

            if (branchOffice.Count == 0)
            {
                var person = await _personService.GetByIdAsync(branchOfficeViewModel.branchOfficeModel.PersonId);

                var branchOfficeModel = new BranchOfficeModel
                {
                    BranchOfficeId = 0,
                    CompanyId = HttpContext.Session.GetInt32("companyId").Value,
                    PersonId = branchOfficeViewModel.branchOfficeModel.PersonId,
                    Code = branchOfficeViewModel.branchOfficeModel.Code,
                    Name = branchOfficeViewModel.branchOfficeModel.Name,
                    Address = branchOfficeViewModel.branchOfficeModel.Address,
                    Telephone = branchOfficeViewModel.branchOfficeModel.Telephone,
                    Cellular = branchOfficeViewModel.branchOfficeModel.Cellular
                };

                await _branchOfficeService.AddAsync(branchOfficeModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("Code", "Este codigo ya se encuentra registrado.");
            }

            return View(branchOfficeViewModel);
        }

        // GET: BranchOffice/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchOfficeModel = await _branchOfficeService.GetByIdAsync(id.Value);
            var personModels = await _personService.GetByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);

            var branchOfficeViewModel = new BranchOfficeViewModel
            {
                personModels = personModels,
                branchOfficeModel = branchOfficeModel
            };

            return View(branchOfficeViewModel);
        }

    }
}
