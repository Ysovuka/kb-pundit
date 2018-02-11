using Microsoft.EntityFrameworkCore;
using Pundit.Harbinger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class CategoryQueryHandler : 
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

        public async Task<ReadAllCategoriesQueryResult> ExecuteAsync(ReadAllCategoriesQuery query)
        {
            var categories = await _dbContext.Set<Category>()
                                    .OrderBy(c => c.Id)
                                    .Skip(query.StartIndex)
                                    .Take(query.Length)
                                    .ToListAsync();

            var count = await _dbContext.Set<Category>().CountAsync();

            return new ReadAllCategoriesQueryResult(categories, query.StartIndex, query.Length, count);
        }

        public async Task<ReadCategoryQueryResult> ExecuteAsync(ReadCategoryQuery query)
        {
            var category = await _dbContext.Set<Category>().FirstOrDefaultAsync(c => c.Id == query.CategoryId);

            Guard.Assert(() => category == null, new ArgumentNullException("Category", $"Category [{query.CategoryId}] was not found."));

            return new ReadCategoryQueryResult(category.Id, category.Name, category.Icon, category.ParentId);
        }
    }
}
