using System.Threading.Tasks;

namespace Pundit.Harbinger.Commands
{
    public interface ICommandDispatcher
    {
        Task ExecuteAsync<T>(T command) where T : class, ICommand;
    }
}
