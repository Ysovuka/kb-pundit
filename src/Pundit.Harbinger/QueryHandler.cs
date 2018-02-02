using System;
using System.Threading.Tasks;

namespace Pundit.Harbinger
{
    public abstract class QueryHandler<TContext, TQuery, TResult> :
            IQueryHandler<TQuery, TResult>
            where TContext : class
            where TQuery : IQuery<TResult>
    {
        protected readonly TContext _dbContext;
        public QueryHandler(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            (_dbContext as IDisposable)?.Dispose();

            GC.SuppressFinalize(this);
        }

        public abstract Task<TResult> ExecuteAsync(TQuery query);
    }
}
