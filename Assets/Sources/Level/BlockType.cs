using System.Collections.Generic;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level {
    /**
     * Represents the static global data for all blocks of the same type.
     */
    public abstract class BlockType : IIdentifiable {
        /**
         * The identifier of the type.
         */
        public Identifier Identifier { get; }

        /**
         * The human-readable name of the type.
         */
        public string Name { get; }

        /**
         * The default collision box.
         * Used by the editor's BlockClicked to place and remove blocks.
         */
        public Aabb DefaultCollisionBox { get; }

        /**
         * The default maximum amount of steps.
         *  The player must have done less steps than this value to leave this block in the same movement.
         */
        public int DefaultMaximumSteps { get; }

        /**
         * The mesh used to represent this BlockType in the editor.
         */
        public Mesh DefaultMesh { get; }

        /**
         * The texture used to represent this BlockType in the editor.
         */
        public Texture DefaultTexture { get; }

        /**
         * The default metadata values for the blocks of this type.
         * This dictionary is mutable. This doesn't mean that already created blocks will be updated!
         */
        public Dictionary<string, MetadataSnapshot> DefaultMetadata { get; } =
            new Dictionary<string, MetadataSnapshot>();

        protected BlockType(Identifier identifier, string name,
            Aabb defaultCollisionBox, int defaultMaximumSteps,
            Mesh defaultMesh, Texture defaultTexture) {
            identifier.ValidateNotNull("Identifier cannot be null!");
            name.ValidateNotNull("Name cannot be null!");
            defaultCollisionBox.ValidateNotNull("Collision block cannot be null!");
            defaultMesh.ValidateNotNull("Default mesh cannot be null!");
            defaultTexture.ValidateNotNull("Default material cannot be null!");
            Identifier = identifier;
            Name = name;
            DefaultMesh = defaultMesh;
            DefaultTexture = defaultTexture;
            DefaultCollisionBox = defaultCollisionBox;
            DefaultMaximumSteps = defaultMaximumSteps;
        }

        public virtual void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
        }

        /**
         *
         * Creates a Block instance using the given BlockPosition and BlockData.
         * 
         * This method also adds the default metadata values into the block.
         *
         * <param name="position">The position where the block is located at.</param>
         * <param name="data">The metadata value for the block.</param>
         * <returns>The new block.</returns>
         */
        public Block CreateBlock(BlockPosition position, BlockData data) {
            data.AddNotPresentMetadata(DefaultMetadata);
            return CreateBlockImpl(position, data);
        }

        /**
         * <param name="position">The position where the block may be placed.</param>
         * <returns>Whether this block can be placed at the given position.</returns>
         */
        public virtual bool CanBePlaced(BlockPosition position) => true;

        /**
         *
         * Creates a Block instance using the given BlockPosition and BlockData.
         * 
         * Children of this class must implement this method.
         *
         * <param name="position">The position where the block is located at.</param>
         * <param name="data">The metadata value for the block.</param>
         * <returns>The new block.</returns>
         */
        protected abstract Block CreateBlockImpl(BlockPosition position, BlockData data);

        protected bool Equals(BlockType other) {
            return Equals(Identifier, other.Identifier);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((BlockType)obj);
        }

        public override int GetHashCode() {
            return Identifier.GetHashCode();
        }

        public static bool operator ==(BlockType left, BlockType right) {
            return Equals(left, right);
        }

        public static bool operator !=(BlockType left, BlockType right) {
            return !Equals(left, right);
        }
    }
}