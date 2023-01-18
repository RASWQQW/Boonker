using Boonker.Data;
using Boonker.Data.Models;
using Microsoft.AspNetCore.Mvc;
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
        [Route("Basket/AddingToBasket/{book}")]
        public ActionResult AddingToBasket(int book)
        {
            var _user = context.User
                .FirstOrDefault(w => w.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if(_user != null)
            {
                Basket _basket = new Basket{ 
                User = _user, Book = context.Books.FirstOrDefault(w => w.Id == book) };
                context.Basket.Add(_basket); context.SaveChanges();
                return RedirectToAction("BookMore", "Books", new { @id= book });
            }
            else {
                 return RedirectToAction("Login", "Authen");
            }
            
        }
        
        [Route("Basket/UserBasket/{username}")]
       public ActionResult UserBasket(string username)
        {
            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = context.Basket.Where(w => w.User.Id == _userId);
            return View(model);
        } 
    }
}
