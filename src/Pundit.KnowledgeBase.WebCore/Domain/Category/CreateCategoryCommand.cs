using Pundit.Harbinger;
using System;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class CreateCategoryCommand : ICommand
    {
        public Guid RequestId { get; }

        public long Id { get; }
        public string Name { get; }
        public string Icon { get; }
        public long? ParentId { get; }

        public CreateCategoryCommand(Guid requestId, long id, string name, string icon, long? parentId)
        {
            RequestId = requestId;

            Id = id;
            Name = name;
            Icon = icon;
            ParentId = parentId;
        }
    }
}
