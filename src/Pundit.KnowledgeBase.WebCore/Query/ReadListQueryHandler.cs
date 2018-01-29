using Microsoft.EntityFrameworkCore;
using Pundit.Harbinger.Queries;
using Pundit.KnowledgeBase.WebCore.Data;
using Pundit.KnowledgeBase.WebCore.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Query
{
    public class ReadListQueryHandler<T, TQuery> : QueryHandler<T, TQuery, DatabaseRepository>
        where TQuery : class, IQuery<T>
        where T : class
    {
        public ReadListQueryHandler(DatabaseRepository dbContext)
            : base(dbContext)
        {
        }

        public override async Task ExecuteAsync(TQuery query)
        {
            var linqQuery = query.AsQuery();

            var dbQuery = (linqQuery != null) 
                            ? linqQuery(_dbContext) 
                            : _dbContext.Set<T>().AsQueryable();

            var navigatedQuery = query.ExecuteNavigation(dbQuery);            

            IEnumerable<T> results = null;

            results = await navigatedQuery.ToListAsync();

            OnExecuted(new QueryEventArgs(results));
        }
    }
}
