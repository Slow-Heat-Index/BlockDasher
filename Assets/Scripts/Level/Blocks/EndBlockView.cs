﻿using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class EndBlockView : BlockView {
        protected override void Start() {
            base.Start();
            gameObject.isStatic = true;
        }

        public override bool IsFaceOpaque(Direction direction) {
            return false;
        }

        public override bool Collides(Direction fromFace, Vector3 current, Vector3 origin, Vector3 direction,
            out Direction face,
            out Vector3 collision) {
            return Block.BlockType.CollisionBox.CollidesSegment(
                Block.Position.Position, current, current + direction * 2,
                out collision, out face);
        }

        protected override Mesh LoadMesh() {
            return Resources.Load<Mesh>("Models/StartEndModel");
        }

        protected override Material LoadMaterial() {
            return Resources.Load<Material>("Models/End/DefaultMaterial");
        }
    }
}