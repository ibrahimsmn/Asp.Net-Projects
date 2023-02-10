using GameNews.Models;
using GameNews.RepositoryDesingPattern.Base;
using GameNews.RepositoryDesingPattern.Concrete;
using GameNews.RepositoryDesingPattern.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GameNews.Components
{
    public class LastCommentsViewComponent:ViewComponent
    {
        readonly INewsDto _newsRepository;
        public LastCommentsViewComponent(INewsDto newRepository)
        {
           _newsRepository = newRepository;
        }
        
        public IViewComponentResult Invoke()
        {
            List<NewsVM> news=_newsRepository.GetNewsLastComments();
            return View(news);
        }
    }
}
