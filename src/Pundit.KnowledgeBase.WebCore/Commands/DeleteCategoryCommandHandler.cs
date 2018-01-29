using Microsoft.EntityFrameworkCore;
using Pundit.Harbinger.Commands;
using Pundit.KnowledgeBase.WebCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Commands
{
    public class DeleteCategoryCommandHandler : CommandHandler<DeleteCategoryCommand, DatabaseRepository>
    {
        public DeleteCategoryCommandHandler(DatabaseRepository repository)
            : base(repository)
        {

        }

        public override async Task ExecuteAsync(DeleteCategoryCommand command)
        {
            var category = await _dbContext.Set<Category>().FirstOrDefaultAsync(c => c.Id == command.Id);

            Guard.Assert(() => category == null, new InvalidOperationException($"Category [{command.Id}] was not found."));

            await category.DeleteSubCategoriesAsync(this);

            _dbContext.Remove(category);

            await _dbContext.SaveChangesAsync();

            OnHandled(new CommandEventArgs(category.Id));
        }

    }
}
