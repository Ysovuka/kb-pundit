using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class UpdateCategoryRequest
    {
        public UpdateCategoryRequest(long id, string name, string icon)
        {
            Id = id;
            Name = name;
            Icon = icon;
        }

        public Guid RequestId { get; } = Guid.NewGuid();
        public long Id { get; }
        public string Name { get; }
        public string Icon { get; }
    }
}
