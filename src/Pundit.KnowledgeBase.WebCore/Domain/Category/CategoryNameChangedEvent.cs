using Pundit.Harbinger;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    internal class CategoryNameChangedEvent : IDomainEvent
    {
        public Category Category { get; }

        public CategoryNameChangedEvent(Category category)
        {
            Category = category;
        }
    }
}