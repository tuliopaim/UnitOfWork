namespace UoW.Api.Domain.Filters
{
    public class ClassFilter : PaginatedFilter
    {
        public long? Code { get; set; }
        public string Name { get; set; }
        public int? Year { get; set; }
        public string TeacherName { get; set; }
        public bool Complete { get; set; }
    }
}