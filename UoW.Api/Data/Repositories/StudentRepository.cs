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
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly ApplicationContext _context;

        public StudentRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Student> GetFullByIdAsync(Guid id, bool track = false)
        {
            return await GetByIdAsync(id, FullStudentQuery(), track);
        }

        public async Task<IEnumerable<Student>> GetFullAsync(bool track = false)
        {
            return await GetAsync(
                include: FullStudentQuery(),
                orderBy: q => q.OrderBy(s => s.CreatedAt),
                track: track);
        }

        public async Task<IEnumerable<Student>> FilterAsync(StudentFilter filter, bool track = false)
        {
            var query = _context.Students.OrderBy(s => s.CreatedAt).AsQueryable();
            
            if (!track) query = query.AsNoTracking();

            if (filter.FullObject) query = FullStudentQuery().Invoke(query);

            query = filter.ApplyToQuery(query);

            return await query.ToListAsync();
        }
        
        private static Func<IQueryable<Student>, IIncludableQueryable<Student, object>> FullStudentQuery()
        {
            return q => q.Include(s => s.Classes);
        }
    }
}