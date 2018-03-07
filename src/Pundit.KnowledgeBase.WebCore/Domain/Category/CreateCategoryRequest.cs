using System;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class CreateCategoryRequest
    {
        public CreateCategoryRequest(long id, string name, string icon, long? parentId)
        {
            Id = id;
            Name = name;
            Icon = icon;
            ParentId = parentId;
        }

        public Guid RequestId { get; } = Guid.NewGuid();

        public long Id { get; }
        public string Name { get; }
        public string Icon { get; }
        public long? ParentId { get; }

        public CreateCategoryCommand GetCommand()
        {
            return new CreateCategoryCommand(RequestId, Id, Name, Icon, ParentId);
        }
    }
}
