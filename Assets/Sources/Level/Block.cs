using System.Collections.Generic;
using Level;
using Sources.Identification;
using Sources.Level.Data;
using UnityEngine;

namespace Sources.Level {
    public class Block : IIdentifiable, IWorldPositionable {
        public Identifier Identifier { get; }
        public Vector3Int Position { get; }
        public bool Valid { get; private set; }
        public GameObject GameObject { get; private set; }
        public BlockView View { get; private set; }

        protected Dictionary<string, string> _metadata;

        public Block(Identifier identifier, Vector3Int position, BlockData data) {
            Identifier = identifier;
            Position = position;
            Valid = true;
            _metadata = data.GetMetadataCopy();

            GameObject = new GameObject(position.ToString()) { transform = { position = position } };
            View = GameObject.AddComponent<BlockView>();
            View.block = this;
        }

        internal void Invalidate() {
            Valid = false;
            Object.Destroy(GameObject);
            GameObject = null;
            View = null;
        }

        public abstract class Builder : IIdentifiable {
            public Identifier Identifier { get; }

            public Builder(Identifier identifier) {
                Identifier = identifier;
            }

            public abstract Block CreateBlock(Vector3Int position, BlockData data);
        }
    }
}