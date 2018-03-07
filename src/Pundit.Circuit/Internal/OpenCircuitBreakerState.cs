using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pundit.Circuit.Internal
{
    internal class OpenCircuitBreakerState : ICircuitBreakerState
    {
        private readonly ICircuitBreakerInvoker _invoker;
        private readonly TimeSpan _resetTimeSpan;
        private readonly ICircuitBreakerSwitch _switch;

        public OpenCircuitBreakerState(
            ICircuitBreakerSwitch @switch,
            ICircuitBreakerInvoker invoker,
            TimeSpan resetTimeSpan)
        {
            _switch = @switch;
            _invoker = invoker;
            _resetTimeSpan = resetTimeSpan;
        }

        public double ServiceLevel { get { return 0; } }

        public void Enter()
        {
            _invoker.InvokeScheduled(() => _switch.AttemptToCloseCircuit(this), _resetTimeSpan);
        }

        public void InvocationFails()
        {
        }

        public void InvocationSucceeds()
        {
        }

        public void Invoke(Action action)
        {
            throw new CircuitBreakerOpenException();
        }

        public T Invoke<T>(Func<T> func)
        {
            throw new CircuitBreakerOpenException();
        }

        public Task InvokeAsync(Func<Task> func, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new CircuitBreakerOpenException();
        }

        public Task<T> InvokeAsync<T>(Func<Task<T>> func, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new CircuitBreakerOpenException();
        }
    }
}
