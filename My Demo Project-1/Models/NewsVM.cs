using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameNews.Models
{
    public class NewsVM
    {
        [Required]
        [MinLength(1)]  
        public string Title { get; set; }
        public int ID { get; set; }
        public int Views { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        [Required]

        public IFormFile ImageURL { get; set; }
        public string ImageUrlName => ImageURL.FileName;
        public int CatergoryID { get; set; }
        public DateTime dateTime { get; set; }
        public string Category { get; set; }
        public string ImageURLL { get; set; }
        public string Source { get; set; }
        public string FrontEndTitle => Title.Length >= 50 ? Title.Remove(47)+"..." : Title;
        public string SliderTitle => Title.Length >= 70 ? Title.Remove(67) + "..." : Title;
    }
}
