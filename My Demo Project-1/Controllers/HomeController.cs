using GameNews.Models;
using GameNews.RepositoryDesingPattern.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameNews.Controllers
{

    public class HomeController : Controller
    {
        readonly INewsDto _news;
        readonly IRepositoryBase<Contact> _contactRepository;
        readonly UserManager<AppUser> _userManager;
        readonly CookieHelper _cookieHelper;
        public HomeController(INewsDto news, UserManager<AppUser> userManager,IRepositoryBase<Contact> contactrepository, CookieHelper cookieHelper)
        {
            _news = news;
            _userManager = userManager;
            _contactRepository = contactrepository;
            _cookieHelper = cookieHelper;
        }

        public  async Task<IActionResult> Index()
        {
            List<NewsVM> news = _news.GetNewsVMIndex();

            return View(news);
        }
        public IActionResult Contact()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                    contact.AppUserID = user.Id;
                    contact.Name = user.UserName;
                    _contactRepository.Add(contact);
                    return RedirectToAction("index");
                }
                _contactRepository.Add(contact);
                return RedirectToAction("index");
            }


            return View(contact);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
