using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pundit.Circuit.Internal
{
    internal class ClosedCircuitBreakerState : ICircuitBreakerState
    {
        private readonly ICircuitBreakerInvoker _invoker;
        private readonly int _threshold;
        private readonly TimeSpan _timeout;
        private readonly ICircuitBreakerSwitch _switch;

        private int _failures;

        public ClosedCircuitBreakerState(
            ICircuitBreakerSwitch @switch,
            ICircuitBreakerInvoker invoker,
            int threshold,
            TimeSpan timeout)
        {
            _threshold = threshold;
            _timeout = timeout;
            _switch = @switch;
            _invoker = invoker;
        }

        public double ServiceLevel { get { return ((_threshold - (double)_failures) / _threshold) * 100; } }

        public void Enter()
        {
            _failures = 0;
        }

        public void InvocationFails()
        {
            if (Interlocked.Increment(ref _failures) == _threshold)
            {
                _switch.OpenCircuit(this);
            }
        }

        public void InvocationSucceeds()
        {
            _failures = 0;
        }

        public void Invoke(Action action)
        {
            _invoker.InvokeThrough(this, action, _timeout);
        }

        public T Invoke<T>(Func<T> func)
        {
            return _invoker.InvokeThrough(this, func, _timeout);
        }

        public async Task InvokeAsync(Func<Task> func, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _invoker.InvokeThroughAsync(this, func, _timeout, cancellationToken);
        }

        public async Task<T> InvokeAsync<T>(Func<Task<T>> func, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _invoker.InvokeThroughAsync(this, func, _timeout, cancellationToken);
        }
    }
}
