using System.ComponentModel.DataAnnotations;

namespace GameNews.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
