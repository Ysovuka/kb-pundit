using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pundit.KnowledgeBase.WebCore.Data;
using Pundit.KnowledgeBase.WebCore.Debug;

namespace Pundit.KnowledgeBase.WebCore.Business
{
    public class CategoryBusinessLayer : ICategoryBusinessLayer
    {
        private DbContextOptions<KnowledgeBaseContext> _options;
        public DbContextOptions<KnowledgeBaseContext> DbContextOptions { get { return _options; } set { _options = value; } }

        public CategoryBusinessLayer()
        {

        }

        public CategoryBusinessLayer(DbContextOptions<KnowledgeBaseContext> options)
        {
            Guard.Assert(() => options == null, new ArgumentNullException("Options", "Options cannot be null."));

            _options = options;
        }

        public async Task<long> CreateAsync(Category category)
        {
            return await OnCreateAsync(category);
        }

        public async Task<Category> ReadAsync(long categoryId)
        {
            return await OnReadAsync(categoryId);
        }

        protected virtual async Task<long> OnCreateAsync(Category category)
        {
            Guard.Assert(() => DbContextOptions == null, new ArgumentNullException("Database Context Options", "DbContextOptions cannot be null."));
            Guard.Assert(() => category == null, new ArgumentNullException("Category", "Category cannot be null."));

            using (var context = CreateContext())
            {
                await context.Categories.AddAsync(category);

                await context.SaveChangesAsync();

                return category.Id;
            }
        }
        
        protected virtual async Task<Category> OnReadAsync(long categoryId)
        {
            Guard.Assert(() => DbContextOptions == null, new ArgumentNullException("Database Context Options", "DbContextOptions cannot be null."));

            using (var context = CreateContext())
            {
                Category dbCategory = await context.Set<Category>().FirstOrDefaultAsync(c => c.Id == categoryId);

                Guard.Assert(() => dbCategory == null, new ArgumentNullException("Category", $"Category [{categoryId}] not found."));

                return dbCategory;
            }
        }

        private KnowledgeBaseContext CreateContext()
        {
            return new KnowledgeBaseContext(_options);
        }
    }
}
