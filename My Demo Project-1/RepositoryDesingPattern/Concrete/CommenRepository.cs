using GameNews.Context;
using GameNews.Models;
using GameNews.RepositoryDesingPattern.Base;
using GameNews.RepositoryDesingPattern.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;
using System.Linq;

namespace GameNews.RepositoryDesingPattern.Concrete
{
    public class CommenRepository : RepositoryBase<Comment>,ICommentsDto
    {
        public CommenRepository(MyDbContext db) : base(db)
        {
        }

        public List<Comment> deneme()
        {
        return _table.Include(x => x.News).OrderByDescending(x => x.Date).Take(3).ToList();
        }
    }
}
