using System.Collections.Generic;
using Sources.Identification;
using Sources.Level.Data;
using UnityEngine;

namespace Sources.Level {
    public class Block : IIdentifiable, IWorldPositionable {
        public Identifier Identifier { get; }
        public Vector3Int Position { get; }

        protected Dictionary<string, string> _metadata;

        public Block(Identifier identifier, Vector3Int position, BlockData data) {
            Identifier = identifier;
            Position = position;
            _metadata = data.GetMetadataCopy();
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