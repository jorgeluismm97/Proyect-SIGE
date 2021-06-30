using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGe.Controllers
{
    public class NoteOutController : Controller
    {
        private readonly INoteTypeService _noteTypeService;
        private readonly INoteService _noteService;
        private readonly INoteDetailService _noteDetailService;
        private readonly IProductService _productService;

        public NoteOutController(INoteTypeService noteTypeService, INoteService noteService, INoteDetailService noteDetailService, IProductService productService)
        {
            _noteTypeService = noteTypeService;
            _noteService = noteService;
            _noteDetailService = noteDetailService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var noteViewModel = await _noteService.GetByCompanyIdActionTypeAsync(HttpContext.Session.GetInt32("companyId").Value, 1);

            return View(noteViewModel);
        }


        // Get: NoteIn/Create
        [Authotize]
        public async Task<IActionResult> Create()
        {

            var products = await _productService.GetByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);
            var noteTypes = await _noteTypeService.GetByActionType(1);
            var number = await _noteService.GetNewNumberByActionTypeAsync(1);

            var noteCreateViewModel = new NoteCreateViewModel
            {
                NoteTypes = noteTypes,
                IssueDate = DateTime.Now,
                Number = number.ToString(),
                Products = products,

            };

            return View(noteCreateViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoteCreateViewModel noteCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var noteModel = new NoteModel();
                var noteDetailModel = new NoteDetailModel();

                noteModel.NoteId = 0;
                noteModel.NoteTypeId = noteCreateViewModel.NoteTypeId;
                noteModel.BranchOfficeId = 1;
                noteModel.CustomerId = 1;
                noteModel.IssueDate = noteCreateViewModel.IssueDate;
                noteModel.Number = Convert.ToInt32(noteCreateViewModel.Number);
                noteModel.Description = noteCreateViewModel.Description;
                noteModel.CreatorUser = "";
                noteModel.UpdaterUser = "";
                noteModel.CreateDate = DateTime.Now;
                noteModel.UpdateDate = DateTime.Now;
                noteModel.Status = true;
                noteModel.Removed = false;

                await Task.Run(async () =>
                { return await _noteService.AddAsync(noteModel); }
                );

                foreach (var item in noteCreateViewModel.Details)
                {
                    noteDetailModel.NoteDetailId = 0;
                    noteDetailModel.NoteId = noteModel.NoteId;
                    noteDetailModel.ProductId = item.ProductId;
                    noteDetailModel.Quantity = item.Quantity;
                    noteDetailModel.UnitPrice = item.UnitPrice;
                    noteDetailModel.CreatorUser = "";
                    noteDetailModel.UpdaterUser = "";
                    noteDetailModel.CreateDate = DateTime.Now;
                    noteDetailModel.UpdateDate = DateTime.Now;
                    noteDetailModel.Status = true;
                    noteDetailModel.Removed = false;

                    await Task.Run(async () =>{ return await _noteDetailService.AddAsync(noteDetailModel); });
                }
            }

            return RedirectToAction("index", "NoteOut");
        }

        public async Task<IActionResult> Details(int id)
        {
            var lstnote = await _noteService.GetByCompanyIdActionTypeAsync(HttpContext.Session.GetInt32("companyId").Value, 1);
            var detail = await _noteDetailService.GetNoteDetailProductByNoteIdAsync(id);
            var note = lstnote.Where(x => x.NoteId == id).First();

            var noteDetailViewModel = new NoteDetailViewModel
            {
                NoteId = note.NoteId,
                IssueDate = note.IssueDate,
                Note = note.Note,
                NoteType = note.NoteType,
                Description = note.Description,
                Status = note.Status,
                Details = detail
            };

            return View(noteDetailViewModel);
        }
    }
}
