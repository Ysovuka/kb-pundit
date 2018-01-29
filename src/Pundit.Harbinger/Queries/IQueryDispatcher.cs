using System.Threading.Tasks;

namespace Pundit.Harbinger.Queries
{
    public interface IQueryDispatcher
    {
        Task ExecuteAsync<T, TQuery>(TQuery query) where TQuery : class, IQuery<T>;
    }
}
