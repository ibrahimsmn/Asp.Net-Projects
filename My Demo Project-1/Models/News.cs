using System.Collections.Generic;
using System;

namespace GameNews.Models
{
    public  class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryID { get; set; }
        public string Source { get; set; }
        public int AppUserID { get; set; }
        public int Views { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
