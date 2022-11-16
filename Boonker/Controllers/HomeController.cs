using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            ViewBag.Title = "Welcome To Boonker";
            ViewBag.Also = "Our Boonker Library";
            return View();
        }
    }
}
