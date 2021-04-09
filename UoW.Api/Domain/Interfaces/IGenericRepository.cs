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
        void LogicRemove(T entity);
        void Update(T entity);

        Task<IEnumerable<T>> GetAsync(bool track = false);

        Task<T> GetByIdAsync(Guid id, bool track = false);

        Task<T> FirstAsync(Expression<Func<T, bool>> expression, bool track = false);

        Task<int> CountAsync(Expression<Func<T, bool>> expression);

        Task<List<T>> SearchAsync(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int? skip = null,
            int? take = null,
            bool track = false);
    }
}