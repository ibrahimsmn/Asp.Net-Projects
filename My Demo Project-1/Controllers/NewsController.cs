using GameNews.Models;
using GameNews.RepositoryDesingPattern.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GameNews.Controllers
{
    public class NewsController : Controller
    {
        readonly INewsDto _newsDto;
        readonly IRepositoryBase<Comment> _commentRepo;
        readonly UserManager<AppUser> _userManager;
        readonly CookieHelper _cookieHelper;
        public NewsController(INewsDto newsDto, IRepositoryBase<Comment> commentRepo,UserManager<AppUser> userManager,CookieHelper cookieHelper)
        {
            _newsDto = newsDto;
            _commentRepo = commentRepo;
            _userManager = userManager;
            _cookieHelper = cookieHelper;
        }

        public IActionResult Index(string title, int id)
        {
            News news = _newsDto.GetNewsVMByComments(id);
            if (news==null)
            {
                return NotFound();
            }
            if (!_cookieHelper.HasNewsCookie(id))
            {
                news.Views++;
                _cookieHelper.SetNewsCookie(id);
                
                
            }
            
            _newsDto.Update(news);
            return View((news, new Comment()));
            
            
        }

        public async Task<IActionResult> Category(string name)
        {
            List<NewsVM> news = await _newsDto.GetNewsByCategoryName(name);
            ViewBag.Category = name.ToUpper();
            return View(news);
        }
        [HttpPost]
        public async Task< IActionResult> AddComment([Bind(Prefix ="item2")]Comment comment, [Bind(Prefix ="item1")]News news )
        {
            comment.NewsID = news.Id;
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                    comment.AppUser = user;
                    comment.Date = DateTime.Now;
                    _commentRepo.Add(comment);
                    return RedirectToAction("index", new { id = news.Id, title = news.Title});
                }
                else
                {
                    if (comment.VisitorName == null)
                    {
                        return RedirectToAction("index", new { id = news.Id, title = news.Title, comment });
                    }
                    else
                    {
                        comment.Date = DateTime.Now;
                        _commentRepo.Add(comment);
                        return RedirectToAction("index", new { id = news.Id, title = news.Title});
                    }
                }
            }
            
            
           
            
           
            return RedirectToAction("index",new {id=news.Id,title=news.Title,comment});
        }
    }
}
