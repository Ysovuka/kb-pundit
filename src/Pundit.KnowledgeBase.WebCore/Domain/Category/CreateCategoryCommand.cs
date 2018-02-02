using Pundit.Harbinger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class CreateCategoryCommand : ICommand
    {
        public Guid RequestId { get; }

        public string Name { get; }
        public string Icon { get; }
        public long? ParentId { get; }

        public CreateCategoryCommand(Guid requestId, string name, string icon, long? parentId)
        {
            RequestId = requestId;
            Name = name;
            Icon = icon;
            ParentId = parentId;
        }
    }
}
