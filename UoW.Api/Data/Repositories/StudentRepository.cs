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
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly ApplicationContext _context;

        public StudentRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Student> GetFullById(Guid id, bool track = false)
        {
            var query = _context.Students.AsQueryable();

            if (!track)
            {
                query = query.AsNoTracking();
            }

            return await query
                .Include(c => c.Classes)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Student>> GetFull(bool track = false)
        {
            var query = _context.Students
                .OrderByDescending(x => x.CreatedAt)
                .AsQueryable();

            if (!track)
            {
                query = query.AsNoTracking();
            }

            return await query
                .Include(c => c.Classes)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> FilterAsync(
            StudentFilter filter,
            bool track = false)
        {
            var query = _context.Students
                .OrderByDescending(x => x.CreatedAt)
                .AsQueryable();

            if (!track)
            {
                query = query.AsNoTracking();
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(s =>s.Name == filter.Name);
            }

            if (filter.BirthDateFrom.HasValue)
            {
                query = query.Where(s => s.BirthDate >= filter.BirthDateFrom);
            }

            if (filter.BirthDateTo.HasValue)
            {
                query = query.Where(s => s.BirthDate <= filter.BirthDateTo.Value);
            }

            if (filter.Complete)
            {
                query = query.Include(s => s.Classes);
            }

            if (filter.Index.HasValue)
            {
                query = query.Skip(filter.Index.Value).Take(filter.PageSize.Value);
            }
            
            return await query.ToListAsync();
        }
    }
}