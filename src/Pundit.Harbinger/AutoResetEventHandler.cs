using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace Pundit.Harbinger
{
    public class AutoResetEventHandler
    {
        private AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        private EventInfo _event = null;
        private object _eventContainer = null;

        public AutoResetEventHandler(object eventContainer, EventHandler eventName)
        {
            _eventContainer = eventContainer;
            _event = eventContainer.GetType().GetEvent(eventName.Method.Name);
        }
        public void WaitForEvent()
        {
            Delegate handler = CreateHandler();

            _event.AddEventHandler(_eventContainer, handler);

            _autoResetEvent.WaitOne();

            _event.RemoveEventHandler(_eventContainer, handler);

        }

        private Delegate CreateHandler()
        {
            var invokeMethod = _event.EventHandlerType.GetMethod("Invoke");
            var invokeParameters = invokeMethod.GetParameters();
            var handlerParameters = invokeParameters.Select(p => Expression.Parameter(p.ParameterType, p.Name)).ToArray();
            var body = Expression.Call(Expression.Constant(_autoResetEvent), "Set", null);
            var handlerExpression = Expression.Lambda(_event.EventHandlerType, body, handlerParameters);
            return handlerExpression.Compile();
        }
    }
}
