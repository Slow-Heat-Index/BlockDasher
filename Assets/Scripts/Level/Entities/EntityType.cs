using Sources.Identification;
using Sources.Level;

namespace Level.Entities {
    public abstract class EntityType : IIdentifiable {
        /**
         * The identifier of this entity type.
         */
        public Identifier Identifier { get; }

        /**
         * The name of this entity type.
         */
        public string Name { get; }

        /**
         * Creates the entity type.
         * <param name="identifier">The identifier of the entity type.</param>
         * <param name="name">The name of the entity type.</param>
         */
        public EntityType(Identifier identifier, string name) {
            Identifier = identifier;
            Name = name;
        }

        /**
         * Spawns a new entity of this entity type.
         * <param name="position">The position of the entity.</param>
         * <returns>The spawned entity.</returns>
         */
        public abstract Entity SpawnEntity(BlockPosition position);
    }
}