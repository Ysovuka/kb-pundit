using System;
using System.Threading.Tasks;

namespace Pundit.Harbinger
{
    public interface IQueryHandler<TQuery, TResult> :
        IDisposable
        where TQuery : IQuery<TResult>
    {
        Task<TResult> ExecuteAsync(TQuery query);
    }
}
