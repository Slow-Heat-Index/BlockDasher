using System.Collections.Generic;
using Sources.Identification;

namespace Sources.Registration {
    public static class Registry {
        private static readonly Dictionary<Identifier, IManager> _managers =
            new Dictionary<Identifier, IManager> {
                
            };

        public static bool Register<T>(Manager<T> manager) where T : IIdentifiable {
            if (_managers.ContainsKey(manager.Identifier)) return false;
            _managers[manager.Identifier] = manager;
            return true;
        }

        public static Manager<T> Get<T>(Identifier identifier) where T : IIdentifiable {
            if (!_managers.TryGetValue(identifier, out var manager)) return null;
            if (!manager.GetType().GenericTypeArguments[0].IsAssignableFrom(typeof(T))) return null;
            return manager as Manager<T>;
        }
    }
}