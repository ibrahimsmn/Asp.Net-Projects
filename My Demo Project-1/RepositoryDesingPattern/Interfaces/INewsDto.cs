using GameNews.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameNews.RepositoryDesingPattern.Interfaces
{
    public interface INewsDto:IRepositoryBase<News>
    {
        Task<List<News>> GetNews(); 
        Task<List<NewsVM>> GetNewsByAuthor(string name);

        NewsVM GetNewsVM(int id);
        News GetNewsVMByComments(int id);
        List<NewsVM> GetNewsVMIndex();
        News GetInformation(int id);
        List<NewsVM> GetNewsLastComments();
        List<NewsVM> GetNewsMostViewed();
        List<NewsVM> GetNewsSliderComponent();
        Task<List<NewsVM>> GetNewsByCategoryName(string categoryname);
        
    }
}
