using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pundit.Circuit.Internal
{
    internal interface ICircuitBreakerState
    {
        double ServiceLevel { get; }

        void Enter();
        void InvocationFails();
        void InvocationSucceeds();
        void Invoke(Action action);
        T Invoke<T>(Func<T> func);
        Task InvokeAsync(Func<Task> func, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> InvokeAsync<T>(Func<Task<T>> func, CancellationToken cancellationToken = default(CancellationToken));
    }
}
