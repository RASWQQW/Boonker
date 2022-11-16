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
        //public readonly BooksAddData context;
        public readonly UserManager<User> _userManager;
        public readonly SignInManager<User> _signInManager;

        public AuthenController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            //this.context = Data;
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
                    PasswordCheck = model.PasswordCheck
                };

                var result = await _userManager.CreateAsync(userCreate, model.Password);

                if (result.Succeeded) { 
                    return RedirectToAction("Authen", "Login"); }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    
                }
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult Login()
        {
            User model = new User();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememeberMe, false);

            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Books", "List");
        }
    }
}
