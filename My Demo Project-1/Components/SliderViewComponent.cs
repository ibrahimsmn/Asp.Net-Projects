using GameNews.Models;
using GameNews.RepositoryDesingPattern.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GameNews.Components
{
    public class SliderViewComponent:ViewComponent
    {
        readonly INewsDto _newsDto;
        public SliderViewComponent(INewsDto newsDto)
        {
            _newsDto = newsDto;
        }

        public IViewComponentResult Invoke()
        {
            List<NewsVM> news = _newsDto.GetNewsSliderComponent();
            return View(news);
        }
    }
}
