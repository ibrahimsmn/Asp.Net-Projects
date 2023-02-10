using GameNews.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameNews.Areas.Management.Controllers
{
    [Area("management")]
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        readonly RoleManager<AppUserRole> _roleManager;
        readonly UserManager<AppUser> _userManager;
        public HomeController(RoleManager<AppUserRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;

        }

        public async Task< IActionResult> Index()
        {
            return View(_userManager.Users.ToList());
        }
        public async Task<IActionResult> DeleteRole(string id)
        {
           AppUserRole role = await _roleManager.FindByIdAsync(id);
           await _roleManager.DeleteAsync(role);
            return RedirectToAction("index");
        }
        public IActionResult Roles()
        {
            return View(_roleManager.Roles.ToList());
        }
        public IActionResult AddRole()
        {

            return View();
        }
        [HttpPost]
        public async Task< IActionResult> AddRole(RoleVM role)
        {
            var result = await _roleManager.CreateAsync(new AppUserRole { Name = role.Name });
            if (result.Succeeded)
            {
                return RedirectToAction("index");

            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                    
                }
                return View(role);
            }
            return View();
        }
        public async Task<IActionResult> RoleAssign(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            List<AppUserRole> roles = _roleManager.Roles.ToList();
            List<string> userRoles = await _userManager.GetRolesAsync(user) as List<string>;
            List<RoleAssignViewModel> assignRoles = new List<RoleAssignViewModel>();
            roles.ForEach(role => assignRoles.Add(new RoleAssignViewModel
            {
                HasAssign = userRoles.Contains(role.Name),
                RoleId = role.Id,
                RoleName = role.Name
            }));

            return View(assignRoles);
        }
        [HttpPost]
        public async Task<IActionResult> RoleAssign(List<RoleAssignViewModel> modellist,string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            foreach (RoleAssignViewModel role in modellist)
            {
                if (role.HasAssign)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
                else
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
            }
            return RedirectToAction("Index");
        }

    }
}
