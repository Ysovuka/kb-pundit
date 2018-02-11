using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class ReadCategoryQueryResult
    {
        public ReadCategoryQueryResult(long id, string name, string icon, long? parentId)
        {
            Id = id;
            Name = name;
            Icon = icon;
            ParentId = parentId;
        }

        public long Id { get; private set; }

        public string Name { get; private set; }

        public string Icon { get; private set; }

        public long? ParentId { get; private set; }
    }
}
