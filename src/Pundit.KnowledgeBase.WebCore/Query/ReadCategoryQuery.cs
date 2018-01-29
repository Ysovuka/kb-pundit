using Pundit.Harbinger.Queries;
using Pundit.KnowledgeBase.WebCore.Data;

namespace Pundit.KnowledgeBase.WebCore.Query
{
    public class ReadCategoryQuery : Query<Category>
    {
        public ReadCategoryQuery ReadById(long id)
        {
            AddExpressionCriteria(c => c.Id == id);

            return this;
        }
    }
}
