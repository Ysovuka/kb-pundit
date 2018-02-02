using System.Threading.Tasks;

namespace Pundit.Harbinger
{
    public interface IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        Task ExecuteAsync(TDomainEvent args);
    }
}
