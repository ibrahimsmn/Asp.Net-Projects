using Microsoft.AspNetCore.Http;
using System;

namespace GameNews.Models
{
    public class CookieHelper
    {
        
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetNewsCookie(int newsId)
        {
            string cookieName = $"news_{newsId}";
            string cookieValue = "1";
            int cookieExpirationDays = 1;

            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(cookieExpirationDays),
                IsEssential = true
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, cookieValue, options);
        }

        public bool HasNewsCookie(int newsId)
        {
            string cookieName = $"news_{newsId}";
            return _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(cookieName);
        }
    }
}
