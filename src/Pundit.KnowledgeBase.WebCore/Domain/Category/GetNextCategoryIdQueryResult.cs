namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class GetNextCategoryIdQueryResult
    {
        public GetNextCategoryIdQueryResult(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
