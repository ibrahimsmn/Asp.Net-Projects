 using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GameNews.Models
{
    public class AppUser:IdentityUser<int>
    {
        public string ImageUrl { get; set; }


    }
}
