using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Class> GetFullById(Guid id, bool track = false)
        {
            var list = await SearchAsync(
                x => x.Id == id,
                q => q.Include(s => s.Students),
                track: track);

            return list?.FirstOrDefault();
        }

        public async Task<IEnumerable<Class>> GetFull(bool track = false)
        {
            return await SearchAsync(
                include: q => q.Include(s => s.Students),
                track: track);
        }

        public async Task<IEnumerable<Class>> FilterAsync(
            ClassFilter filter,
            bool track = false)
        {
            var query = _context.Classes
                .OrderByDescending(x => x.CreatedAt)
                .AsQueryable();

            if (!track)
            {
                query = query.AsNoTracking();
            }

            if (filter.Code.HasValue)
            {
                query = query.Where(c => c.Code == filter.Code);
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(c => c.Name == filter.Name);
            }

            if (filter.Year.HasValue)
            {
                query = query.Where(c => c.Year == filter.Year);
            }

            if (!string.IsNullOrWhiteSpace(filter.TeacherName))
            {
                query = query.Where(c => c.TeacherName == filter.TeacherName);
            }

            if (filter.Complete)
            {
                query = query.Include(c => c.Students);
            }

            if (filter.Index.HasValue)
            {
                query = query.Skip(filter.Index.Value).Take(filter.PageSize.Value);
            }

            return await query.ToListAsync();
        }
    }
}