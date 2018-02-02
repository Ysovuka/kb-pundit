using Pundit.Harbinger;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    internal class CategoryIdChangedEvent : IDomainEvent
    {
        public Category Category { get; }

        public CategoryIdChangedEvent(Category category)
        {
            Category = category;
        }
    }
}