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
        
        public async Task<Class> GetFullById(Guid id)
        {
            return await _context.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Class>> FilterAsync(
            long code,
            string name = null,
            int? year = null,
            string teacherName = null,
            bool complete = false,
            bool track = false)
        {
            var querie = _context.Classes
                .Where(c => c.Code == code);

            if (!string.IsNullOrWhiteSpace(name))
            {
                querie = querie.Where(c => c.Name == name);
            }

            if (year.HasValue)
            {
                querie = querie.Where(c => c.Year == year.Value);
            }

            if (!string.IsNullOrWhiteSpace(teacherName))
            {
                querie = querie.Where(c => c.TeacherName == teacherName);
            }

            if (complete)
            {
                querie = querie.Include(c => c.Students);
            }

            if (!track)
            {
                querie = querie.AsNoTracking();
            }

            return await querie.ToListAsync();
        }
    }
}