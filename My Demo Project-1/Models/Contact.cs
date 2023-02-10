using System.ComponentModel.DataAnnotations;

namespace GameNews.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [MaxLength(1000)]
        [Required]
        public string Message { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        public int? AppUserID { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}
