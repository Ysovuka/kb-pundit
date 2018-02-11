using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace System.Linq
{
    public static class SortExtension
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, IEnumerable<SortParameter> sortParameters)
            where T : class
        {
            IQueryable<T> orderedFiltered = null;
            var orderCount = 0;
            foreach(var parameter in sortParameters)
            {
                if (!string.IsNullOrEmpty(parameter.Property))
                {
                    if (orderCount == 0)
                        orderedFiltered = query.Sort(parameter.Property, parameter.Order.OrderBy());
                    else
                        orderedFiltered = orderedFiltered.Sort(parameter.Property, parameter.Order.ThenBy());
                }

                orderCount++;
            }

            return orderedFiltered ?? query;
        }
        
        private static IQueryable<T> Sort<T>(this IQueryable<T> query, string propertyName, string sortOrder)
            where T : class
        {
            var queryElementTypeParam = Expression.Parameter(typeof(T));
            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, propertyName);
            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var sort = Expression.Call(
                typeof(Queryable),
                sortOrder,
                new Type[] { typeof(T), memberAccess.Type },
                query.Expression,
                Expression.Quote(keySelector));

            return query.Provider.CreateQuery<T>(sort);
        }
    }
}
