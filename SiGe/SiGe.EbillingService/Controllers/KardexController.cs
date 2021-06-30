using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGe.Controllers
{
    public class KardexController : Controller
    {
        private readonly IProductService _productService;
        private readonly INoteService _noteService;

        public KardexController(IProductService productService, INoteService noteService)
        {
            _productService = productService;
            _noteService = noteService;
        }
        // GET: KardexController
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();

            var kardexModelView = new KardexViewModel
            {
                Products = products
            };

            return View(kardexModelView) ;
        }

        // GET: KardexController/Details/5
        public async Task<IActionResult> Details(KardexViewModel kardexViewModel)
        {
            var balance = await _noteService.GetNoteBalanceKardexSimpleAsync(kardexViewModel.Product.ProductId,1);
            var details = await _noteService.GetNoteKardexSimpleAsync(kardexViewModel.Product.ProductId, 1);

            var det = new List<DetailViewModel>();
            int IntCantidad = 0;
            int IntBalance = 0;
            if(balance.Count>0)
            {
                IntCantidad = 0; // balance.Sum(x => x.Quantity);
                IntBalance = IntCantidad;
            }


            foreach (var item in details)
            {
                if (item.ActionTYpe == 0)
                {
                    IntCantidad += item.Quantity;
                }
                else
                {
                    IntCantidad -= item.Quantity;
                }

                var x = new DetailViewModel
                {
                    Day = item.IssueDate.Day,
                    Month = item.IssueDate.Month,
                    Year = item.IssueDate.Year,
                    Description = item.Description,
                    QuantityIn =item.ActionTYpe == 0 ? item.Quantity :  0,
                    QuantityOut = item.ActionTYpe == 1 ? item.Quantity : 0,
                    Balance = IntCantidad
                };

                det.Add(x);
            }


            var kardexDetailModel = new KardexDetailModel
            {
                Product = kardexViewModel.Product,
                BalanceIn = IntBalance,
                BalanceOut = IntCantidad,
                Details = det
            };

            return View(kardexDetailModel);
        }

        // GET: KardexController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KardexController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: KardexController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: KardexController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: KardexController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: KardexController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        ////POST: Serie/Create
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> AddProduct(int id)
        {
            var x = await _productService.GetByIdAsync(id);

            return Json(x);

        }
    }
}
