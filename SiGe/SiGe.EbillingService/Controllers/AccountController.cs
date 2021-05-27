using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SiGe.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPersonService _personService;
        private readonly ICompanyService _companyService;

        public AccountController(IUserService userService, IPersonService personService, ICompanyService companyService)
        {
            _userService = userService;
            _personService = personService;
            _companyService = companyService;
        }

        public IActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Register()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var personModelvalidate = await _personService.GetByIdentityDocumentTypeIdIdentityDocumentNumberAsync(model.IdentityDocumentTypeId, model.IdentityDocumentNumber);

                if(personModelvalidate != null)
                {
                    ModelState.AddModelError("IdentityDocumentNumber", "El número de documento de identidad se encuentra registrado");
                }
                else
                {
                    var personModel = new PersonModel
                    {
                        PersonId = 0,
                        IdentityDocumentTypeId = model.IdentityDocumentTypeId,
                        IdentityDocumentNumber = model.IdentityDocumentNumber,
                        FatherLastName = model.FatherLastName,
                        MotherLastName = model.MotherLastName,
                        Name = model.Name,
                        Email = model.Email
                    };

                    var resultPerson = await _personService.AddAsync(personModel);

                    if (resultPerson == 1)
                    {
                        var userModel = new UserModel
                        {
                            UserId = 0,
                            PersonId = personModel.PersonId,
                            UserName = model.IdentityDocumentNumber,
                            Password = UtilitarianUTL.EncriptarCadena(model.Password),
                            IsAdministrator = true
                        };
                        var resultUser = await _userService.AddAsync(userModel);
                        if (resultUser == 1)
                        {

                            var userClaims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name,$"{personModel.FatherLastName} {personModel.MotherLastName}, {personModel.Name}"),
                            new Claim(ClaimTypes.Email, "anet@test.com"),
                        };

                            var grandmaIdentity =
                            new ClaimsIdentity(userClaims, "User Identity");

                            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                            await HttpContext.SignInAsync(userPrincipal);
                            HttpContext.Session.SetInt32("userId", userModel.UserId);
                            return RedirectToAction("index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    }
                }
            }

            return View(model);
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {

                if(user.IdentityAction== 1)
                {
                    var userModel = await _userService.ValidateAsync(user.UserName, user.Password);

                    if (userModel != null)
                    {
                        var person = await _personService.GetByIdAsync(userModel.PersonId);
                        var userClaims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.Name,$"{person.FatherLastName} {person.MotherLastName}, {person.Name}"),
                                new Claim(ClaimTypes.Email, "anet@test.com"),
                            };

                        var grandmaIdentity =
                        new ClaimsIdentity(userClaims, "User Identity");

                        var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                        await HttpContext.SignInAsync(userPrincipal);
                        HttpContext.Session.SetInt32("userId", userModel.UserId);
                        HttpContext.Session.SetInt32("IdentityAction", user.IdentityAction);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("UserName", "Nombre de Usuario o Contraseña Invalido");
                }
                else
                {
                    var companyModel = await _companyService.GetByUserNameIdentityDocumentNumberAsync(user.UserName, user.IdentityDocumentNumber);

                    if(companyModel != null)
                    {
                        var userModel = await _userService.ValidateAsync(user.UserName, user.Password);

                        if (userModel != null)
                        {
                            var person = await _personService.GetByIdAsync(userModel.PersonId);
                            var userClaims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.Name,$"{person.FatherLastName} {person.MotherLastName}, {person.Name}"),
                                new Claim(ClaimTypes.Email, "anet@test.com"),
                            };

                            var grandmaIdentity =
                            new ClaimsIdentity(userClaims, "User Identity");

                            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                            await HttpContext.SignInAsync(userPrincipal);
                            HttpContext.Session.SetInt32("userId", userModel.UserId);
                            HttpContext.Session.SetInt32("companyId", companyModel.CompanyId);
                            HttpContext.Session.SetInt32("isAdministrator", userModel.IsAdministrator?1:0);
                            HttpContext.Session.SetInt32("IdentityAction", user.IdentityAction);
                            return RedirectToAction("Index", "Home");
                        }
                        ModelState.AddModelError("UserName", "Nombre de Usuario o Contraseña Invalido");
                    }
                    ModelState.AddModelError("IdentityDocumentNumber", "No se puede acceder al panel de esta empresa.");
                }
                



                
            }
            return View(user);
            
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var userId = HttpContext.Session.GetInt32("userId").Value;

            var userEntity = await _userService.GetByIdAsync(userId);
            var personEntity = await _personService.GetByIdAsync(userEntity.PersonId);

            var registerViewModel = new RegisterViewModel
            {
                UserId = userId,
                IdentityDocumentTypeId = personEntity.IdentityDocumentTypeId,
                IdentityDocumentNumber = personEntity.IdentityDocumentNumber,
                FatherLastName = personEntity.FatherLastName,
                MotherLastName = personEntity.MotherLastName,
                Name = personEntity.Name,
                Email = personEntity.Email,
                Password = userEntity.Password,
                ConfirmPassword = userEntity.Password
            };



            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var userModel = await _userService.GetByIdAsync(model.UserId);

                userModel.Password = UtilitarianUTL.EncriptarCadena(model.Password);
                userModel.UpdaterUser = "";
                userModel.UpdateDate = DateTime.Now;

                var resultUser = await _userService.UpdateAsync(userModel);

                if (resultUser != 1)
                {
                    ModelState.AddModelError("Password", "No se pudo modificar la contraseña.");
                }

                var personModel = await _personService.GetByIdAsync(userModel.PersonId);

                personModel.FatherLastName = model.FatherLastName;
                personModel.MotherLastName = model.MotherLastName;
                personModel.Name = model.Name;
                personModel.Email = model.Email;
                personModel.UpdaterUser = "";
                personModel.UpdateDate = DateTime.Now;

                var resultPerson = await _personService.UpdateAsync(personModel);

                if (resultPerson != 1)
                {
                    ModelState.AddModelError("Email", "No se pudo modificar el correo electronico.");
                }
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
