using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.Harbinger.Queries
{
    public class QueryDispatcher : IQueryDispatcher
    {
        protected readonly ILogger<QueryDispatcher> Logger;

        protected readonly Func<Type, IEnumerable<object>> ResolveCallback;

        public QueryDispatcher(IServiceProvider serviceProvider, ILogger<QueryDispatcher> logger)
            : this(type => new[] { serviceProvider.GetService(type) }, logger)
        {
        }

        public QueryDispatcher(Func<Type, IEnumerable<object>> resolveCallback, ILogger<QueryDispatcher> logger)
        {
            ResolveCallback = resolveCallback;
            Logger = logger;
        }

        public async Task ExecuteAsync<T, TQuery>(TQuery query)
            where TQuery : class, IQuery<T>
        {
            // Initialize context
            IQueryHandler<T, TQuery>[] QueryHandlers = null;
            try
            {
                QueryHandlers =
                        ResolveCallback(typeof(IQueryHandler<T, TQuery>))
                        .OfType<IQueryHandler<T, TQuery>>()
                        .ToArray();
            }
            catch (Exception ex)
            {
                Logger?.LogWarning(new EventId(), ex, "Unable to resolve handlers for {Query} Query", typeof(T).FullName);
                throw;
            }

            if (QueryHandlers?.Any() ?? false)
            {
                List<Task> QueryTasks = new List<Task>();

                Logger?.LogInformation("Processing {Query} Query", typeof(T).FullName);

                Stopwatch stopwatch = null;
                if (Logger != null)
                    stopwatch = Stopwatch.StartNew();

                // Execute Query
                foreach (IQueryHandler<T, TQuery> QueryHandler in QueryHandlers)
                {
                    Logger?.LogInformation("Running {Handler} to process {Query} Query", QueryHandler.GetType().FullName, typeof(T).FullName);
                    QueryTasks.Add(QueryHandler.ExecuteAsync(query));
                }

                await Task.WhenAll(QueryTasks.ToArray());

                // Dispose context
                foreach (IQueryHandler<T, TQuery> QueryHandler in QueryHandlers)
                {
                    QueryHandler.Dispose();
                }

                stopwatch?.Stop();
                Logger?.LogInformation("{Query} processed succesfully in {Duration}ms", typeof(T).FullName, stopwatch?.ElapsedMilliseconds);
            }
            else
            {
                Logger?.LogWarning("There is no registered handlers for {Query} Query", typeof(T).FullName);
                throw new ArgumentException("Unknown Query \"" + typeof(T).FullName + "\"", nameof(query));
            }
        }
    }
}
