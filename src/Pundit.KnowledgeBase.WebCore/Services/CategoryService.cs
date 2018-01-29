using Microsoft.EntityFrameworkCore;
using Pundit.Harbinger.Commands;
using Pundit.Harbinger.Queries;
using Pundit.KnowledgeBase.WebCore.Commands;
using Pundit.KnowledgeBase.WebCore.Data;
using Pundit.KnowledgeBase.WebCore.Debug;
using Pundit.KnowledgeBase.WebCore.Query;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Services
{
    public class CategoryService : IDisposable
    {
        private readonly DbContextOptionsBuilder<DatabaseRepository> _builder;
        public CategoryService(DbContextOptionsBuilder<DatabaseRepository> builder)
        {
            Guard.Assert(() => builder == null, new ArgumentNullException("Database Repository Builder", "DbContextOptionsBuilder<DatabaseRepository> [builder] cannot be null."));

            _builder = builder;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<long> CreateAsync(CreateCategoryCommand command)
        {
            AutoResetEvent resetEvent = new AutoResetEvent(false);
            long categoryId = -1;

            using (var commandHandler = new CreateCategoryCommandHandler(new DatabaseRepository(_builder.Options)))
            {
                commandHandler.Executed += (object sender, CommandEventArgs e) =>
                {
                    categoryId = e.Id;
                    resetEvent.Set();
                };

                await commandHandler.ExecuteAsync(command);
                resetEvent.WaitOne();
            }

            Guard.Assert(() => categoryId == -1, new InvalidOperationException("Category could not be created."));

            return categoryId;
        }

        public async Task<Category> ReadAsync(ReadCategoryQuery query)
        {
            AutoResetEvent resetEvent = new AutoResetEvent(false);
            Category results = null;

            using (var queryHandler = new ReadObjectQueryHandler<Category, ReadCategoryQuery>(new DatabaseRepository(_builder.Options)))
            {
                queryHandler.Executed += (object sender, QueryEventArgs e) =>
                {
                    results = (Category)e.Data;
                    resetEvent.Set();
                };

                await queryHandler.ExecuteAsync(query);
                resetEvent.WaitOne();
            }

            return results;
        }

        public async Task<IEnumerable<Category>> ReadAllAsync(ReadAllCategoriesQuery query)
        {
            AutoResetEvent resetEvent = new AutoResetEvent(false);
            List<Category> results = new List<Category>();

            using (var queryHandler = new ReadListQueryHandler<Category, ReadAllCategoriesQuery>(new DatabaseRepository(_builder.Options)))
            {
                queryHandler.Executed += (object sender, QueryEventArgs e) =>
                {
                    results = (List<Category>)e.Data;
                    resetEvent.Set();
                };

                await queryHandler.ExecuteAsync(query);
                resetEvent.WaitOne();
            }

            return results;
        }
    }
}
