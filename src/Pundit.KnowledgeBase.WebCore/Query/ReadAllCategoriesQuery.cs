using Microsoft.EntityFrameworkCore;
using Pundit.Harbinger.Queries;
using Pundit.KnowledgeBase.WebCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Query
{
    public class ReadAllCategoriesQuery : Query<Category>
    {
        public ReadAllCategoriesQuery IncludeChildren()
        {
            SetQueryCriteria((context) =>
            {
                return from a in context.Set<Category>()
                       select a;
            });

            AddNavigationCriteria(query => query.Include(c => c.Children));

            return this;
        }
    }
}
