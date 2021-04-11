using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using UoW.Api.Data.Repositories.Base;
using UoW.Api.Domain.Entities;
using UoW.Api.Domain.Filters;
using UoW.Api.Domain.Interfaces;

namespace UoW.Api.Data.Repositories
{
    public class ClassRepository : GenericRepository<Class>, IClassRepository
    {
        private readonly ApplicationContext _context;
        public ClassRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Class> GetFullByIdAsync(Guid id, bool track = false)
        {
            return await FirstAsync(
                x => x.Id == id,
                FullClassQuery(),
                track);
        }

        public async Task<IEnumerable<Class>> GetFullAsync(bool track = false)
        {
            return await GetAsync(
                include: FullClassQuery(),
                track: track);
        }

        public async Task<IEnumerable<Class>> FilterAsync(
            ClassFilter filter,
            bool track = false)
        {
            var query = _context.Classes.OrderBy(x => x.Code).AsQueryable();

            if (!track) query = query.AsNoTracking();

            if (filter.FullObject)
                query = FullClassQuery().Invoke(query);

            query = filter.ApplyToQuery(query);

            return await query.ToListAsync();
        }

        private static Func<IQueryable<Class>, IIncludableQueryable<Class, object>> FullClassQuery()
        {
            return q => q.Include(s => s.Students);
        }
    }
}