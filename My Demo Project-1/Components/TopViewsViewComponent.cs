using GameNews.Models;
using GameNews.RepositoryDesingPattern.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GameNews.Components
{
    public class TopViewsViewComponent:ViewComponent
    {
        readonly INewsDto _newsDto;
        public TopViewsViewComponent(INewsDto newsDto)
        {
            _newsDto = newsDto;
        }
        public IViewComponentResult Invoke()
        {
            List<NewsVM> news = _newsDto.GetNewsMostViewed();
            return View(news);
        }
    }
}
