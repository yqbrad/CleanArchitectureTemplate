using System.Collections.Generic;

namespace Framework.Domain.Paging
{
    public class PageQuery<TResult> : IPageQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int SkipCount => (PageNumber - 1) * PageSize;
        public bool NeedTotalCount { get; set; }
        public string SortBy { get; set; }
        public bool SortAscending { get; set; }

        public IEnumerable<TResult> Data { get; set; }
    }
}