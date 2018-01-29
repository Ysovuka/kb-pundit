using Microsoft.EntityFrameworkCore;
using Pundit.Harbinger.Queries;
using Pundit.KnowledgeBase.WebCore.Data;
using System;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Query
{
    public class ReadObjectQueryHandler<T, TQuery> : QueryHandler<T, TQuery, DatabaseRepository>
        where TQuery : class, IQuery<T>
        where T : class
    {
        public ReadObjectQueryHandler(DatabaseRepository dbContext)
            : base(dbContext)
        {
        }

        public override async Task ExecuteAsync(TQuery query)
        {
            var navigatedQuery = query.ExecuteNavigation(_dbContext.Set<T>());

            var predicate = query.AsExpression();

            T results = null;

            if (predicate != null)
            {
                results = await navigatedQuery.FirstOrDefaultAsync(predicate);
            }
            else
            {
                results = await navigatedQuery.FirstOrDefaultAsync(c => true);
            }

            OnExecuted(new QueryEventArgs(results));
        }
    }
}
