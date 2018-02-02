using Pundit.Harbinger;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class CreateCategoryEvent : IDomainEvent
    {
        public CreateCategoryEvent(Category category)
        {
            Category = category;
        }

        public Category Category { get; }
    }
}
