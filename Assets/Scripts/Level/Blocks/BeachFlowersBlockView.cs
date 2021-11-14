using System;
using Sources.Level;
using Sources.Level.Blocks;
using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class BeachFlowersBlockView : BlockView {

        public override void Initialize() {
            base.Initialize();
            gameObject.isStatic = true;
            transform.rotation = Quaternion.Euler(0, RotationHash(Block.Position.Position) * 90.0f, 0);
        }

        public override bool IsFaceOpaque(Direction direction) {
            return false;
        }

        public override bool Collides(Direction fromFace, Vector3 current, Vector3 origin, Vector3 direction,
            out Direction face,
            out Vector3 collision) {
            return Block.BlockType.DefaultCollisionBox.CollidesSegment(
                Block.Position.Position, current, current + direction * 2,
                out collision, out face);
        }

        protected override Mesh LoadMesh() {
            var type = Block.GetMetadataEnum<BeachFlowersBlock.BeachFlowersType>(MetadataSnapshots.MetadataBeachFlowersType.Key, 0);

            return Resources.Load<Mesh>(type switch {
                0 => "Models/Blocks/BeachFlowers1/Model",
                1 => "Models/Blocks/BeachFlowers2/Model",
                2 => "Models/Blocks/BeachFlowers3/Model",
                3 => "Models/Blocks/BeachFlowersRed/Model",
                4 => "Models/Blocks/BeachFlowersRock/Model",
                5 => "Models/Blocks/BeachFlowersSun/Model",
                _ => throw new ArgumentOutOfRangeException(type +" - "+type)
            });
        }

        protected override Material LoadMaterial() {
            var type = Block.GetMetadataEnum<BeachFlowersBlock.BeachFlowersType>(MetadataSnapshots.MetadataBeachFlowersType.Key, 0);

            return Resources.Load<Material>(type switch {
                0 => "Models/Blocks/BeachFlowers1/DefaultMaterial",
                1 => "Models/Blocks/BeachFlowers2/DefaultMaterial",
                2 => "Models/Blocks/BeachFlowers3/DefaultMaterial",
                3 => "Models/Blocks/BeachFlowersRed/DefaultMaterial",
                4 => "Models/Blocks/BeachFlowersRock/DefaultMaterial",
                5 => "Models/Blocks/BeachFlowersSun/DefaultMaterial",
                _ => throw new ArgumentOutOfRangeException(type +" - "+type)
            });
        }


        private int RotationHash(Vector3Int position) {
            return Math.Abs(position.x * 73 + position.y * 331 + position.z * 571) % 881;
        }
    }
}