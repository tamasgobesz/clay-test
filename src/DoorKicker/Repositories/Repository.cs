using DoorKicker.Entities;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DoorKicker.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected DoorDbContext _dbContext;
        protected DbSet<T> _dbSet;

        public Repository(DoorDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.SingleOrDefault(e => e.Id == id);
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public T Get(int id)
        {
            return _dbSet.SingleOrDefault(x => x.Id == id);
        }

        public T GetIncluding(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.SingleOrDefault(x => x.Id == id);
        }

        public T Insert(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.SaveChanges();
        }
    }
}
