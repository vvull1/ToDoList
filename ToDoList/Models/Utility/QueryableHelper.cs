using System.Linq.Expressions;

namespace ToDoList.Models.Utility
{
    public static class QueryableHelper
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, int, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }


        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            if (condition)
                return query.Where(predicate);
            else
                return query;
        }
    }
}
