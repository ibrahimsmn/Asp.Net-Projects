using GameNews.Context;
using GameNews.RepositoryDesingPattern.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameNews.RepositoryDesingPattern.Base
{
    public  class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        MyDbContext _db;

      protected  DbSet<T> _table;
        public RepositoryBase(MyDbContext db)
        {
            _db = db;
            _table = db.Set<T>();
        }

        private void Save()
        {
            _db.SaveChanges();
           
        }
        public async void Add(T entity)
        {
           await _table.AddAsync(entity);
            Save();
        }

        public void Delete(int id)
        {
            T item = GetById(id);
            _table.Remove(item);
            Save();
        }

        

        

        public void Update(T entity)
        {
            _table.Update(entity);
            Save();
        }

        public T GetSingle(Func<T, bool> metot)
        {
            return _table.FirstOrDefault(metot);
        }

        public T GetById(int id)
        {
            return _table.Find(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _table.ToListAsync();
          }

        public T GetByFilter(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression).FirstOrDefault();
        }
    }
}
