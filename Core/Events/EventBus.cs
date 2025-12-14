using System;
using System.Collections.Generic;

namespace XrShared.Core.Services
{
    public sealed class EventBus
    {
        private readonly Dictionary<Type, Delegate> _handlers = new Dictionary<Type, Delegate>();

        public void Subscribe<T>(Action<T> handler)
        {
            Type type = typeof(T);
            if (_handlers.TryGetValue(type, out Delegate existing))
                _handlers[type] = Delegate.Combine(existing, handler);
            else
                _handlers[type] = handler;
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            Type type = typeof(T);
            if (_handlers.TryGetValue(type, out Delegate existing))
            {
                Delegate next = Delegate.Remove(existing, handler);
                if (next == null) _handlers.Remove(type);
                else _handlers[type] = next;
            }
        }

        public void Publish<T>(T evt)
        {
            Type type = typeof(T);
            if (_handlers.TryGetValue(type, out Delegate d) && d is Action<T> a)
            {
                a.Invoke(evt);
            }
        }
    }
}
