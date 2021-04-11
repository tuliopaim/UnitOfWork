using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UoW.Api.Domain.Filters
{
    public class PaginatedFilter : Filter
    {
        [Range(1, int.MaxValue)]
        public int? Page { get; set; }

        [Range(1, int.MaxValue)]
        public int? PageSize { get; set; }

        public bool ValidPagination => 
            Page.HasValue && PageSize.HasValue && Page.Value > 0 && PageSize.Value > 0;

        public int? Index => ValidPagination 
            ? (Page - 1) * PageSize 
            : 0;

        public IQueryable<T> ApplyToQuery<T>(IQueryable<T> query)
        {
            if (ValidPagination)
                query = query.Skip(Index.Value).Take(PageSize.Value);

            return query;
        }
    }
}