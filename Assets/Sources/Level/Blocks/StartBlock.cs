﻿using Level;
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
                Resources.Load<Mesh>("Models/StartEndModel"),
                Resources.Load<Texture>("Models/Start/Default")
            ) {
            }

            public override bool CanBePlaced(BlockPosition position) => position.World.StartPosition == null;

            public override Block CreateBlock(BlockPosition position, BlockData data) {
                return new StartBlock(position, data);
            }
        }
    }
}