using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGe.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IPersonCompanyService _personCompanyService;
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;

        public CompanyController(ICompanyService companyService, IPersonCompanyService personCompanyService, IPaymentService paymentService, IUserService userService)
        {
            _companyService = companyService;
            _personCompanyService = personCompanyService;
            _paymentService = paymentService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var companyViewModels = new List<CompanyViewModel>();
            var userId = HttpContext.Session.GetInt32("userId").Value;
            

            if(HttpContext.Session.GetInt32("IdentityAction") == 1)
            {
                var companyModels = await _companyService.GetByUserIdAsync(userId);
                foreach (var companyModel in companyModels)
                {
                    var paymentModels = await _paymentService.GetByCompanyIdAsync(companyModel.CompanyId);

                    var companyViewModel = new CompanyViewModel
                    {
                        companyModel = companyModel,
                        paymentModels = paymentModels

                    };
                    companyViewModels.Add(companyViewModel);
                }
            }
            else
            {
                var companyModel = await _companyService.GetByIdAsync(HttpContext.Session.GetInt32("companyId").Value);
                var paymentModels = await _paymentService.GetByCompanyIdAsync(companyModel.CompanyId);

                var companyViewModel = new CompanyViewModel
                {
                    companyModel = companyModel,
                    paymentModels = paymentModels

                };
                companyViewModels.Add(companyViewModel);
            }
            
            
            return View(companyViewModels);
        }

        // GET: Company/Create
        [Authorize]
        public IActionResult Create()
        {
            return View(new CompanyModel());
        }

        // POST: Company/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyModel companyModel)
        {
            var company = await _companyService.GetByIdentityDocumentNumberAsync(companyModel.IdentityDocumentNumber);

            if(company == null || company.CompanyId == 0)
            {
                await _companyService.AddAsync(companyModel);

                var userEntity = await _userService.GetByIdAsync(HttpContext.Session.GetInt32("userId").Value);

                var personCompanyModel = new PersonCompanyModel();
                personCompanyModel.PersonCompanyId = 0;
                personCompanyModel.PersonId = userEntity.PersonId;
                personCompanyModel.CompanyId = companyModel.CompanyId;

                await _personCompanyService.AddAsync(personCompanyModel);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("IdentityDocumentNumber", "Este numero de RUC se encuentra registrado");
            }
            return View(companyModel);
        }

        // GET: Companies/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyModel = await _companyService.GetByIdAsync(id.Value);

            return View(companyModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( CompanyModel companyModel)
        {
            if (companyModel.CompanyId == 0)
            {
                return NotFound();
            }
            companyModel.UpdaterUser = "";
            companyModel.UpdateDate = DateTime.Now;

            await _companyService.UpdateAsync(companyModel);

            return RedirectToAction(nameof(Index));
        }

        // GET: Instructors/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyModel = await _companyService.GetByIdAsync(id.Value);
            if (companyModel == null)
            {
                return NotFound();
            }

            return View(companyModel);
        }

        // POST: Instructors/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentModel = await _paymentService.GetByCompanyIdAsync(id);
            var companyModel = await _companyService.GetByIdAsync(id);


            if (paymentModel == null)
            {
                companyModel.UpdaterUser = "";
                companyModel.UpdateDate = DateTime.Now;
                companyModel.Status = false;
                companyModel.Removed = true;

                await _companyService.UpdateAsync(companyModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("CompanyId", "Este registro no se puede eliminar, tiene una suscripcion activa");
            }

            return View(companyModel);
        }
    }
}
