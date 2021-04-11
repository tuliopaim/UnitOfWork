using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace UoW.Api.Domain.Interfaces
{
    public interface IGenericRepository<T> : IDisposable
    {
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        
        Task<T> GetByIdAsync(Guid id, bool track = false);

        Task<T> FirstAsync(
            Expression<Func<T, bool>> expression,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool track = false);

        Task<int> CountAsync(Expression<Func<T, bool>> expression);

        Task<List<T>> GetAsync(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = null,
            int? take = null,
            bool track = false);
    }
}