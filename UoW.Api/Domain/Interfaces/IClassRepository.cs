using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UoW.Api.Domain.Entities;

namespace UoW.Api.Domain.Interfaces
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        Task<Class> GetFullById(Guid id);

        Task<IEnumerable<Class>> FilterAsync(
            long code,
            string name = null,
            int? year = null,
            string teacherName = null,
            bool complete = false,
            bool track = false);
    }
}