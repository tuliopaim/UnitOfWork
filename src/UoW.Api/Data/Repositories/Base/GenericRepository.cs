using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using UoW.Api.Domain.Entities;
using UoW.Api.Domain.Interfaces;

namespace UoW.Api.Data.Repositories.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Remove(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }
        
        public virtual void Update(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.CountAsync(expression);
        }

        public virtual async Task<T> GetByIdAsync(
            Guid id,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool track = false)
        {
            return await BuildQuery(include: include, track: track).FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public virtual async Task<T> FirstAsync(
            Expression<Func<T, bool>> expression,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool track = false)
        {
            return await BuildQuery(expression, include, track: track).FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetAsync(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = null,
            int? take = null,
            bool track = false)
        {
            return await BuildQuery(expression, include, orderBy, skip, take, track).ToListAsync();
        }

        protected IQueryable<T> BuildQuery(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = null,
            int? take = null,
            bool track = false)
        {
            var query = _dbSet.AsQueryable();

            if (!track) query = query.AsNoTracking();

            if (expression != null) query = query.Where(expression);

            if (include != null) query = include(query);

            query = orderBy != null ? orderBy(query) : query.OrderBy(x => x.CreatedAt);

            if (skip.HasValue) query = query.Skip(skip.Value);

            if (take.HasValue) query = query.Take(take.Value);

            return query;
        }
        
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}