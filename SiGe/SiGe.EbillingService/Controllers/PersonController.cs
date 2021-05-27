using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGe.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IPersonCompanyService _personCompanyService;
        public PersonController(IPersonService personService,IPersonCompanyService personCompanyService)
        {
            _personService = personService;
            _personCompanyService = personCompanyService;
        }

        public async Task<IActionResult> Index()
        {
            var personModels = await _personService.GetByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);
            return View(personModels);
        }

        // GET: Product/Create
        [Authorize]
        public IActionResult Create()
        {
            return View(new PersonModel());
        }

        // POST: Product/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonModel personModel)
        {
            var person = await _personService.GetByIdentityDocumentTypeIdIdentityDocumentNumberAsync(0,personModel.IdentityDocumentNumber);

            if (person == null || person.PersonId == 0)
            {
                await _personService.AddAsync(personModel);

                var personCompany = new PersonCompanyModel
                {
                    PersonCompanyId = 0,
                    PersonId = personModel.PersonId,
                    CompanyId = HttpContext.Session.GetInt32("companyId").Value
                };
                
                await _personCompanyService.AddAsync(personCompany);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("IdentityDocumentNumber", "Este persona ya se encuentra registrada");
            }
            return View(personModel);
        }

        // GET: Companies/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyModel = await _personService.GetByIdAsync(id.Value);

            return View(companyModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PersonModel personModel)
        {
            if (personModel.PersonId == 0)
            {
                return NotFound();
            }
            personModel.UpdaterUser = "";
            personModel.UpdateDate = DateTime.Now;

            await _personService.UpdateAsync(personModel);

            return RedirectToAction(nameof(Index));
        }

        // GET: Companies/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personModel = await _personService.GetByIdAsync(id.Value);
            if (personModel == null)
            {
                return NotFound();
            }

            return View(personModel);
        }

        // POST: Companies/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personModel = await _personService.GetByIdAsync(id);


            if (personModel == null)
            {
                personModel.UpdaterUser = "";
                personModel.UpdateDate = DateTime.Now;
                personModel.Status = false;
                personModel.Removed = true;

                await _personService.UpdateAsync(personModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("PersonId", "Este registro no se puede eliminar, por que no existe");
            }

            return View(personModel);
        }

    }
}
