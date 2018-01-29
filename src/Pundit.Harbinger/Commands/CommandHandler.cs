using System;
using System.Threading.Tasks;

namespace Pundit.Harbinger.Commands
{
    public abstract class CommandHandler<TCommand, TContext> : ICommandHandler<TCommand>
    {
        protected readonly TContext _dbContext;
        public CommandHandler(TContext dbContext)
        {
            Guard.Assert(() => dbContext == null, new ArgumentNullException("DbContext", "DbContext cannot be null."));

            _dbContext = dbContext;
        }

        public event EventHandler<CommandEventArgs> Executed;

        public void Dispose()
        {
            (_dbContext as IDisposable)?.Dispose();

            GC.SuppressFinalize(this);
        }

        public abstract Task ExecuteAsync(TCommand command);


        protected void OnHandled(CommandEventArgs e)
        {
            Guard.Assert(() => e == null, new ArgumentNullException("CommandEventArgs", "CreateCategoryCommandHandler_HandleAsync - CommandEventArgs cannot be null."));

            if (Executed != null)
                Executed.Invoke(this, e);
        }
    }
}
