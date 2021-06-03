using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGe.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var companyId = HttpContext.Session.GetInt32("companyId").Value;
            var productModels = await _productService.GetByCompanyIdAsync(companyId);
            return View(productModels);
        }

        // GET: Product/Create
        [Authorize]
        public IActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: Product/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel productModel)
        {
            var product = await _productService.GetByCodeAsync(productModel.Code);

            if (product == null || product.ProductId == 0)
            {
                productModel.CompanyId = HttpContext.Session.GetInt32("companyId").Value;
                await _productService.AddAsync(productModel);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("Code", "Este codigo ya se encuentra registrado");
            }
            return View(productModel);
        }

        // GET: Product/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _productService.GetByIdAsync(id.Value);

            return View(productModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductModel productModel)
        {
            if (productModel.CompanyId == 0)
            {
                return NotFound();
            }
            productModel.UpdaterUser = "";
            productModel.UpdateDate = DateTime.Now;

            await _productService.UpdateAsync(productModel);

            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _productService.GetByIdAsync(id.Value);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // POST: Product/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentModel = true; // await _paymentService.GetByCompanyIdAsync(id);
            var productModel = await _productService.GetByIdAsync(id);


            if (paymentModel)
            {
                productModel.UpdaterUser = "";
                productModel.UpdateDate = DateTime.Now;
                productModel.Status = false;
                productModel.Removed = true;

                await _productService.UpdateAsync(productModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("CompanyId", "Este registro no se puede eliminar, tiene una suscripcion activa");
            }

            return View(productModel);
        }

        [HttpPost]
        public async Task<JsonResult> BuscarProducto(string description)
        {
            var companyId = HttpContext.Session.GetInt32("companyId").Value;
            var product = await _productService.GetByDescriptionCompanyIdAsync(description, companyId);
            return Json(product);
        }
    }
}
