using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pundit.KnowledgeBase.WebCore.Data;
using Pundit.KnowledgeBase.WebCore.Debug;

namespace Pundit.KnowledgeBase.WebCore.Business
{
    public class CategoryBusinessLayer : ICategoryBusinessLayer, IDisposable
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<long> CreateAsync(Category category)
        {
            return await OnCreateAsync(category);
        }

        public async Task<IEnumerable<Category>> ReadAllAsync()
        {
            return await OnReadAllAsync();
        }

        public async Task<Category> ReadAsync(long categoryId)
        {
            return await OnReadAsync(categoryId);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            return await OnUpdateAsync(category);
        }

        public async Task<Category> DeleteAsync(long categoryId)
        {
            return await OnDeleteAsync(categoryId);
        }

        protected virtual async Task<long> OnCreateAsync(Category category)
        {
            Guard.Assert(() => category == null, new ArgumentNullException("Category", "Category cannot be null."));

            using (var context = CreateContext())
            {
                await context.Categories.AddAsync(category);

                await context.SaveChangesAsync();

                return category.Id;
            }
        }

        protected virtual async Task<IEnumerable<Category>> OnReadAllAsync()
        {
            using (var context = CreateContext())
            {
                var results = await context.Set<Category>().ToListAsync();

                return results;
            }
        }
        
        protected virtual async Task<Category> OnReadAsync(long categoryId)
        {
            using (var context = CreateContext())
            {
                Category dbCategory = await context.Set<Category>().FirstOrDefaultAsync(c => c.Id == categoryId);

                Guard.Assert(() => dbCategory == null, new ArgumentNullException("Category", $"Category [{categoryId}] not found."));

                return dbCategory;
            }
        }

        protected virtual async Task<Category> OnUpdateAsync(Category category)
        {
            Guard.Assert(() => category == null, new ArgumentNullException("Category", "Category cannot be null."));

            using (var context = CreateContext())
            {
                context.Update(category);

                await context.SaveChangesAsync();

                return category;
            }
        }

        protected virtual async Task<Category> OnDeleteAsync(long categoryId)
        {
            using (var context = CreateContext())
            {
                Category dbCategory = await context.Set<Category>().FirstOrDefaultAsync(c => c.Id == categoryId);

                Guard.Assert(() => dbCategory == null, new ArgumentNullException("Category", $"Category [{categoryId}] not found."));

                context.Remove(dbCategory);

                await context.SaveChangesAsync();

                return dbCategory;
            }
        }

        private KnowledgeBaseContext CreateContext()
        {
            Guard.Assert(() => DbContextOptions == null, new ArgumentNullException("Database Context Options", "DbContextOptions cannot be null."));

            return new KnowledgeBaseContext(_options);
        }
    }
}
