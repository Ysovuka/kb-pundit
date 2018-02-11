using Microsoft.EntityFrameworkCore;
using Pundit.Harbinger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class CategoryCommandHandler : 
        ICommandHandler<CreateCategoryCommand>,
        ICommandHandler<UpdateCategoryCommand>
    {
        private readonly DatabaseContext _dbContext;
        public CategoryCommandHandler(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
        }

        public async Task ExecuteAsync(CreateCategoryCommand command)
        {
            var category = new Category(command.RequestId, command.Name, command.Icon);

            await _dbContext.AddAsync(category);

            await _dbContext.SaveChangesAsync();

            DomainEvents.Instance.Raise(new CreateCategoryEvent(category));
        }

        public async Task ExecuteAsync(UpdateCategoryCommand command)
        {
            var category = await _dbContext.Set<Category>().FirstOrDefaultAsync(c => c.Id == command.Id);

            Guard.Assert(() => category == null, new ArgumentNullException("Category", $"Category [{command.Id}] was not found."));

            category.Update(command.RequestId, command.Name, command.Icon);

            await _dbContext.SaveChangesAsync();

            DomainEvents.Instance.Raise(new UpdateCategoryEvent(category));
        }
    }
}
