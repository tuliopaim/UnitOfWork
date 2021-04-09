using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UoW.Api.Domain.Entities;
using UoW.Api.Domain.Filters;

namespace UoW.Api.Domain.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<IEnumerable<Student>> GetFull(bool track = false);

        Task<Student> GetFullById(Guid id, bool track = false);

        Task<IEnumerable<Student>> FilterAsync(StudentFilter filter, bool track = false);
    }
}