﻿using System;
using System.Collections.Generic;
using Level;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Level {
    public abstract class Block : IIdentifiable {
        public Identifier Identifier { get; }
        public BlockPosition Position { get; }
        public BlockType BlockType { get; }
        public bool Valid { get; private set; }
        public GameObject GameObject { get; private set; }
        public BlockView View { get; private set; }

        protected readonly Dictionary<string, string> _metadata;

        public Block(Identifier identifier, BlockType blockType, BlockPosition position, BlockData data) {
            identifier.ValidateNotNull("Identifier cannot be null!");
            blockType.ValidateNotNull("Block type cannot be null!");

            Identifier = identifier;
            BlockType = blockType;
            Position = position;
            Valid = true;
            _metadata = data.GetMetadataCopy();

            GameObject = new GameObject(position.ToString()) { transform = { position = position.Position } };
            View = GenerateBlockView();
            View.Block = this;
        }

        public BlockData ToBlockData() {
            return new BlockData(Identifier, _metadata);
        }

        public int GetMetadataSize() {
            return _metadata?.Count ?? 0;
        }

        public string GetMetadata(string key) {
            return _metadata[key];
        }

        public bool GetMetadataBoolean(string key, bool fallback = false) {
            if (!_metadata.TryGetValue(key, out var value)) return fallback;
            return !bool.TryParse(value, out var result) ? fallback : result;
        }

        public bool HasMetadata(string key) {
            return _metadata.ContainsKey(key);
        }

        public Dictionary<string, string> GetMetadataCopy() {
            return new Dictionary<string, string>(_metadata);
        }

        public Dictionary<string, string> GetMetadataWithoutDefaultValues() {
            var def = BlockType.DefaultMetadata;
            var result = new Dictionary<string, string>();
            foreach (var pair in _metadata) {
                if (def.TryGetValue(pair.Key, out var val) && val.Value.Equals(pair.Value)) continue;
                result[pair.Key] = pair.Value;
            }

            return result;
        }

        public void ForEachMetadata(Action<string, string> action) {
            if (_metadata == null) return;
            foreach (var pair in _metadata) {
                action(pair.Key, pair.Value);
            }
        }

        public virtual void OnPlace() {
        }

        public virtual void OnBreak() {
        }

        public abstract BlockView GenerateBlockView();

        public abstract bool CanMoveTo(Direction direction);

        public abstract bool CanMoveFrom(Direction direction);

        internal void Invalidate() {
            Valid = false;
            Object.Destroy(GameObject);
            GameObject = null;
            View = null;
        }
    }
}