using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UoW.Api.Domain.Entities;
using UoW.Api.Domain.Filters;

namespace UoW.Api.Domain.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<IEnumerable<Student>> GetFullAsync(bool track = false);

        Task<Student> GetFullByIdAsync(Guid id, bool track = false);

        Task<IEnumerable<Student>> FilterAsync(StudentFilter filter, bool track = false);
    }
}