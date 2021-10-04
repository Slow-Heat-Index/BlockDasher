﻿using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class GrassBlock : Block {
        public GrassBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Grass, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<GrassBlockView>();

        public class BlockType : Level.BlockType {

            public static readonly BlockType Instance = new BlockType();
            
            private BlockType() : base(
                Identifiers.Grass,
                "Grass",
                Resources.Load<Mesh>("Models/Grass/Model"),
                Resources.Load<Texture>("Models/Grass/White")
            ) {
            }

            public override Block CreateBlock(BlockPosition position, BlockData data) {
                return new GrassBlock(position, data);
            }
        }
    }
}