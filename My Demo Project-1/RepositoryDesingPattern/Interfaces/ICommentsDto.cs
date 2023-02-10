using GameNews.Models;
using System.Collections.Generic;

namespace GameNews.RepositoryDesingPattern.Interfaces
{
    public interface ICommentsDto:IRepositoryBase<Comment>
    {
        public List<Comment> deneme();
    }
}
