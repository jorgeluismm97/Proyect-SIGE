using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGe.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IIdentityDocumentTypeService _identityDocumentTypeService;

        public CustomerController(ICustomerService customerService, IIdentityDocumentTypeService identityDocumentTypeService)
        {
            _customerService = customerService;
            _identityDocumentTypeService = identityDocumentTypeService;
        }
        public async Task<IActionResult> Index()
        {
            var productModels = await _customerService.GetByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);
            return View(productModels);
        }

        // GET: User/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var identityDocumentType = await _identityDocumentTypeService.GetAllAsync();

            var customer = new CustomerViewModel
            {
                IdentityDocumentTypes = identityDocumentType
            };


            return View(customer);
        }

        // POST: User/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel customerViewModel)
        {
            if (customerViewModel.CustomerId == 0)
            {
                var customerModel = new CustomerModel
                {
                    CustomerId = 0,
                    CompanyId = HttpContext.Session.GetInt32("companyId").Value,
                    IdentityDocumentTypeId = customerViewModel.IdentityDocumentTypeId,
                    IdentityDocumentNumber = customerViewModel.IdentityDocumentNumber,
                    BusinessName = customerViewModel.BusinessName,
                    Address = customerViewModel.Address,
                    Email = customerViewModel.Email
                };

                await _customerService.AddAsync(customerModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("CustomerId", "No se puede realizar el registro.");
            }

            return View(customerViewModel);
        }

        // GET: User/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerModel = await _customerService.GetByIdAsync(id.Value);
            var identityDocumentType = await _identityDocumentTypeService.GetAllAsync();

            var customer = new CustomerViewModel
            {
                CustomerId = customerModel.CustomerId,
                IdentityDocumentTypes = identityDocumentType,
                IdentityDocumentTypeId = customerModel.IdentityDocumentTypeId,
                IdentityDocumentNumber = customerModel.IdentityDocumentNumber,
                BusinessName = customerModel.BusinessName,
                Address = customerModel.Address,
                Email = customerModel.Email
            };
            return View(customer);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerViewModel customerViewModel)
        {
            if (customerViewModel.CustomerId == 0)
            {
                return NotFound();
            }

            var customerModel = await _customerService.GetByIdAsync(customerViewModel.CustomerId);

            customerModel.BusinessName = customerViewModel.BusinessName;
            customerModel.Address = customerViewModel.Address;
            customerModel.Email = customerViewModel.Email;

            await _customerService.UpdateAsync(customerModel);

            return RedirectToAction(nameof(Index));
        }
    }
}
