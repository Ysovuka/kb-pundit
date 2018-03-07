using Microsoft.AspNetCore.Mvc;
using Pundit.Harbinger;
using Pundit.Interlude;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class ReadAllCategoriesQuery : IQuery<ReadAllCategoriesQueryResult>
    {
        public ReadAllCategoriesQuery() { }

        [FromQuery]
        public PagingOptions PagingOptions { get; private set; } = new PagingOptions();

        [FromQuery]
        public string Include { get; private set; }

        [FromQuery]
        public string Filter { get; private set; }

        [FromQuery]
        public string Sort { get; private set; }

    }
}
