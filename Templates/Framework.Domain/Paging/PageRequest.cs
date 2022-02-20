using System.ComponentModel;
using $safeprojectname$.Requests;

namespace $safeprojectname$.Paging
{
    public class PageRequest<TResult> : IRequest<PageResult<TResult>>
    {
        [DefaultValue(1)]
        public int PageNumber { get; set; }

        [DefaultValue(10)]
        public int PageSize { get; set; }

        [DefaultValue(false)]
        public bool NeedTotalCount { get; set; }

        public string SortBy { get; set; }

        public bool SortAscending { get; set; }

        public int SkipCount => (PageNumber - 1) * PageSize;
    }
}