using Level;
using Level.Blocks;
using Level.Player.Data;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class EndBlock : Block {
        public EndBlock(BlockPosition position, BlockData data)
            : base(Identifiers.End, EndBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<EndBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => true;
        public override bool IsClimbableFrom(Direction direction) => false;

        public override void OnPlayerStopsIn(PlayerData player) {
            player.Win();
        }

        public class EndBlockType : BlockType {
            public static readonly EndBlockType Instance = new EndBlockType();

            private EndBlockType() : base(
                Identifiers.End,
                "End",
                new Aabb(0, 0, 0, 1, 0.1f, 1),
                2,
                Resources.Load<Mesh>("Models/StartEndModel"),
                Resources.Load<Texture>("Models/Blocks/End/Default")
            ) {
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new EndBlock(position, data);
            }
        }
    }
}