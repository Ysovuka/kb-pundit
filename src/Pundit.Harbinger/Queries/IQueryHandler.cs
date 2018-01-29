using System;
using System.Threading.Tasks;

namespace Pundit.Harbinger.Queries
{
    public interface IQueryHandler<T, TQuery> : IDisposable
        where TQuery : IQuery<T>
    {
        event EventHandler<QueryEventArgs> Executed;
        Task ExecuteAsync(TQuery query);
    }
}
