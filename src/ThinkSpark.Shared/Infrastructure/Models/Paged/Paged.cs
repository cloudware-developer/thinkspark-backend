using ThinkSpark.Shared.Infrastructure.Models.Enums;

namespace ThinkSpark.Shared.Infrastructure.Models.Paged
{
    public class Paged<TEntity>
    {
        public int CurrentPage { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 10;
        public string FirstPageUrl { get; set; } = string.Empty;
        public string LastPageUrl { get; set; } = string.Empty;
        public string PreviousPageUrl { get; set; } = string.Empty;
        public string NextPageUrl { get; set; } = string.Empty;
        public string CurrentPageUrl { get; set; } = string.Empty;
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public string OrderByColumn { get; set; } = string.Empty;
        public OrderByDirectionEnum OrderByDirection { get; set; } = OrderByDirectionEnum.Asc;
        public TEntity? Collection { get; set; }
    }
}
