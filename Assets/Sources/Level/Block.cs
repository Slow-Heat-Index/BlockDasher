using System;
using System.Collections.Generic;
using Level;
using Level.Player.Data;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Level {
    /**
     * This is the logic part of a block. This class stores the logic and the local data of a block.
     *
     * The class BlockType stores the general static information for this block's type.
     * The class BlockView stores and manages the GameObject linked to this block.
     * 
     */
    public abstract class Block : IIdentifiable {
        /**
         * This block's type.
         */
        public Identifier Identifier { get; }

        /**
         * The position of this block in the World.
         */
        public BlockPosition Position { get; }

        /**
         * The class that stores the general static information for this block's type.
         */
        public BlockType BlockType { get; }

        /**
         * Whether this block is valid.
         * When a block is not valid, the GameObject and View are null.
         */
        public bool Valid { get; private set; }

        /**
         * The GameObject linked to this block.
         */
        public GameObject GameObject { get; private set; }

        /**
         * The class that stores and manages the GameObject linked to this block.
         */
        public BlockView View { get; private set; }

        /**
         * The player must have done less steps than this value to leave this block in the same movement.
         */
        public virtual int MaximumSteps => BlockType.DefaultMaximumSteps;

        /**
         * Returns whether this block should behave like an air block.
         */
        public virtual bool BehavesLikeAir => BlockType.BehavesLikeAirAsDefault;

        /**
         * Returns the collision box of this block. Used in the editor.
         */
        public virtual Aabb CollisionBox => BlockType.DefaultCollisionBox;

        protected readonly Dictionary<string, string> Metadata;

        public Block(Identifier identifier, BlockType blockType, BlockPosition position, BlockData data) {
            identifier.ValidateNotNull("Identifier cannot be null!");
            blockType.ValidateNotNull("Block type cannot be null!");

            Identifier = identifier;
            BlockType = blockType;
            Position = position;
            Valid = true;
            Metadata = data.GetMetadataCopy();

            GameObject = new GameObject(position.ToString()) { transform = { position = position.Position } };
            View = GenerateBlockView();
            View.Block = this;
            View.Initialize();
        }

        /**
         * Transforms this block into a BlockData.
         * This method is used to save this block's state into a file.
         * <returns>The BlockData.</returns>
         */
        public BlockData ToBlockData() {
            return new BlockData(Identifier, Metadata);
        }

        /**
         * <returns>The amount of metadata values this block has.</returns>
         */
        public int GetMetadataSize() {
            return Metadata?.Count ?? 0;
        }

        /**
         * Returns the value of the metadata that matches the given key.
         *
         * <param name="key">The key of the metadata.</param>
         * <returns>The value or null if the metadata is not present.</returns>
         */
        public string GetMetadata(string key) {
            return Metadata[key];
        }

        /**
         * Same as GetMetadata, but parsed as a boolean.
         *
         * <param name="key">The key of the metadata.</param>
         * <param name="fallback">The value to use if the metadata is not found of it is not a boolean.</param>
         * <returns>The value as a boolean.</returns>
         */
        public bool GetMetadataBoolean(string key, bool fallback = false) {
            if (!Metadata.TryGetValue(key, out var value)) return fallback;
            return !bool.TryParse(value, out var result) ? fallback : result;
        }

        public int GetMetadataEnum<T>(string key, int fallback) where T : Enum {
            if (!Metadata.TryGetValue(key, out var value)) return fallback;
            var values = Enum.GetValues(typeof(T));
            foreach (int item in values) {
                if (Enum.GetName(typeof(T), item) == value) {
                    return item;
                }
            }

            return fallback;
        }

        /**
         * <param name="key">The key of the metadata.</param>
         * <returns>Whether there's a metadata value inside this block that matches the given key.</returns>
         */
        public bool HasMetadata(string key) {
            return Metadata.ContainsKey(key);
        }

        /**
         * <returns>A new Dictionary containing all metadata values inside this block.</returns>
         */
        public Dictionary<string, string> GetMetadataCopy() {
            return new Dictionary<string, string>(Metadata);
        }

        /**
         * Same as GetMetadataCopy, but removing all default values.
         *
         * Default values are defined in the BlockType.
         *
         * <returns>A new Dictionary containing all metadata values inside this block that are not default.</returns>
         */
        public Dictionary<string, string> GetMetadataWithoutDefaultValues() {
            var def = BlockType.DefaultMetadata;
            var result = new Dictionary<string, string>();
            foreach (var pair in Metadata) {
                if (def.TryGetValue(pair.Key, out var val) && val.Value.Equals(pair.Value)) continue;
                result[pair.Key] = pair.Value;
            }

            return result;
        }

        /**
         * Executes the given action for every metadata key-value pair.
         * <param name="action">The action to execute.</param>
         */
        public void ForEachMetadata(Action<string, string> action) {
            if (Metadata == null) return;
            foreach (var pair in Metadata) {
                action(pair.Key, pair.Value);
            }
        }

        /**
         * This method is invoked when this block is placed.
         */
        public virtual void OnPlace() {
        }

        /**
         * This method is invoked when this block is broken.
         */
        public virtual void OnBreak() {
        }

        /**
         * This method is invoked when a player enters this block.
         * <returns>Whether the player should stop its movement.</returns>
         */
        public virtual bool OnPlayerStepsIn(PlayerData player) {
            return false;
        }

        /**
         * This method is invoked when a player enters this block and stops in it.
         */
        public virtual void OnPlayerStopsIn(PlayerData playerData) {
        }

        /**
         * Generates a BlockView for this Block.
         * <returns>The BlockView.</returns>
         */
        public abstract BlockView GenerateBlockView();

        /**
         * <param name="direction">The direction the player is trying to move.</param>
         * <returns>Whether the player can move to the block located at the given direction.</returns>
         */
        public abstract bool CanMoveTo(Direction direction);

        /**
         * <param name="direction">The direction from where the player is trying to enter this block.</param>
         * <returns>Whether the player can move to this block from the given direction.</returns>
         */
        public abstract bool CanMoveFrom(Direction direction);

        /**
         * <param name="direction">The direction from where the player is trying to climb this block.</param>
         * Returns whether this block is climbable from the given direction.
         */
        public abstract bool IsClimbableFrom(Direction direction);

        /**
         * Invalidates this block.
         * This method should only be used by Chunk!
         */
        internal void Invalidate() {
            Valid = false;
            Object.Destroy(GameObject);
            GameObject = null;
            View = null;
        }
    }
}