using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pundit.KnowledgeBase.WebCore.Data;

namespace Pundit.KnowledgeBase.WebCore.Business
{
    public class CategoryBusinessLayer : ICategoryBusinessLayer
    {
        private DbContextOptions<KnowledgeBaseContext> _options;
        public CategoryBusinessLayer(DbContextOptions<KnowledgeBaseContext> options)
        {
            _options = options;
        }

        public async Task<long> CreateAsync(Category category)
        {
            using (var context = CreateContext())
            {
                await context.Categories.AddAsync(category);

                await context.SaveChangesAsync();

                return category.Id;
            }
        }

        private KnowledgeBaseContext CreateContext()
        {
            return new KnowledgeBaseContext(_options);
        }
    }
}
