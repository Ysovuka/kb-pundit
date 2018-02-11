using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace System.Linq
{
    public static class IncludeExtension
    {
        public static IQueryable<T> Include<T>(this IQueryable<T> query, IEnumerable<IncludeParameter> includeParameters)
            where T : class
        {
            IQueryable<T> includedQuery = query;

            foreach(var parameter in includeParameters)
            {
                includedQuery = query.Include(parameter.Property);
            }

            return includedQuery;
        }
    }
}
