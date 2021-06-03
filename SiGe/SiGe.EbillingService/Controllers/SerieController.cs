using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGe.Controllers
{
    public class SerieController : Controller
    {
        private readonly IDocumentTypeService _documentTypeService;
        private readonly IBranchOfficeService _branchOfficeService;
        private readonly IDocumentTypeBranchOfficeSerieService _documentTypeBranchOfficeSerie;

        public SerieController(IDocumentTypeService documentTypeService, IBranchOfficeService branchOfficeService, IDocumentTypeBranchOfficeSerieService documentTypeBranchOfficeSerie)
        {
            _documentTypeService = documentTypeService;
            _branchOfficeService = branchOfficeService;
            _documentTypeBranchOfficeSerie = documentTypeBranchOfficeSerie;
        }


        public async Task<IActionResult> Index()
        {
            var serie = await _documentTypeBranchOfficeSerie.GetByCompanyIdAsync(HttpContext.Session.GetInt32("companyId").Value);
            return View(serie);
        }

        // GET: Serie/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var branchs = await _branchOfficeService.GetByCompanyId(HttpContext.Session.GetInt32("companyId").Value);
            var documentTypes = await _documentTypeService.GetAllAsync();

            var serie = new SerieViewModel
            {
                DocumentTypes = documentTypes,
                BranchOffices = branchs
            };


            return View(serie);
        }

        // POST: Serie/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SerieViewModel serieViewModel)
        {
            if (serieViewModel.DocumentTypeBranchOfficeSerieId == 0)
            {
                var documentTypeBranchOfficeSerie  = new DocumentTypeBranchOfficeSerieModel
                {
                    DocumentTypeBranchOfficeSerieId = 0,
                    DocumentTypeId = serieViewModel.DocumentTypeId,
                    BranchOfficeId = serieViewModel.BranchOfficeId,
                    Serie = serieViewModel.Serie,
                    IsElectronic = serieViewModel.IsElectronic
                };

                await _documentTypeBranchOfficeSerie.AddAsync(documentTypeBranchOfficeSerie);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("personId", "No existen más personal sin usuario.");
            }

            return View(serieViewModel);
        }

        // GET: User/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentTypeBranchOffice = await _documentTypeBranchOfficeSerie.GetByIdAsync(id.Value);

            var branchs = await _branchOfficeService.GetByCompanyId(HttpContext.Session.GetInt32("companyId").Value);
            var documentTypes = await _documentTypeService.GetAllAsync();

            var serie = new SerieViewModel
            {
                DocumentTypeBranchOfficeSerieId = documentTypeBranchOffice.DocumentTypeBranchOfficeSerieId,
                DocumentTypes = documentTypes,
                DocumentTypeId = documentTypeBranchOffice.DocumentTypeId,
                BranchOffices = branchs,
                BranchOfficeId = documentTypeBranchOffice.BranchOfficeId,
                Serie = documentTypeBranchOffice.Serie,
                IsElectronic = documentTypeBranchOffice.IsElectronic
            };


            return View(serie);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SerieViewModel serieViewModel)
        {
            if (serieViewModel.DocumentTypeBranchOfficeSerieId == 0)
            {
                return NotFound();
            }

            var  documentTypeBranchOfficeSerie = await _documentTypeBranchOfficeSerie.GetByIdAsync(serieViewModel.DocumentTypeBranchOfficeSerieId);

            documentTypeBranchOfficeSerie.DocumentTypeId = serieViewModel.DocumentTypeId;
            documentTypeBranchOfficeSerie.BranchOfficeId = serieViewModel.BranchOfficeId;
            documentTypeBranchOfficeSerie.Serie = serieViewModel.Serie;
            documentTypeBranchOfficeSerie.IsElectronic = serieViewModel.IsElectronic;
            documentTypeBranchOfficeSerie.UpdaterUser = "";
            documentTypeBranchOfficeSerie.UpdateDate = DateTime.Now;

            await _documentTypeBranchOfficeSerie.UpdateAsync(documentTypeBranchOfficeSerie);

            return RedirectToAction(nameof(Index));
        }

    }
}
