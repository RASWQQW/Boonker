using Boonker.Data;
using Boonker.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Controllers
{
    public class BooksController: Controller
    {
        private readonly BooksAddData context;

        public BooksController(BooksAddData Data)
        {
            context = Data;
        }

        [Route("/Browse/")]
        public ActionResult Charts()
        {
            var obj = context.Books.Include(w => w.Category).Include(w => w.Author).OrderByDescending( w => w.Views).Take(10).ToList();
            var simobj = context.Cats.OrderByDescending(w => w.CatViews).ToList();
            List<int> AmountList = new List<int>();


            foreach (var item in simobj)
            {
               //get full amount of cats in the books whereas count total view 
               int s = context.Books.Include(w => w.Category).Include(w => w.Author).Where(w => w.Category == item).Count();
               
               AmountList.Add(s);
                
            }

            CommonViewModel common = new CommonViewModel();
            common.RecBooks = obj;
            common.AllCats = simobj;
            common.CatAmount = AmountList;

            return View(common);

        }

        [Route("Books/Book/{id}")]
        public ActionResult BookMore(int id)
        {
            var obj = context.Books.Include(w => w.Category).FirstOrDefault(w => w.Id == id);
            var simobj = context.Books.Include(w => w.Category).Include(w => w.Author).Where(w => w.Category == obj.Category).ToList();
            simobj.Remove(obj);

            CommonViewModel common = new CommonViewModel();

            common.RecBooks = simobj;

            obj.Views = obj.Views + 1; obj.Category.CatViews = obj.Category.CatViews + 1;
            
            common.books = obj;
            
            context.SaveChanges();

            return View(common);
        }

        //[Route("Books/UpdateBook/{id}")]
        public ActionResult UpdateBook(int id)
        {
            var book = context.Books.Include(w => w.Author).Include(w => w.Category).FirstOrDefault(x => x.Id == id);

            UpdateViewModel model = new UpdateViewModel();

            List<SelectListItem> categories = context.Cats.OrderBy(n => n.Name)
                .Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(), 
                    Text = n.Name
                }).ToList();

            List<SelectListItem> Authors = context.Authors.OrderBy(n => n.Name)
                .Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.Name

                }).ToList();   

            model.Categories = categories;
            model.books = book;
            model.Authors = Authors;

            return View(model);  
        }

        [HttpPost]
        public ActionResult UpdateBook(UpdateViewModel obj)
        {
            Book dbBook = context.Books.FirstOrDefault(b => b.Id == obj.books.Id);
            if (dbBook != null)
            {
                //context.Books.Remove(dbBook);  
                //context.Books.Add(obj);  
                //Author Author = context.Authors.FirstOrDefault(w => w.Name == obj.books.Author.Name);
                //if(Author == null) { Author = new Author { Name = obj.books.Author.Name, DSCR = "" };  }

                dbBook.Amount = obj.books.Amount;
                dbBook.Price = obj.books.Price;
                dbBook.Title = obj.books.Title;
                dbBook.DSCR = obj.books.DSCR;
                dbBook.IsFav = obj.books.IsFav;
                dbBook.IsFav = obj.books.IsFav;
                dbBook.AuthorId = obj.books.AuthorId;
                dbBook.Img = obj.books.Img;
                dbBook.CategoryId = obj.books.CategoryId;
                //dbBook.Category = obj.books.Category;

                context.SaveChanges();
            }

            return RedirectToAction("List", "Books");
        }

        public ActionResult Delete(int id)
        {
            var book = context.Books.FirstOrDefault(x => x.Id == id);

            return View(book);
        }
            
        [HttpPost]
        public ActionResult Delete(Book s)
        {
            Console.WriteLine(s.Id);
            Book dbBook = context.Books.FirstOrDefault(b => b.Id == s.Id);
            if(dbBook != null)
            {
                context.Books.Remove(s);
                context.SaveChanges();
            }
            
            return RedirectToAction("List", "Books");
        }

        [HttpGet]
        public ActionResult Index()
        {
            Book obj = new Book();
            UpdateViewModel model = new UpdateViewModel();

            List<SelectListItem> categories = context.Cats.OrderBy(n => n.Name)
                .Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.Name
                }).ToList();

             List<SelectListItem> Authors = context.Authors.OrderBy(n => n.Name)
                .Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.Name
                }).ToList();

            model.Categories = categories;
            model.books = obj;
            model.Authors = Authors;
            
            return View(model);
        }
           
        [HttpPost]
        public ActionResult Index(UpdateViewModel model)
        {
            //model.books.Author = new Author { Name = model.books.Author.Name, DSCR = "" };
            context.Books.Add(model.books);
            context.SaveChanges();

            return View("List");
        }

        [Route("Books/List")]
        [Route("Books/List/{category}")]
        public ViewResult List(string category)
        {
            string _cat = category;
            List<Book> booksList = null;
            Cat currCat = null;

            if (string.IsNullOrEmpty(_cat))
            {
                booksList = context.Books.Include(t => t.Category).OrderBy(i => i.Id).ToList();

            }
            else {
                booksList = context.Books.Include(t => t.Category)
                    .Where(i => i.Category.Name.Equals(_cat)).OrderBy(i => i.Id).ToList();
                currCat = context.Cats.FirstOrDefault(w => w.Name == _cat); 
            }

            List<Cat> AllCats = context.Cats.ToList();

            var BookObject = new ViewModel {
                Allbooks = booksList,
                cats = currCat,
                AllCats = AllCats

            };

            ViewBag.Title = "Welcome to the Lib";
            ViewBag.Also = "Our Popular Books Here";

            return View(BookObject);
        }

    }
}
