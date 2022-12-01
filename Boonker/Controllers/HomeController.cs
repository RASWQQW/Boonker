using Boonker.Data;
using Boonker.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Boonker.Controllers
{
    public class HomeController : Controller
    {
        public readonly BooksAddData context;
        public readonly UserManager<User> _userManager;
        public readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment Enviro;

        public HomeController(BooksAddData Data, UserManager<User> userManager, 
            SignInManager<User> signInManager, IWebHostEnvironment env)
        {
            context = Data;
            _userManager = userManager;
            _signInManager = signInManager;
            Enviro = env;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Welcome To Boonker";
            ViewBag.Also = "Our Boonker Library";
            return View();
        }

        public IActionResult UserPage()
        {
            UserViewModel mainUser = new UserViewModel();

            var user = context.User.FirstOrDefault(
                w => w.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = context.User.FirstOrDefault(w => w.Id == userId);
            var UserBooks = context.UserCreatedBook.Where(s => s.CUser == currentUser).Select(w => w.CreatedBook).ToList();

            mainUser.currentUser = currentUser;
            mainUser.UserBooks = UserBooks;


            return View(mainUser);
        }
        [HttpGet]
        [Route("Home/UserEdit/{user_id}")]  
        public IActionResult UserEdit(int user_id)
        {
            var user = context.User.FirstOrDefault(w => w.addId == user_id); 
            user.GetType();
            if(User.Identity.IsAuthenticated && (User.FindFirstValue(ClaimTypes.NameIdentifier) == user.Id || user.Role == "admin")) 
            {
                var User = context.User.FirstOrDefault(w => w.addId == user_id);

                var model = new UserViewModel
                {
                    currentUser = User
                };
                return View(model);
            }
            else { return RedirectToAction("Index", "Home"); }

            
        }
        [HttpPost]
        public IActionResult UserEdit(UserViewModel userMod)
        {
            
            var ChangeUser = context.User.FirstOrDefault(w => w.Id == userMod.currentUser.Id);

            ExtraClasses edit = new ExtraClasses();

            ChangeUser.UserName = userMod.currentUser.UserName;
            ChangeUser.Email = userMod.currentUser.Email;
            ChangeUser.About = userMod.currentUser.About;
            if(userMod.UserImage != null){
                ChangeUser.Image = edit.UploadFile(Enviro, und: userMod.UserImage, fileN: null, option: "users");

            }

            context.SaveChanges();

            return RedirectToAction("UserPage", "Home");
        }
        
    }
}
