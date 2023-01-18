using Boonker.Data;
using Boonker.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [Route("Home/UserPage/{id?}")]
        public IActionResult UserPage(int id)
        {   
            UserViewModel mainUser = new UserViewModel();

            User currentUser = null; List<Follows> Status = null;

            if (id == 0 || id == null){
                currentUser = context.User.FirstOrDefault(
                    w => w.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            else{
                currentUser = context.User.FirstOrDefault(
                    w => w.addId == id);

                User IsUsers = null; User ToUser = null;
                GainUser(id, ref ToUser, ref IsUsers, ref Status);
            }

            var UserBooks = context.Books
               .Include(w => w.Author).Include(w => w.Category).Include(w => w.ImgEntry)
               .Join(context.UserCreatedBook.Where(w => w.CUser.Id == currentUser.Id),
                   c => c.Id, t => t.CreatedBook.Id, (c, t) => new Book
                   {
                       ImgEntry = c.ImgEntry,
                       Author = c.Author,
                       Title = c.Title,
                       Id = c.Id,
                       Price = c.Price,
                   }
               ).ToList();

            mainUser.currentUser = currentUser;
            mainUser.UserBooks = UserBooks;
            mainUser.Status = Status;

            return id == 0 ? View(mainUser) : View("UserPageA", mainUser);
        }

        [Route("Home/Follow/{id}")]
        public ActionResult Indexs(int id)
        {
            User IsUsers = null; List<Follows> Status = null; User ToUser = null;
            GainUser(id, ref ToUser, ref IsUsers, ref Status);

            if(Status.Count() == 0){
                ToUser.Followers = ToUser.Followers + 1;
                Follows NewSection = new Follows
                {
                    UserId = ToUser.addId,
                    FollowedUserId = IsUsers.addId,
                    User = ToUser,
                    FollowedUser = IsUsers
                };
                context.Follows.Add(NewSection); 
            }
            else{
                ToUser.Followers = ToUser.Followers - 1;
                context.Follows.Remove(Status.First());
            }
            context.SaveChanges();

            return RedirectToAction("UserPage", "Home", new { id = id});
        }

        public void GainUser(int id, ref User ToUser, ref User IsUsers, ref List<Follows> Status){
            ToUser = context.User.FirstOrDefault(w => w.addId == id);
            var IsUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            IsUsers = context.User.FirstOrDefault(w => w.Id == IsUser);
            var tUser = ToUser.addId; var iUser = IsUsers.addId;

            Status = context.Follows.Where(w => w.UserId == tUser && w.FollowedUserId == iUser).ToList();
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

        [HttpGet][HttpPost]
        public async Task<IActionResult> ChangePasswordAsync(UpdateViewModel model)
        {
            if(model == null) { return View(new UpdateViewModel()); }
            else
            {
                var user = context.User.FirstOrDefault(
                    w => w.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (user.Password == model.CurrentPassword)
                {
                    if(model.NewPassword.Length > 8 && model.NewPassword.Any(x => !char.IsLetter(x)))
                    {
                        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
                        var result = await _userManager.UpdateAsync(user);
                        user.Password = model.NewPassword;
                        user.PasswordCheck = model.NewPassword;
                        context.SaveChanges();
                        return RedirectToAction("UserPage", "Home");
                    }
                    return View(model);
                }
                return View(model);
            }
        } 
        
    }
}
