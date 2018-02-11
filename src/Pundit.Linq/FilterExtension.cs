using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace System.Linq
{
    public static class FilterExtension
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, IEnumerable<FilterParameter> filterParameters)
            where T : class
        {
            Expression<Func<T, bool>> containsExpression = null;

            containsExpression = query.Contains(filterParameters, expression: containsExpression);

            if (containsExpression != null)
            {
                return query.Where(containsExpression);
            }

            return query;
        }

        private static Expression<Func<T, bool>> Contains<T>(this IQueryable<T> query,
            IEnumerable<FilterParameter> filterParameters,
            Expression<Func<T, bool>> expression = default(Expression<Func<T, bool>>))
            where T : class
        {
            foreach(var parameter in filterParameters)
            {
                if (!string.IsNullOrEmpty(parameter.Value))
                {
                    var property = typeof(T).GetProperty(parameter.Property);
                    if (property == null) continue;

                    if (expression == null)
                        expression = query.Contains(parameter.Property, parameter.Value);
                    else
                        if (!parameter.BitwseOr)
                            expression = expression.And(query.Contains(parameter.Property, parameter.Value));
                        else
                            expression = expression.Or(query.Contains(parameter.Property, parameter.Value));
                }
            }

            return expression;
        }

        private static Expression<Func<T, bool>> Contains<T>(this IQueryable<T> query, string propertyName, string value)
            where T : class
        {
            ConstantExpression searchArgument = Expression.Constant(value);
            ParameterExpression param = Expression.Parameter(typeof(T));

            Expression property = param.NavigateProperty<T>(propertyName).ToStringMethodCallExpression();

            // Get Contains method for property type
            MethodInfo containsMethodInfo = typeof(string).GetMethod("Contains");

            MethodCallExpression fieldExpression = Expression.Call(property, containsMethodInfo);

            // Create the contains expression
            return Expression.Lambda<Func<T, bool>>(fieldExpression, param);
        }
    }
}
