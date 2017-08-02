using System;
using System.Linq;
using AhvazpmuApi.Entities;
using System.Linq.Expressions;
using AhvazpmuApi.QueryParameters;

namespace AhvazpmuApi.Repositories
{
    public interface IRepository<T> where T:class
    {
        void Add(T item);
        void Delete(T item);
        IQueryable<T> GetAll<TOrderBy>(NewsQueryParameters newsQueryParameters, Expression<Func<T, TOrderBy>> orderBy);
        T GetSingle(Expression<Func<T, bool>> predicate);
        bool Save();
        int Count();
        void Update(T item);
    }
}