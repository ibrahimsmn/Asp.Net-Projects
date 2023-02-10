using GameNews.Models;
using GameNews.RepositoryDesingPattern.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GameNews.Areas.Management.Controllers
{
    [Area("management")]
    [Authorize(Roles = "writer")]
    public class NewsController : Controller
    {
        readonly IRepositoryBase<News> _reponews;
        readonly IRepositoryBase<Category> _repository;
        readonly INewsDto _newsDto;
        readonly UserManager<AppUser> _userManager;
        public NewsController(IRepositoryBase<News> reponews, INewsDto newsDto, IRepositoryBase<Category> repository, UserManager<AppUser> userManager)
        {
            _reponews = reponews;
            _newsDto = newsDto;
            _repository = repository;
            _userManager = userManager;

        }

        public async Task<IActionResult> Index()
        {
            List<NewsVM> news = await _newsDto.GetNewsByAuthor(User.Identity.Name);
            return View();
        }

        public IActionResult DeleteNews(int id)
        {
            
            _reponews.Delete(id);
            return RedirectToAction("index");
        }

        public async Task<IActionResult> EditNews(int id)
        {
            News information = _reponews.GetById(id);
            if (information == null)
            {
                return RedirectToAction("index");
            }

            List<Category> categories = await _repository.GetAll();
            return View((information, categories));
        }
        [HttpPost]
        public IActionResult EditNews([Bind(Prefix = "item1")] News information)
        {
            information.ModifiedDate = System.DateTime.Now;
            _reponews.Update(information);
            return RedirectToAction("index");
        }
        public async Task<IActionResult> MyPosts()
        {
            List<NewsVM> news = await _newsDto.GetNewsByAuthor(User.Identity.Name);
            return View(news);
        }
        public async Task<IActionResult> AddNews()
        {
            ViewBag.Categories = await _repository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNews(NewsVM model)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                var extension = Path.GetExtension(model.ImageURL.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/sidebar/", newimagename);

                using (var stream = new FileStream(location, FileMode.Create))
                {
                    model.ImageURL.CopyTo(stream);
                }
                News information = new News
                {
                    ImageUrl = newimagename,
                    CategoryID = model.CatergoryID,
                    Description = model.Description,
                    Title = model.Title,
                    Source = model.Source,
                    Content = model.Content,
                    AppUserID = user.Id,
                    PublishedAt = DateTime.Now,

                };
                _newsDto.Update(information);
                return RedirectToAction("index");
            }
            return View(model);
        }

    }
}
