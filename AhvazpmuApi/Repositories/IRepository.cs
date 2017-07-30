using System;
using System.Linq;
using AhvazpmuApi.Entities;
using System.Linq.Expressions;

namespace AhvazpmuApi.Repositories
{
    public interface IRepository<T> where T:class
    {
        void Add(T item);
        void Delete(T item);
        IQueryable<T> GetAll();
        T GetSingle(Expression<Func<T, bool>> predicate);
        bool Save();
        void Update(T item);
    }
}