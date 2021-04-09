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
            var query = _context.Students.AsQueryable();

            if (!track)
            {
                query = query.AsNoTracking();
            }

            return await query
                .Include(c => c.Classes)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> FilterAsync(
            string name,
            DateTime? from = null,
            DateTime? to = null,
            bool complete = false,
            bool track = false)
        {
            var querie = _context.Students
                .Where(s =>
                    s.Name == name);

            if (from.HasValue)
            {
                querie = querie.Where(s => s.BirthDate >= from.Value);
            }

            if (to.HasValue)
            {
                querie = querie.Where(s => s.BirthDate <= to.Value);
            }

            if (complete)
            {
                querie = querie.Include(s => s.Classes);
            }

            if (!track)
            {
                querie = querie.AsNoTracking();
            }

            return await querie.ToListAsync();
        }
    }
}