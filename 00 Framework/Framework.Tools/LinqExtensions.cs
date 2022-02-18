using System;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Tools
{
    public static class LinqExtensions
    {
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> query, string sortBy, bool ascending)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, sortBy);
            var exp = Expression.Lambda(prop, param);
            var method = ascending ? "OrderBy" : "OrderByDescending";
            var types = new[]
            {
                query.ElementType,
                exp.Body.Type
            };
            var mce = Expression.Call(typeof(Queryable), method, types, param);
            return query.Provider.CreateQuery<T>(mce);
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition,
            Expression<Func<T, bool>> predicate)
            => condition ? query.Where(predicate) : query;

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition,
            Expression<Func<T, int, bool>> predicate)
            => condition ? query.Where(predicate) : query;
    }
}