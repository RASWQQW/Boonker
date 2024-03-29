﻿using Boonker.Data;
using Boonker.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Boonker.Controllers
{
    public class BooksController : Controller
    {
        private readonly BooksAddData context;

        public readonly IWebHostEnvironment Enviro;

        public BooksController(BooksAddData Data, IWebHostEnvironment Env)
        {
            context = Data;
            Enviro = Env;
        }

        [Route("/Browse/")]
        public ActionResult Charts()
        {
            var obj = context.Books.Include(w => w.ImgEntry).Include(w => w.Category).Include(w => w.Author).OrderByDescending(w => w.Views).Take(10).ToList();
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

        [Route("/Books/Book/{id}/{title?}/{firstN?}/{lastN?}")]
        public ActionResult BookMore(int id, string title, string firstN, string lastN)
        {
            var obj = context.Books
                .Include(w => w.Category)
                .Include(w => w.ImgEntry)
                .Include(w => w.Author)
                .FirstOrDefault(w => w.Id == id);

            var simobj = context.Books.Include(w => w.Category).Include(w => w.ImgEntry).Include(w => w.Author)
                .Where(w => w.Category == obj.Category).ToList();

            // to check there is book in basket or not
            var inbasketIs = context.Basket.Where(w => w.User.Id == User.FindFirstValue(ClaimTypes.NameIdentifier) && w.Book.Id == obj.Id).Any();

            simobj.Remove(obj);

            CommonViewModel common = new CommonViewModel();

            obj.Views = obj.Views + 1; obj.Category.CatViews = obj.Category.CatViews + 1;

            common.InBasket = inbasketIs;
            common.books = obj;
            common.RecBooks = simobj;
            if (!(string.IsNullOrEmpty(title) && string.IsNullOrEmpty(firstN) && string.IsNullOrEmpty(lastN))
                ) { common.FoundObjets = SearchBook(title, firstN, lastN); }

            context.SaveChanges();

            return View(common);
        }

        //[Route("Books/UpdateBook/{id}")]
        public ActionResult UpdateBook(int id)
        {
            var book = context.Books.Include(w => w.ImgEntry).Include(w => w.Author).Include(w => w.Category).FirstOrDefault(x => x.Id == id);

            UpdateViewModel model = new UpdateViewModel();

            List<SelectListItem> categories = context.Cats.OrderBy(n => n.Name)
                .Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.Name
                }).ToList();

            List<SelectListItem> Authors = context.Authors.OrderBy(n => n.FirstName)
                .Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.FirstName

                }).ToList();

            model.Categories = categories;
            model.books = book;
            model.Authors = Authors;

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateBook(UpdateViewModel obj)
        {
            Book dbBook = context.Books.Include(w => w.Category).Include(w => w.ImgEntry).FirstOrDefault(b => b.Id == obj.books.Id);
            if (dbBook != null)
            {
                dbBook.Amount = obj.books.Amount;
                dbBook.Price = obj.books.Price;
                dbBook.Title = obj.books.Title;
                dbBook.DSCR = obj.books.DSCR;
                dbBook.IsFav = obj.books.IsFav;
                dbBook.AuthorId = obj.books.AuthorId;
                dbBook.CategoryId = obj.books.CategoryId;


                ExtraClasses cls = new ExtraClasses();

                if (obj.Images != null)
                {
                    dbBook.ImgEntry.Image1 = cls.Extracter(obj.Images.Image1, Enviro);
                    dbBook.ImgEntry.Image2 = cls.Extracter(obj.Images.Image2, Enviro);
                    dbBook.ImgEntry.Image3 = cls.Extracter(obj.Images.Image3, Enviro);
                    dbBook.ImgEntry.Image4 = cls.Extracter(obj.Images.Image4, Enviro);
                    dbBook.ImgEntry.Image5 = cls.Extracter(obj.Images.Image5, Enviro);

                }

                context.SaveChanges();
            }

            return RedirectToAction("List", "Books");
        }

        private void Extracter(string image11, IFormFile image12)
        {
            throw new NotImplementedException();
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
            if (dbBook != null)
            {
                context.Books.Remove(s);
                context.SaveChanges();
            }

            return RedirectToAction("List", "Books");
        }

        [HttpGet]
        public ActionResult Index()
        {
            ImagesForms forimg = new ImagesForms();

            UpdateViewModel model = new UpdateViewModel();
            Book Books = new Book();

            List<SelectListItem> categories = context.Cats.OrderBy(n => n.Name)
                .Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.Name
                }).ToList();

            List<SelectListItem> Authors = context.Authors.OrderBy(n => n.FirstName)
               .Select(n => new SelectListItem
               {
                   Value = n.Id.ToString(),
                   Text = n.FirstName
               }).ToList();

            model.Categories = categories;
            model.Authors = Authors;
            model.books = Books;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(UpdateViewModel model){

            UserCreatedBook created = new UserCreatedBook {

                CUser = context.User.FirstOrDefault(
                    w => w.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)),

                UserId = context.User.FirstOrDefault(
                    w => w.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)).addId,

                BookId = model.books.Id,
                CreatedBook = model.books

            }; context.UserCreatedBook.Add(created);

            ExtraClasses cls = new ExtraClasses();
            ImgEntry FullImage = new ImgEntry();

            FullImage.Id = context.ImgEntry.Count() + 1;

            if(model.Images != null)
            {
                FullImage.Image1 = cls.Extracter(model.Images.Image1, Enviro);
                FullImage.Image2 = cls.Extracter(model.Images.Image2, Enviro);
                FullImage.Image3 = cls.Extracter(model.Images.Image3, Enviro);
                FullImage.Image4 = cls.Extracter(model.Images.Image4, Enviro);
                FullImage.Image5 = cls.Extracter(model.Images.Image5, Enviro);
            }


            model.books.ImgEntry = FullImage;
            model.books.ImgEntryId = FullImage.Id;

            context.ImgEntry.Add(FullImage);context.Books.Add(model.books);
            context.SaveChanges();

            return RedirectToAction("List", "Books");
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
                booksList = context.Books.Include(t => t.Category)
                    .Include(w => w.Author)
                    .Include(s => s.ImgEntry)
                    .OrderByDescending(i => i.Id).ToList();

            }
            else {
                booksList = context.Books.Include(t => t.Category)
                    .Include(w => w.Author)
                    .Include(s => s.ImgEntry)
                    .Where(i => i.Category.Name.Equals(_cat)).OrderByDescending(i => i.Id).ToList();
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
            
        
        public List<FoundObjects> SearchBook(string title, string firstN, string lastN)
        {
            List<FoundObjects> objectfull = null;
            try
            {
                objectfull = new GoogleSearchApi().Mousae(title, firstN + " " + lastN);

                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (User.Identity.IsAuthenticated)
                {
                    var userIn = context.User.FirstOrDefault(w => w.Id == user);
                    var BookIn = context.Books.FirstOrDefault(w => w.Title.Replace(" ", "") == title &&
                                w.Author.FirstName.Replace(" ", "") == firstN && w.Author.LastName.Replace(" ", "") == lastN);

                    var SaveSearched = new BookSearched
                    {
                        User = userIn,
                        UserId = userIn.addId,
                        Book = BookIn,
                        BookId = BookIn.Id,
                        ResultList = objectfull
                    };

                    //Response.Cookies.Append();

                    context.Searched.Add(SaveSearched);
                    context.SaveChanges();
                }
            }   
            catch{
                 objectfull = new List<FoundObjects> { new FoundObjects{ Title = "None", Url = "https//:www.google.com" }};
            }

            return objectfull;

        }
    }
}
