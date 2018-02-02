using Pundit.Harbinger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand>
    {
        private readonly DatabaseContext _dbContext;
        public CreateCategoryCommandHandler(DatabaseContext dbContext)
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
    }
}
