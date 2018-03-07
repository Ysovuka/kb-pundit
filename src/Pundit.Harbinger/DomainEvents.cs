using System;
using System.Collections.Generic;
using System.Linq;

namespace Pundit.Harbinger
{
    public class DomainEvents
    {
        private DomainEvents() { }
        private static DomainEvents _instance;

        public static DomainEvents Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DomainEvents();

                return _instance;
            }
        }

        [ThreadStatic] //so that each thread has its own callbacks
        private IDictionary<string, Delegate> _actions;

        //Registers a callback for the given domain event
        public void Register<T>(string actionName, Action<T> callback)
            where T : IDomainEvent
        {
            if (_actions == null)
                _actions = new Dictionary<string, Delegate>();

            _actions.Add(actionName, callback);
        }

        public void Unregister(string actionName)
        {
            if (_actions == null)
                _actions = new Dictionary<string, Delegate>();

            _actions.Remove(actionName, out Delegate action);
        }

        //Clears callbacks passed to Register on the current thread
        public void ClearCallbacks()
        {
            _actions.Clear();
        }

        //Raises the given domain event
        public void Raise<T>(T args)
            where T : IDomainEvent
        {
            if (_actions != null)
            {
                foreach( var action in _actions.Where(p => p.Value is Action<T>))
                {
                    ((Action<T>)action.Value)(args);
                }
            }
        }
    }
}
