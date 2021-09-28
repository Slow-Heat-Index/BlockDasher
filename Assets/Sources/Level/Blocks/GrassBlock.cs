using Sources.Identification;
using Sources.Level.Data;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class GrassBlock : Block {
        public GrassBlock(Vector3Int position, BlockData data)
            : base(Identifiers.Grass, position, data) {
        }


        public new class Builder : Block.Builder {
            public Builder() : base(Identifiers.Grass) {
            }

            public override Block CreateBlock(Vector3Int position, BlockData data) {
                return new GrassBlock(position, data);
            }
        }
    }
}