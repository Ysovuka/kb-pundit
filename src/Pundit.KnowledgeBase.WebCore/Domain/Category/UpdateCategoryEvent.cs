using Pundit.Harbinger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class UpdateCategoryEvent : IDomainEvent
    {
        public UpdateCategoryEvent(Category category)
        {
            Category = category;
        }

        public Category Category { get; }
    }
}
