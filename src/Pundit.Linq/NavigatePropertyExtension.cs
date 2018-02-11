using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq
{
    public static class NavigatePropertyExtensions
    {
        public static LambdaExpression NavigateProperty<TEntity>(this ParameterExpression parameter, string navigationProperty)
            where TEntity : class
        {
            var properties = navigationProperty.Split(".");

            var property = Expression.Property(parameter, properties[0]);

            property = properties.Skip(1).Aggregate(property, Expression.Property);

            var lambda = Expression.Lambda(typeof(Func<,>).MakeGenericType(typeof(TEntity), property.Type), property, parameter);

            return lambda;
        }

        public static MethodCallExpression ToStringMethodCallExpression(this LambdaExpression lambda)
        {
            // Get return type of lambda expression.
            var type = lambda.ReturnType;

            // Get ToString method for property type
            MethodInfo toStringMethodInfo = type.GetMethod("ToString", new Type[] { });

            // Invoke ToString on property object.
            MethodCallExpression toStringMethod = Expression.Call(lambda.Body, toStringMethodInfo);

            return toStringMethod;
        }
    }
}
