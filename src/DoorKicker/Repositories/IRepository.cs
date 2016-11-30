using System;
using System.Linq;
using System.Linq.Expressions;

namespace DoorKicker.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T Get(int id);
        T GetIncluding(int id, params Expression<Func<T, object>>[] includeProperties);
        T Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
