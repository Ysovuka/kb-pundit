using Pundit.Harbinger;
using System;
using System.Collections.Generic;

namespace Pundit.KnowledgeBase.WebCore.Domain
{
    public interface IEntity
    {
        IReadOnlyDictionary<Type, IDomainEvent> Events { get; }
    }
}
