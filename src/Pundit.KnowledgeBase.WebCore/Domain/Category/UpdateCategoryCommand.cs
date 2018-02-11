using Pundit.Harbinger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class UpdateCategoryCommand : ICommand
    {
        public Guid RequestId { get; }

        public long Id { get; }
        public string Name { get; }
        public string Icon { get; }

        public UpdateCategoryCommand(Guid requestId, long id, string name, string icon)
        {
            RequestId = requestId;

            Id = id;
            Name = name;
            Icon = icon;
        }
    }
}
