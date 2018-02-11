using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Pundit.KnowledgeBase.WebCore.UnitTests
{
    [TestClass]
    public class NavigationPropertyTests
    {
        [TestMethod]
        [DataRow("TestClass2.FinalProperty")]
        public void Walk_NavigationProperty_Expression(string navigationProperty)
        {
            var testClass = new TestClass();

            var lambda = TryNavigation<TestClass>(navigationProperty);

            Delegate toString = lambda.Compile();

            Assert.AreEqual("Cool", toString.DynamicInvoke(testClass));
        }

        private LambdaExpression TryNavigation<TEntity>(string navigationProperty)
        {
            var properties = navigationProperty.Split(".");

            ParameterExpression parameter = Expression.Parameter(typeof(TEntity));
            var property = Expression.Property(parameter, properties[0]);

            property = properties.Skip(1).Aggregate(property, Expression.Property);

            return Expression.Lambda(typeof(Func<,>).MakeGenericType(typeof(TEntity), property.Type), property, parameter);
        }
    }

    public class TestClass
    {
        public TestClass2 TestClass2 { get; set; } = new TestClass2();
    }

    public class TestClass2
    {
        public string FinalProperty { get; set; } = "Cool";
    }
}
