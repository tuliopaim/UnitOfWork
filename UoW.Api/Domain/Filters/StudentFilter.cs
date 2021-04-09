using System;

namespace UoW.Api.Domain.Filters
{
    public class StudentFilter : PaginatedFilter
    {
        public string Name { get; set; }

        public DateTime? BirthDateFrom { get; set; }
        public DateTime? BirthDateTo { get; set; }
        public bool Complete { get; set; }
    }
}