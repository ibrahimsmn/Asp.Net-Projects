using GameNews.Context;
using GameNews.Models;
using GameNews.RepositoryDesingPattern.Base;
using GameNews.RepositoryDesingPattern.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GameNews.RepositoryDesingPattern.Concrete
{
    public class NewsRepository : RepositoryBase<News>, INewsDto
    {
        readonly UserManager<AppUser> _userManager;

        public NewsRepository(MyDbContext db, UserManager<AppUser> userManager) : base(db)
        {
            _userManager = userManager;
        }

        public News GetInformation(int id)
        {
            return _table.Where(x => x.Id == id).Include(x => x.Category).FirstOrDefault();
        }

        

        public async Task<List<News>> GetNews()
        {
            return await _table.Include(x => x.Category).Include(x => x.AppUser).ToListAsync();
        }
        public async Task<List<NewsVM>> GetNewsByAuthor(string name)
        {
            AppUser user = await _userManager.FindByNameAsync(name);

            return await _table.Where(x => x.AppUserID == user.Id).Select(x => new NewsVM
            {
                Content = x.Content,
                Description = x.Description,
                Title = x.Title,
                Source = x.Source,
                Category = x.Category.Name,
                ID=x.Id
                

            }).ToListAsync();
        }

        public async Task<List<NewsVM>> GetNewsByCategoryName(string categoryname)
        {
            return await _table.Where(x => x.Category.Name == categoryname).Select(x => new NewsVM
            {

                ImageURLL = x.ImageUrl,
                Title = x.Title,
                Views = x.Views,
                Category = x.Category.Name,
                ID=x.Id


            }).ToListAsync();
        }

        public List<NewsVM> GetNewsLastComments()
        {
            return _table.OrderByDescending(x => x.Comments.Max(x => x.Date)).Take(3).Select(x => new NewsVM
            {
                Title = x.Title,
                ImageURLL = x.ImageUrl,
                ID =x.Id
            }).ToList();
        }

        public List<NewsVM> GetNewsMostViewed()
        {
            return _table.OrderByDescending(x=> x.Views).Take(3).Select(x=> new NewsVM
            {
                Title = x.Title,
                ImageURLL = x.ImageUrl,
                ID = x.Id,
                Views = x.Views
            }).ToList();
        }

        public List<NewsVM> GetNewsSliderComponent()
        {
            return _table.OrderByDescending(x => x.PublishedAt).Select(x=> new NewsVM
            {
                ImageURLL= x.ImageUrl,
                Title=x.Title,
                ID=x.Id,

            }).Take(3).ToList();
        }

        public NewsVM GetNewsVM(int id)
        {
            return _table.Where(x => x.Id == id).Select(x => new NewsVM
            {
                Category = x.Category.Name,
                Content=x.Content,
                Description=x.Description,
                Title=x.Title,
                ImageURLL=x.ImageUrl,
                Source=x.Source,
                


            }).FirstOrDefault();
        }

        public News GetNewsVMByComments(int id)
        {
            return _table.Where(x => x.Id == id).Include(x => x.Comments).Include(x=> x.AppUser).Include(x=> x.Category).FirstOrDefault();
        }

        public List<NewsVM> GetNewsVMIndex()
        {
            return  _table.Include(x => x.Category).Include(x => x.AppUser).Select(x => new NewsVM
            {
                
                Title = x.Title,
                ImageURLL = x.ImageUrl,
                ID = x.Id,
                dateTime=x.PublishedAt,
                
                
            }).OrderByDescending(x=> x.dateTime).Skip(3).ToList();


        }
        
       
    }
}
