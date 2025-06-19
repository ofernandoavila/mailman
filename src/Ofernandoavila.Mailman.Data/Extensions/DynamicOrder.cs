using System.Linq.Expressions;

namespace Ofernandoavila.Mailman.Data.Extensions;

public static class DynamicOrder
{
    public static IOrderedQueryable<TSource> OrderByCustom<TSource, TKey>(this IQueryable<TSource> source,
                                                                            Expression<Func<TSource, TKey>> keySelector,
                                                                            bool descending = false)
    {
        if(descending)
            return source.OrderByDescending(keySelector);

        return source.OrderBy(keySelector);
    }
}