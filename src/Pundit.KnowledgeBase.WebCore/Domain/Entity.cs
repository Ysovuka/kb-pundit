using Pundit.Harbinger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Domain
{
    public abstract class Entity : IEntity
    {
        protected void Raise<TDomainEvent>(TDomainEvent e)
            where TDomainEvent : IDomainEvent
        {
            _events[e.GetType()] = e;
        }

        public void ClearEvents()
        {
            _events.Clear();
        }

        private IDictionary<Type, IDomainEvent> _events = new Dictionary<Type, IDomainEvent>();
        public IReadOnlyDictionary<Type, IDomainEvent> Events => _events as IReadOnlyDictionary<Type, IDomainEvent>;
    }
}
