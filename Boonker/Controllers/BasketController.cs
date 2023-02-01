using Boonker.Data;
using Boonker.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Boonker.Controllers
{
    public class BasketController : Controller
    {
        public readonly BooksAddData context;
        public BasketController(BooksAddData CT)
        {
            context = CT;
        }
        [HttpPost]
        [Route("Basket/AddingToBasket/{bookId?}/{amount?}")]
        public ActionResult AddingToBasket(int bookId, int amount)
        {
            var _user = context.User
                .FirstOrDefault(w => w.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if(_user != null)
            {
                Basket _basket = new Basket{ 
                User = _user, Book = context.Books.FirstOrDefault(w => w.Id == bookId), AmountOf=amount};
                context.Basket.Add(_basket); context.SaveChanges();
                return RedirectToAction("BookMore", "Books", new { @id= bookId });
            }
            else {
                 return RedirectToAction("Login", "Authen");
            }
            
        }
        
        [Route("Basket/UserBasket/{username?}")]
        public ActionResult UserBasket(string username)
        {
            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var _ids = context.Basket.Where(w => w.User.Id == _userId).Select(w => w.Book.Id).ToList();
            List<Basket> books = new List<Basket>();

            foreach(var id in _ids)
            {
                var s = id;
                var book = context.Books.Include(w => w.Category).Include(w => w.Author).Include(w => w.ImgEntry).FirstOrDefault(w => w.Id == id);


                Basket basket = new Basket();
                basket.Book = book;
                basket.AmountOf = context.Basket.FirstOrDefault(w => w.Book == book).AmountOf;

                books.Add(basket);
            }


            ViewBasketOf _basket = new ViewBasketOf();
            _basket.AllBasketBooks = books;
            return View(_basket);

        } 

        [Route("Basket/DeleteGood/{bookId?}")]
        public ActionResult DeleteGood(int bookId)
        {
            var _delElem = context.Basket.FirstOrDefault(w => w.Book.Id == bookId);
            context.Basket.Remove(_delElem); context.SaveChanges();
            return RedirectToAction("UserBasket", "Basket");
        }
    }
}
