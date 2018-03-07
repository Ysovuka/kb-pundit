using Microsoft.EntityFrameworkCore;
using Pundit.Harbinger;
using System;
using System.Threading;
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

        public async Task ExecuteAsync(CreateCategoryCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var category = new Category(command.RequestId, command.Id, command.Name, command.Icon);

            await _dbContext.AddAsync(category, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            DomainEvents.Instance.Raise(new CreateCategoryEvent(category));
        }

        public async Task ExecuteAsync(UpdateCategoryCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var category = await _dbContext.Set<Category>().FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

            Guard.Assert(() => category == null, new ArgumentNullException("Category", $"Category [{command.Id}] was not found."));

            category.Update(command.RequestId, command.Name, command.Icon);

            await _dbContext.SaveChangesAsync(cancellationToken);

            DomainEvents.Instance.Raise(new UpdateCategoryEvent(category));
        }
    }
}
