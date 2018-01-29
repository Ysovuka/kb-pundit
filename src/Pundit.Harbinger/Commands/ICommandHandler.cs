using System;
using System.Threading.Tasks;

namespace Pundit.Harbinger.Commands
{
    public interface ICommandHandler<T> : IDisposable
    {
        event EventHandler<CommandEventArgs> Executed;
        Task ExecuteAsync(T command);
    }
}
