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

        public async Task<Student> GetFullById(Guid id)
        {
            return await _context.Students
                .Include(s => s.Classes)
                .FirstOrDefaultAsync(s => s.Id == id);
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