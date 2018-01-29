using Microsoft.EntityFrameworkCore;
using Pundit.Harbinger.Commands;
using Pundit.KnowledgeBase.WebCore.Data;
using Pundit.KnowledgeBase.WebCore.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Commands
{
    public class CreateCategoryCommandHandler : CommandHandler<CreateCategoryCommand, DatabaseRepository>
    {
        public CreateCategoryCommandHandler(DatabaseRepository repository)
            : base(repository)
        {
            
        }

        public override async Task ExecuteAsync(CreateCategoryCommand command)
        {
            Category category = null;

            if (command.HasParentAssociation())
            {
                var parent = await _dbContext.Set<Category>().FirstOrDefaultAsync(c => c.Id == command.ParentId);

                category = parent.CreateSubCategory(new Category(command.Name, command.Icon));
            }
            else
            {
                category = new Category(command.Name, command.Icon);
            }

            Guard.Assert(() => category == null, new InvalidOperationException("Category could not be created."));

            await _dbContext.AddAsync(category);

            await _dbContext.SaveChangesAsync();

            OnHandled(new CommandEventArgs(category.Id));
        }

    }
}
