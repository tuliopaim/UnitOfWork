using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UoW.Api.Domain.Entities;
using UoW.Api.Domain.Filters;

namespace UoW.Api.Domain.Interfaces
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        Task<IEnumerable<Class>> GetFull(bool track = false);

        Task<Class> GetFullById(Guid id, bool track = false);

        Task<IEnumerable<Class>> FilterAsync(
            ClassFilter filter,
            bool track = false);
    }
}