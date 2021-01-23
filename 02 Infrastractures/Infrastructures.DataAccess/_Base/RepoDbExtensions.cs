using System.Collections.Generic;
using System.Data;

namespace DDD.Infrastructure.DataAccess._Base
{
    public static class RepoDbExtensions
    {
        public static DataTable AsRepoDbTableValueParameter<T>(this IEnumerable<T> list)
        {
            var dt = new DataTable("dbo.ListInt");
            dt.Columns.Add("Id", typeof(int));
            foreach (var item in list)
                dt.Rows.Add(item);

            return dt;
        }
    }
}