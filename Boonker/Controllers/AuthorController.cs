using Boonker.Data;
using Boonker.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Controllers
{
    public class AuthorController : Controller
    {
        private readonly BooksAddData context;

        public AuthorController(BooksAddData Data)
        {
            this.context = Data;
        }
        public IActionResult AuthorView()
        {
            return View();
        }
        [Route("Author/CreateAuthor")]
        [HttpGet]
        public ActionResult CreateAuthor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAuthor(Author sended)
        {
            context.Authors.Add(sended);
            context.SaveChanges();

            return View("Index");
        }
    }
}
