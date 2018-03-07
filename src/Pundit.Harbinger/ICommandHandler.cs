using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pundit.Harbinger
{
    public interface ICommandHandler<TCommand> :
        IDisposable
        where TCommand : ICommand
    {
        Task ExecuteAsync(TCommand command, CancellationToken cancellationToken = default(CancellationToken));
    }
}
