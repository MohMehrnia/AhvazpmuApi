using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AhvazpmuApi.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using AhvazpmuApi.QueryParameters;

namespace AhvazpmuApi.Repositories
{
    public class Repository<T , C> : IRepository<T> where T : class, new() where C:DbContext
    {
        private C _context; 
        public Repository(C context)
        {
            _context = context;
        }
        public virtual void Add(T item)
        {
            _context.Set<T>().Add(item);
        }
        public virtual void Delete(T item)
        {
            _context.Set<T>().Remove(item);
        }
        public virtual IQueryable<T> GetAll<TOrderBy>(NewsQueryParameters newsQueryParameters, 
                                                      Expression<Func<T, TOrderBy>> orderBy, 
                                                      Expression<Func<T, bool>> predice)
        {
            return _context.Set<T>()
                .Where(predice)
                .OrderByDescending(orderBy)
                .Skip(newsQueryParameters.PageCount * (newsQueryParameters.Page - 1))
                .Take(newsQueryParameters.PageCount);
        }
        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }
        public virtual bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
        public virtual void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public virtual int Count()
        {
            return _context.Set<T>().Count();
        }
    }
}
