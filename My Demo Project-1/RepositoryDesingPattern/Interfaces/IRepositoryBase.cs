using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameNews.RepositoryDesingPattern.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<List<T>> GetAll();

        
        T GetByFilter(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);

        T GetSingle(Func<T, bool> metot);
        T GetById(int id);
    }
}
