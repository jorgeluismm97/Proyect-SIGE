using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGe.Controllers
{
    public class CompanyCertificateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
