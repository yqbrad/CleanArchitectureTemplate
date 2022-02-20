using System.Collections.Generic;
using $safeprojectname$.Results;

namespace $safeprojectname$.Paging
{
    public class PageResult<TResult> : IResult
    {
        public IEnumerable<TResult> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long? TotalCount { get; set; }
    }
}