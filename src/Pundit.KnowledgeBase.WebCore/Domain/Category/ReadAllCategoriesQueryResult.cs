using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class ReadAllCategoriesQueryResult
    {
        public ReadAllCategoriesQueryResult(List<Category> categories, int startIndex, int length, int size)
        {
            Data = categories.ToList();
            StartIndex = startIndex;
            Length = length;
            Size = size;
        }

        public int StartIndex { get; }
        public int Length { get; }

        public int Size { get; }
        public IEnumerable<Category> Data { get; }
    }
}
