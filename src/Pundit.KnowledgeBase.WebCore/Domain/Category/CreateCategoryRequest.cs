using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class CreateCategoryRequest
    {
        public CreateCategoryRequest(string name, string icon, long? parentId)
        {
            Name = name;
            Icon = icon;
            ParentId = parentId;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public string Icon { get; }
        public long? ParentId { get; }
    }
}
