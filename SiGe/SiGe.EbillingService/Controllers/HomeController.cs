using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiGe.Controllers
{
    public class HomeController : Controller
    {

        private readonly IDocumentService _documentService;
        private readonly INoteService _noteService;

        public HomeController(IDocumentService documentService, INoteService noteService)
        {
            _documentService = documentService;
            _noteService = noteService;
        }

        [Authorize]
        public IActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }

            var home = new HomeViewModel
            {
                NumberOfCustomer = 1,
                NumberOfIn = 1,
                NumberOfOut = 2,
                NumberOfSales = 1
            };

            return View(home);
        }
    }
}
