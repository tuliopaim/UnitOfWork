using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UoW.Api.Domain.Entities;

namespace UoW.Api.Domain.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<IEnumerable<Student>> GetFull(bool track = false);

        Task<Student> GetFullById(Guid id, bool track = false);

        Task<IEnumerable<Student>> FilterAsync(
            string name,
            DateTime? from = null,
            DateTime? to = null,
            bool complete = false,
            bool track = false);
    }
}