using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UoW.Api.Data.Repositories.Base;
using UoW.Api.Domain.Entities;
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
            var query = _context.Classes.AsQueryable();

            if (!track)
            {
                query = query.AsNoTracking();
            }

            return await query
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Class>> GetFull(bool track = false)
        {
            var query = _context.Classes.AsQueryable();

            if (!track)
            {
                query = query.AsNoTracking();
            }

            return await query
                .Include(c => c.Students)
                .ToListAsync();
        }

        public async Task<IEnumerable<Class>> FilterAsync(
            long code,
            string name = null,
            int? year = null,
            string teacherName = null,
            bool complete = false,
            bool track = false)
        {
            var query = _context.Classes
                .Where(c => c.Code == code);

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(c => c.Name == name);
            }

            if (year.HasValue)
            {
                query = query.Where(c => c.Year == year.Value);
            }

            if (!string.IsNullOrWhiteSpace(teacherName))
            {
                query = query.Where(c => c.TeacherName == teacherName);
            }

            if (complete)
            {
                query = query.Include(c => c.Students);
            }

            if (!track)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }
    }
}