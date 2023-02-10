using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameNews.Models
{
    public class RoleVM
    {

        [Required(ErrorMessage ="Boş Bırakmayınız!")]
        [MaxLength(20,ErrorMessage ="En fazla 20 karakter giriniz.")]
        [MinLength(5,ErrorMessage ="En az 5 karakter giriniz.")]
        public string Name { get; set; }
    }
}
