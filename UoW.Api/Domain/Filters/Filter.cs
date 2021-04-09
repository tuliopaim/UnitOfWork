using System.ComponentModel.DataAnnotations;

namespace UoW.Api.Domain.Filters
{
    public class PaginatedFilter
    {
        [Range(1, int.MaxValue)]
        public int? Page { get; set; }

        [Range(1, int.MaxValue)]
        public int? PageSize { get; set; }

        public int? Index 
        { 
            get
            {
                if (!Page.HasValue || !PageSize.HasValue) 
                    return null;

                return (Page - 1) * PageSize;
            }
        }

    }
}