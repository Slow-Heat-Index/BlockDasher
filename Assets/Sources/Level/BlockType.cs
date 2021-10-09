﻿using System;
using System.Collections.Generic;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level {
    public abstract class BlockType : IIdentifiable {
        public Identifier Identifier { get; }

        public string Name { get; }

        public Aabb CollisionBox { get; }

        public Mesh DefaultMesh { get; }

        public Texture DefaultTexture { get; }

        public Dictionary<string, Type> DefaultMetadataTypes { get; } = new Dictionary<string, Type>();

        public Dictionary<string, string> DefaultMetadataValues { get; } = new Dictionary<string, string>();

        protected BlockType(Identifier identifier, string name, Aabb collisionBox, Mesh defaultMesh,
            Texture defaultTexture) {
            identifier.ValidateNotNull("Identifier cannot be null!");
            name.ValidateNotNull("Name cannot be null!");
            collisionBox.ValidateNotNull("Collision block cannot be null!");
            defaultMesh.ValidateNotNull("Default mesh cannot be null!");
            defaultTexture.ValidateNotNull("Default material cannot be null!");
            Identifier = identifier;
            Name = name;
            DefaultMesh = defaultMesh;
            DefaultTexture = defaultTexture;
            CollisionBox = collisionBox;
        }

        public virtual bool CanBePlaced(BlockPosition position) => true;

        public Block CreateBlock(BlockPosition position, BlockData data) {
            data.AddNotPresentMetadata(DefaultMetadataValues);
            return CreateBlockImpl(position, data);
        }

        protected abstract Block CreateBlockImpl(BlockPosition position, BlockData data);

        protected bool Equals(BlockType other) {
            return Equals(Identifier, other.Identifier);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((BlockType)obj);
        }

        public override int GetHashCode() {
            return Identifier.GetHashCode();
        }

        public static bool operator ==(BlockType left, BlockType right) {
            return Equals(left, right);
        }

        public static bool operator !=(BlockType left, BlockType right) {
            return !Equals(left, right);
        }
    }
}