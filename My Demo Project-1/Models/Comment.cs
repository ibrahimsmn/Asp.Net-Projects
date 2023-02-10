using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameNews.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(300)]
        public string Text { get; set; }
        public int? AppUserID { get; set; }
        
        [MaxLength(20)]
        public string? VisitorName { get; set; }
        
        public int NewsID { get; set; }
        public DateTime Date { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual News News { get; set; }
       

    }
}
