using GameNews.Context;
using GameNews.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameNews.Components
{
    public class NavbarViewComponent: ViewComponent
    {
        UserManager<AppUser> _usermanager;
        public NavbarViewComponent(UserManager<AppUser> usermanager)
        {
           _usermanager = usermanager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _usermanager.FindByNameAsync(User.Identity.Name);
                return View(user);
            }
            
            return View(new AppUser());
           
        }
    }
}
