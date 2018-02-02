using System.Threading.Tasks;

namespace Pundit.Harbinger
{
    public abstract class DomainEventHandler<TDomainEvent> :
        IDomainEventHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        public abstract Task ExecuteAsync(TDomainEvent args);
    }
}
