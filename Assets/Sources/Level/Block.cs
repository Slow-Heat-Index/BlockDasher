using System.Collections.Generic;
using Level;
using Sources.Identification;
using Sources.Level.Data;
using UnityEngine;

namespace Sources.Level {
    public abstract class Block : IIdentifiable {
        public Identifier Identifier { get; }
        public BlockPosition Position { get; }
        public bool Valid { get; private set; }
        public GameObject GameObject { get; private set; }
        public BlockView View { get; private set; }

        protected Dictionary<string, string> _metadata;

        public Block(Identifier identifier, BlockPosition position, BlockData data) {
            Identifier = identifier;
            Position = position;
            Valid = true;
            _metadata = data.GetMetadataCopy();

            GameObject = new GameObject(position.ToString()) { transform = { position = position.Position } };
            View = GenerateBlockView();
            View.Block = this;
        }

        public abstract BlockView GenerateBlockView();

        internal void Invalidate() {
            Valid = false;
            Object.Destroy(GameObject);
            GameObject = null;
            View = null;
        }
    }
}