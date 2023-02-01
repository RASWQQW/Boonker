using Boonker.Data;
using Boonker.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace Boonker.Controllers
{
    public class AuthorController : Controller
    {
        private readonly BooksAddData context;
        private readonly IWebHostEnvironment Enviro;

        public AuthorController(BooksAddData Data, IWebHostEnvironment env)
        {
            this.context = Data;
            Enviro = env;
        }
        

        //[HttpGet]
        [Route("Author/AuthorPage/{AuthorId}")]
        public IActionResult AuthorPage(int AuthorId)
        {
            var Author = context.Authors.FirstOrDefault(w => w.Id == AuthorId);

            AuthorViewModel model = new AuthorViewModel();

            int BooksAmount = context.Books.Where(w => w.Author.Id == AuthorId).Count();
            var Janres = context.Books
                .Include(w => w.ImgEntry)
                .Include(w => w.Category)
                .Include(c => c.Category).Where(w => w.Author.Id == AuthorId).Select(w => w.Category.Name).ToList();

            List<Book> Books = context.Books
                .Include(w => w.Category)
                .Include(w=> w.Author)
                .Include(w => w.ImgEntry).Where(w => w.Author.Id == AuthorId).ToList();

            model.Author = Author;
            model.BooksAmount = BooksAmount;
            model.AllJanres = Janres;
            model.AuthorBooks = Books;
            return View(model);

        }

        public IActionResult AuthorView()
        {
            return View();
        }

        [Route("Author/CreateAuthor")]
        [HttpGet]
        public ActionResult CreateAuthor()
        {
            CreateAuthorViewModel User = new CreateAuthorViewModel();
            return View(User);
        }

        [HttpPost]
        public ActionResult CreateAuthor(CreateAuthorViewModel sended)
        {
            if(ModelState.IsValid){
                ExtraClasses file = new ExtraClasses();
                if (sended.Image is null)
                {
                    ModelState.AddModelError("Image", "You must to load photo!");
                    return View(sended);
                }
                else
                {
                    sended.Author.Image = file.UploadFile(Enviro: Enviro, fileN: sended, und: null, option: "authors");
                    context.Authors.Add(sended.Author);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Books");
                }
            }
            return View(sended);

        }

        [Route("Author/AuthorList/")]
        public ActionResult AuthorList()
        {
            var Authors = context.Authors.OrderByDescending(w => w.FirstName).ToList();

            AuthorsView auth = new AuthorsView { Authors=Authors};

             return View(auth);
        }
    }
}
