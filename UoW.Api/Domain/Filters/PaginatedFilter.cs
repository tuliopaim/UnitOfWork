using System.ComponentModel.DataAnnotations;

namespace UoW.Api.Domain.Filters
{
    public class PaginatedFilter
    {
        [Range(1, int.MaxValue)]
        public int? Page { get; set; }

        [Range(1, int.MaxValue)]
        public int? PageSize { get; set; }

        public bool ValidPagination => 
            Page.HasValue && PageSize.HasValue && Page.Value > 1 && PageSize.Value > 0;

        public int? Index => ValidPagination 
            ? (Page - 1) * PageSize 
            : 0;
    }
}