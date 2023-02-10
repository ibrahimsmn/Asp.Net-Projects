using GameNews.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameNews.Components
{
    public class CommentViewComponent:ViewComponent
    {
        UserManager<AppUser> _usermanager;
        public CommentViewComponent(UserManager<AppUser> usermanager)
        {
            _usermanager = usermanager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
                AppUser user = await _usermanager.FindByNameAsync(User.Identity.Name);
                return View(user);
        }
    }
}
