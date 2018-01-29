using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Pundit.Harbinger.Queries
{
    public interface IQuery<T>
    {
        Expression<Func<T, bool>> AsExpression();
        IQueryable<T> ExecuteNavigation(IQueryable<T> query);
        Func<DbContext, IQueryable<T>> AsQuery();
    }
}
