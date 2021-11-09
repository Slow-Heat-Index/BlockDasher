using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class StartBlock : Block {
        public StartBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Start, StartBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<StartBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => true;
        public override bool IsClimbableFrom(Direction direction) => false;

        public override void OnPlace() {
            Position.World.StartPosition = this;
        }

        public override void OnBreak() {
            if (Position.World.StartPosition == this) {
                Position.World.StartPosition = null;
            }
        }

        public class StartBlockType : BlockType {
            public static readonly StartBlockType Instance = new StartBlockType();

            private StartBlockType() : base(
                Identifiers.Start,
                "Start",
                new Aabb(0, 0, 0, 1, 0.1f, 1),
                2,
                true,
                Resources.Load<Mesh>("Models/StartEndModel"),
                Resources.Load<Texture>("Models/Blocks/Start/Default")
            ) {
            }

            public override bool CanBePlaced(BlockPosition position) => position.World.StartPosition == null;

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new StartBlock(position, data);
            }
        }
    }
}