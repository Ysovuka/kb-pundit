using Microsoft.EntityFrameworkCore;
using Pundit.Harbinger;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class CategoryQueryHandler :
        IQueryHandler<GetNextCategoryIdQuery, GetNextCategoryIdQueryResult>,
        IQueryHandler<ReadAllCategoriesQuery, ReadAllCategoriesQueryResult>,
        IQueryHandler<ReadCategoryQuery, ReadCategoryQueryResult>
    {
        private readonly DatabaseContext _dbContext;
        public CategoryQueryHandler(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
        }

        public async Task<ReadAllCategoriesQueryResult> ExecuteAsync(ReadAllCategoriesQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var categories = await _dbContext.Set<Category>()
                                    .OrderBy(c => c.Id)
                                    .Skip(query.PagingOptions.Offset.Value)
                                    .Take(query.PagingOptions.Limit.Value)
                                    .ToListAsync(cancellationToken);

            var count = await _dbContext.Set<Category>().CountAsync(cancellationToken);

            return new ReadAllCategoriesQueryResult(categories, query.PagingOptions.Offset.Value, query.PagingOptions.Limit.Value, count);
        }

        public async Task<ReadCategoryQueryResult> ExecuteAsync(ReadCategoryQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var category = await _dbContext.Set<Category>().FirstOrDefaultAsync(c => c.Id == query.CategoryId, cancellationToken);

            Guard.Assert(() => category == null, new ArgumentNullException("Category", $"Category [{query.CategoryId}] was not found."));

            return new ReadCategoryQueryResult(category.Id, category.Name, category.Icon, category.ParentId);
        }

        public async Task<GetNextCategoryIdQueryResult> ExecuteAsync(GetNextCategoryIdQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            long id = await _dbContext.Set<Category>().CountAsync(cancellationToken) + 1;

            return new GetNextCategoryIdQueryResult(id);
        }
    }
}
