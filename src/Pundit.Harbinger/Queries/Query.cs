using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Pundit.Harbinger.Queries
{
    public abstract class Query<T> : IQuery<T>
    {
        private List<Func<IQueryable<T>, IQueryable<T>>> _queryNavigation = new List<Func<IQueryable<T>, IQueryable<T>>>();
        private Expression<Func<T, bool>> _curExpression;
        private Func<DbContext, IQueryable<T>> _query;

        public IQueryable<T> ExecuteNavigation(IQueryable<T> query)
        {
            foreach (var queryNavigation in _queryNavigation)
            {
                query = queryNavigation(query);
            }

            return query;
        }

        public Func<DbContext, IQueryable<T>> AsQuery() => _query;

        public Expression<Func<T, bool>> AsExpression() => _curExpression;

        public Expression<Func<T, bool>> AndAlso(Query<T> otherQuery)
        {
            Guard.Assert(() => otherQuery == null, new ArgumentNullException(nameof(otherQuery)));

            return AsExpression().AndAlso(otherQuery.AsExpression());
        }

        public Expression<Func<T, bool>> OrElse(Query<T> otherQuery)
        {
            Guard.Assert(() => otherQuery == null, new ArgumentNullException(nameof(otherQuery)));

            return AsExpression().OrElse(otherQuery.AsExpression());
        }

        protected void AddExpressionCriteria(Expression<Func<T, bool>> nextExpression)
        {
            Guard.Assert(() => nextExpression == null, new ArgumentNullException(nameof(nextExpression)));

            _curExpression = (_curExpression == null)
                                ? nextExpression
                                : _curExpression.AndAlso(nextExpression);
        }

        protected void AddNavigationCriteria(Func<IQueryable<T>, IQueryable<T>> nextCriteria)
        {
            Guard.Assert(() => nextCriteria == null, new ArgumentNullException(nameof(nextCriteria)));

            _queryNavigation.Add(nextCriteria);
        }

        protected void SetQueryCriteria(Func<DbContext, IQueryable<T>> query)
        {
            _query = query;
        }
    }
}
