using Pundit.Harbinger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class ReadCategoryQuery : IQuery<ReadCategoryQueryResult>
    {
        public ReadCategoryQuery(long id)
        {
            CategoryId = id;
        }

        public long CategoryId { get; }
    }
}
