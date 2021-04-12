using System.Linq;
using UoW.Api.Domain.Entities;

namespace UoW.Api.Domain.Filters
{
    public class ClassFilter : PaginatedFilter
    {
        public long? Code { get; set; }
        public string Name { get; set; }
        public int? Year { get; set; }
        public string TeacherName { get; set; }

        public IQueryable<Class> ApplyToQuery(IQueryable<Class> query)
        {
            query = base.ApplyToQuery(query);

            if (Code.HasValue)
                query = query.Where(c => c.Code == Code);

            if (!string.IsNullOrWhiteSpace(Name))
                query = query.Where(c => c.Name == Name);

            if (Year.HasValue)
                query = query.Where(c => c.Year == Year);

            if (!string.IsNullOrWhiteSpace(TeacherName))
                query = query.Where(c => c.TeacherName == TeacherName);

            return query;
        }
    }
}