using System.Linq.Expressions;

namespace WebAPI.Application.Extensions;

public static class QueryExtension
{
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> query,
        bool condition,
        Expression<Func<T, bool>> predicate )
    {
        if ( condition )
        {
            return query.Where( predicate );
        }

        return query;
    }
}