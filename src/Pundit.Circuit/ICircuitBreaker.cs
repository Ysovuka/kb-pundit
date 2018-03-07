using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pundit.Circuit
{
    public interface ICircuitBreaker
    {
        double ServiceLevel { get; }

        void Execute(Action action);
        T Execute<T>(Func<T> func);
        Task ExecuteAsync(Func<Task> func, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> ExecuteAsync<T>(Func<Task<T>> func, CancellationToken cancellationToken = default(CancellationToken));
    }
}
