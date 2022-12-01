using Boonker.Data;
using Boonker.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Controllers
{
    public class AuthenController: Controller
    {
        public readonly BooksAddData context;
        public readonly UserManager<User> _userManager;
        public readonly SignInManager<User> _signInManager;


        public AuthenController(BooksAddData Data, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.context = Data;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public ActionResult Register()
        {
            User model = new User();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                User userCreate = new User
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    Password = model.Password,
                    PasswordCheck = model.PasswordCheck,
                    Followers = 0,
                    Role = "Client",
                    Image = "DefaultUserImage.png",
                    addId = context.User.Count() + 1
                };

                var result = await _userManager.CreateAsync(userCreate, model.Password);

                if (result.Succeeded) { 
                    return RedirectToAction("Login", "Authen"); }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        if(error.Code.ToString().ToLower().Contains("password"))
                        {
                            ModelState.AddModelError("Password", error.Description);
                            ModelState.AddModelError("PasswordCheck", error.Description);
                        }
                        if (error.Code.ToString().ToLower().Contains("email")){
                            ModelState.AddModelError("Email", error.Description);
                        }
                        if (error.Code.ToString().ToLower().Contains("username")){
                            ModelState.AddModelError("UserName", error.Description);
                        }
                    }
                }
            }
            return View(model); 
        }

        [HttpGet]
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            User user = new User();

            model.Email = user.Email;
            model.Password = user.Password;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var state = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememeberMe, false);

                if (state.Succeeded)
                {
                    return RedirectToAction("List", "Books");
                }
            }

            ModelState.AddModelError("Email", "Неправильный логин и (или) пароль");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("List", "Books");
        }
    }
}
