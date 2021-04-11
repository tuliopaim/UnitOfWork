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

        public async Task<Student> GetFullByIdAsync(Guid id, bool track = false)
        {
            return await FirstAsync(
                x => x.Id == id,
                q => q.Include(s => s.Classes),
                track);
        }

        public async Task<IEnumerable<Student>> GetFullAsync(bool track = false)
        {
            return await GetAsync(
                include: q => q.Include(s => s.Classes),
                orderBy: q => q.OrderBy(s => s.CreatedAt),
                track: track);
        }

        public async Task<IEnumerable<Student>> FilterAsync(StudentFilter filter, bool track = false)
        {
            var query = _context.Students.OrderBy(s => s.CreatedAt).AsQueryable();

            if (!track) 
                query = query.AsNoTracking();
            
            if (filter.FullObject)
            {
                query = query.Include(s => s.Classes);
            }

            filter.HandleQuery(query);

            return await query.ToListAsync();
        }
    }
}