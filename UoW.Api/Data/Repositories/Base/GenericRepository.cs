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

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void LogicRemove(T entity)
        {
            entity.Remove();

            Update(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<IEnumerable<T>> Get(bool track = false)
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id, bool track = false)
        {
            var query = _dbSet.AsQueryable();

            if (!track)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {

            return await _dbSet.CountAsync(expression);
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> expression, bool track = false)
        {

            var query = _dbSet.AsQueryable();

            if (!track)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> SearchAsync(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int? skip = null,
            int? take = null,
            bool track = false)
        {
            var query = _dbSet.AsQueryable();

            if (!track)
                query = query.AsNoTracking();

            if (expression != null)
                query = query.Where(expression);

            if (include != null)
                query = include(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}