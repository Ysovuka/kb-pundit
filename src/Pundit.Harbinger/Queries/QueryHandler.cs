using System;
using System.Threading.Tasks;

namespace Pundit.Harbinger.Queries
{
    public abstract class QueryHandler<T, TQuery, TContext> : IQueryHandler<T, TQuery>
        where TQuery : IQuery<T>
        where TContext : class
    {
        protected readonly TContext _dbContext;
        public QueryHandler(TContext dbContext)
        {
            Guard.Assert(() => dbContext == null, new ArgumentNullException("DbContext", "DbContext cannot be null."));
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            (_dbContext as IDisposable)?.Dispose();

            GC.SuppressFinalize(this);
        }

        public event EventHandler<QueryEventArgs> Executed;

        protected void OnExecuted(QueryEventArgs e)
        {
            Guard.Assert(() => e == null, new ArgumentNullException("EventArgs", "EventArgs cannot be null."));

            if (Executed != null)
                Executed.Invoke(this, e);
        }

        public abstract Task ExecuteAsync(TQuery query);
    }
}
