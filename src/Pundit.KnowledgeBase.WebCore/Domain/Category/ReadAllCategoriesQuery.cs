using Pundit.Harbinger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class ReadAllCategoriesQuery : IQuery<ReadAllCategoriesQueryResult>
    {
        public ReadAllCategoriesQuery(int startIndex = 0, int length = 50)
        {
            StartIndex = startIndex;
            Length = length;
        }

        public int StartIndex { get; }
        public int Length { get; }

    }
}
