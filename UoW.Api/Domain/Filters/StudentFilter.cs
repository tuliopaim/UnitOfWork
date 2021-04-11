using System;
using System.Linq;
using UoW.Api.Domain.Entities;

namespace UoW.Api.Domain.Filters
{
    public class StudentFilter : PaginatedFilter
    {
        public string Name { get; set; }
        public DateTime? BirthDateFrom { get; set; }
        public DateTime? BirthDateTo { get; set; }

        public IQueryable<Student> ApplyToQuery(IQueryable<Student> query)
        {
            query = base.ApplyToQuery(query);

            if (!string.IsNullOrWhiteSpace(Name))
                query = query.Where(s => s.Name == Name);

            if (BirthDateFrom.HasValue)
                query = query.Where(s => s.BirthDate >= BirthDateFrom);

            if (BirthDateTo.HasValue)
                query = query.Where(s => s.BirthDate <= BirthDateTo.Value);
            
            return query;
        }
    }
}