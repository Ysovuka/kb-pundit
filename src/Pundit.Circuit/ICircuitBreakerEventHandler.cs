namespace Pundit.Circuit
{
    public interface ICircuitBreakerEventHandler
    {
        void OnCircuitClosed(ICircuitBreaker circuitBreaker);
        void OnCircuitOpened(ICircuitBreaker circuitBreaker);
        void OnCircuitHalfOpened(ICircuitBreaker circuitBreaker);
    }
}
