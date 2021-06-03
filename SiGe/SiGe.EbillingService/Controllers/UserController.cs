using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGe.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly IPersonService _personService;

        public UserController(IUserService userService, IPersonService personService)
        {
            _userService = userService;
            _personService = personService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetByCompanyId(HttpContext.Session.GetInt32("companyId").Value);
            return View(user);
        }

        // GET: User/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var persons = await _personService.GetWithOutUserByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);

            var user = new UserViewModel
            {
                personModels = persons
            };
            

            return View(user);
        }

        // POST: User/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
            if(userViewModel.UserId == 0)
            {
                var person = await _personService.GetByIdAsync(userViewModel.personId);

                var userModel = new UserModel
                {
                    UserId = 0,
                    PersonId = userViewModel.personId,
                    UserName = person.IdentityDocumentNumber,
                    Password = UtilitarianUTL.EncriptarCadena(userViewModel.Password),
                    IsAdministrator = false
                };

                await _userService.AddAsync(userModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("personId", "No existen más personal sin usuario.");
            }

            return View(userViewModel);
        }

        // GET: User/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _userService.GetByIdAsync(id.Value);
            var personModels = await _personService.GetByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);

            var user = new UserViewModel
            {
                personModels = personModels,
                UserId = userModel.UserId,
                personId = userModel.PersonId,
            };

            return View(user);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel userViewModel)
        {
            if (userViewModel.UserId == 0)
            {
                return NotFound();
            }
            var person = await _personService.GetByIdAsync(userViewModel.personId);
            var userModel = new UserModel
            {
                UserId = userViewModel.UserId,
                PersonId = userViewModel.personId,
                UserName = person.IdentityDocumentNumber,
                Password = UtilitarianUTL.EncriptarCadena(userViewModel.Password),
                IsAdministrator = false,
                UpdaterUser = "",
                UpdateDate = DateTime.Now
            };

            await _userService.UpdateAsync(userModel);

            return RedirectToAction(nameof(Index));
        }

    }
}
