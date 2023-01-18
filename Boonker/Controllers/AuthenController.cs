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
    public class AuthenController : Controller
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
                        if (error.Code.ToString().ToLower().Contains("password"))
                        {
                            ModelState.AddModelError("Password", error.Description);
                            ModelState.AddModelError("PasswordCheck", error.Description);
                        }
                        if (error.Code.ToString().ToLower().Contains("email")) {
                            ModelState.AddModelError("Email", error.Description);
                        }
                        if (error.Code.ToString().ToLower().Contains("username")) {
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

            ViewBag.Submit = "Log in ";
            model.Email = user.Email;
            model.Password = user.Password;
            return View("Login", model);
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
            ViewBag.Submit = "Login in";
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

        [HttpGet]
        public ActionResult RecoverPass()
        {
            LoginViewModel model = new LoginViewModel();
            User user = new User();

            model.Email = user.Email;
            ViewBag.Submit = "Send";

            return View("Login", model);
        }
        // this method will be send reply about reset of password and save operation in base
        [HttpPost]
        public ActionResult RecoverPass(User model)
        {
            var userIsTrue = context.User.FirstOrDefault(x => x.Email == model.Email);

            if (model.Email != null)
            {
                if (userIsTrue != null)
                {
                    var number = new Random().Next(1000, 9999);
                    var userobj = context.PassResOps.FirstOrDefault(w => w.email == userIsTrue.Email);

                    switch (userobj)
                    {
                        case null: context.PassResOps.Add(new ResOps { email = userIsTrue.Email, codekey = number });
                            userobj = context.PassResOps.FirstOrDefault(w => w.email == userIsTrue.Email); break;
                        default: userobj.codekey = number; break;
                    }
                    context.SaveChanges();

                    Classes.SendEmilToCheck check = new Classes.SendEmilToCheck();
                    check.SendEmail(model.Email, number);
                    Response.Cookies.Append("UserEmail", userobj.email);
                    return RedirectToAction("CodeReceiever", "Authen", new {model = new UserViewModel()});
                }
                else {
                    ModelState.AddModelError("Email", "Email is not found");
                }
            }
            return RedirectToAction("Authen", "Login");
        }

        [HttpGet][HttpPost]
        public ActionResult CodeReceiever(UserViewModel model)
        {
            if(model.keycode == 0){
                model = new UserViewModel();
                return View("CodeReceiever", model);
            }
            else{
                var UserEmail = Request.Cookies["userEmail"];
                var number = context.PassResOps.FirstOrDefault(x => x.email == UserEmail);
                if(model.keycode == number.codekey) {
                    return RedirectToAction("PasSet", "Authen");
                }
                ModelState.AddModelError("keycode", "Code is wrong");
            }
            return View(model);
        }

        [HttpPost][HttpGet]
        public async Task<ActionResult> PasSetAsync(LoginViewModel model)
        {
            if(model.Password != null){
                var UserEmail = Request.Cookies["UserEmail"];
                if (model.Password.Length >= 8 && model.Password.Any(x => !char.IsLetter(x))){
                    var user = context.User.FirstOrDefault(x => x.Email == UserEmail);

                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                    var result = await _userManager.UpdateAsync(user);
                    var sec = await _userManager.UpdateSecurityStampAsync(user);
                    context.SaveChanges();

                    if (result.Succeeded)
                    {
                        user.Password = model.Password;
                        user.PasswordCheck = model.Password;
                        context.SaveChanges();
                        return RedirectToAction("Login", "Authen");
                    }
                    ModelState.AddModelError("Password", "Password has mistakes");
                    return View("PasSet", model);
                }
                
            }
            else{ model = new LoginViewModel(); }

            return View("PasSet", model);

        }
        
    }
}
