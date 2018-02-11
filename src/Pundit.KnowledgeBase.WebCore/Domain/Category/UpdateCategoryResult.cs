using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class UpdateCategoryResult
    {
        public UpdateCategoryResult(Guid requestId, UpdateCategoryEvent e)
        {
            var category = e.Category;

            if (requestId == category.RequestId)
            {
                IsSuccessful = true;

                Id = e.Category.Id;
                Name = e.Category.Name;
                Icon = e.Category.Icon;
                ParentId = e.Category.ParentId;
            }
        }

        public long Id { get; private set; }

        public string Name { get; private set; }

        public string Icon { get; private set; }

        public long? ParentId { get; private set; }

        public bool IsSuccessful { get; } = false;
    }
}
