namespace Pundit.Circuit.Internal
{
    internal interface ICircuitBreakerSwitch
    {
        void OpenCircuit(ICircuitBreakerState from);
        void AttemptToCloseCircuit(ICircuitBreakerState from);
        void CloseCircuit(ICircuitBreakerState from);
    }
}
