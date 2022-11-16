using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Controllers
{
    public class AdminController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        public AdminController(RoleManager<IdentityRole> manager)
        {
            _roleManager = manager;
        }

        public async Task<IActionResult> GetRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
            return View(await _roleManager.Roles.ToListAsync());
        }
    }
}