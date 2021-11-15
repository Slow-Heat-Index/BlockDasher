using System.Collections.Generic;
using Sources.Identification;
using Sources.Level.Manager;

namespace Sources.Registration {
    public static class Registry {
        private static readonly Dictionary<Identifier, IManager> Managers =
            new Dictionary<Identifier, IManager> {
                { Identifiers.ManagerBlock, new BlockManager() },
                { Identifiers.ManagerEntity, new EntityManager() },
                { Identifiers.ManagerSkybox, new SkyboxManager() }
            };

        public static bool Register<T>(Manager<T> manager) where T : IIdentifiable {
            if (Managers.ContainsKey(manager.Identifier)) return false;
            Managers[manager.Identifier] = manager;
            return true;
        }

        public static Manager<T> Get<T>(Identifier identifier) where T : IIdentifiable {
            if (!Managers.TryGetValue(identifier, out var manager)) return null;
            if (!manager.ManagedType.IsAssignableFrom(typeof(T))) return null;
            return manager as Manager<T>;
        }
    }
}