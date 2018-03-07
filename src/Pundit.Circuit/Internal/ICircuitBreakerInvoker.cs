using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pundit.Circuit.Internal
{
    internal interface ICircuitBreakerInvoker
    {
        void InvokeScheduled(Action action, TimeSpan interval);
        void InvokeThrough(ICircuitBreakerState state, Action action, TimeSpan timeout);
        T InvokeThrough<T>(ICircuitBreakerState state, Func<T> func, TimeSpan timeout);
        Task InvokeThroughAsync(ICircuitBreakerState state, Func<Task> func, TimeSpan timeout, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> InvokeThroughAsync<T>(ICircuitBreakerState state, Func<Task<T>> func, TimeSpan timeout, CancellationToken cancellationToken = default(CancellationToken));
    }
}
