using System;
using System.Collections.Generic;

namespace XrShared.Core.Services
{
    public sealed class ServiceLocator
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public void Register<T>(T instance) where T : class
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            _services[typeof(T)] = instance;
        }

        public bool TryResolve<T>(out T service) where T : class
        {
            if (_services.TryGetValue(typeof(T), out object obj) && obj is T typed)
            {
                service = typed;
                return true;
            }

            service = null;
            return false;
        }

        public T Resolve<T>() where T : class
        {
            if (TryResolve(out T service)) return service;
            throw new InvalidOperationException($"Service not registered: {typeof(T).Name}");
        }

        public void Clear()
        {
            _services.Clear();
        }
    }
}
