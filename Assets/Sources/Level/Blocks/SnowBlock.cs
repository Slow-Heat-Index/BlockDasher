using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class SnowBlock : Block {
        public SnowBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Snow, SnowBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<SnowBlockView>();

        public class SnowBlockType : BlockType {
            public static readonly SnowBlockType Instance = new SnowBlockType();

            private SnowBlockType() : base(
                Identifiers.Snow,
                "Snow",
                new Aabb(0, 0, 0, 1, 1, 1),
                Resources.Load<Mesh>("Models/BlockModel"),
                Resources.Load<Texture>("Models/Snow/White")
            ) {
            }

            public override Block CreateBlock(BlockPosition position, BlockData data) {
                return new SnowBlock(position, data);
            }
        }
    }
}