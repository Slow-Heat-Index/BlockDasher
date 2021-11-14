using Level.Entities;
using Sources.Identification;
using Sources.Registration;

namespace Sources.Level.Manager {
    public class EntityManager : Manager<EntityType> {
        public EntityManager() : base(Identifiers.ManagerEntity) {
            Register(TriangleEntity.TriangleEntityType.Instance);
            Register(SphereEntity.SphereEntityType.Instance);
        }
    }
}