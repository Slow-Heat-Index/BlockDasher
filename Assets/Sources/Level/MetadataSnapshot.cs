using System;
using Sources.Level.Blocks;
using Sources.Util;

namespace Sources.Level {
    public struct MetadataSnapshot {
        public string Key;
        public string Value;
        public Type Type;

        public string Name;

        public MetadataSnapshot(string key = null, string value = null, Type type = null, string name = null) {
            Key = key;
            Value = value;
            Type = type;
            Name = name;
        }
    }

    public struct MetadataSnapshots {
        public static readonly MetadataSnapshot MetadataForceTop = new MetadataSnapshot(
            "metadata_force_top",
            "false",
            typeof(bool),
            "Force top"
        );

        public static readonly MetadataSnapshot MetadataInverse = new MetadataSnapshot(
            "metadata_inverse",
            "false",
            typeof(bool),
            "Inverse"
        );


        public static readonly MetadataSnapshot MetadataFacing = new MetadataSnapshot(
            "metadata_facing",
            "North",
            typeof(Direction),
            "Facing"
        );

        public static readonly MetadataSnapshot MetadataTreeType = new MetadataSnapshot(
            "metadata_tree_type",
            "Random",
            typeof(TreeBlock.TreeType),
            "Tree Type"
        );

        public static readonly MetadataSnapshot MetadataSkullType = new MetadataSnapshot(
            "metadata_skull_type",
            "Random",
            typeof(SkullBlock.SkullType),
            "Skull Type"
        );
        
        public static readonly MetadataSnapshot MetadataRockType = new MetadataSnapshot(
            "metadata_rock_type",
            "Random",
            typeof(RockBlock.RockType),
            "Rock Type"
        );
        
        public static readonly MetadataSnapshot MetadataBeachFlowersType = new MetadataSnapshot(
            "metadata_beach_flowers_type",
            "Flowers1",
            typeof(BeachFlowersBlock.BeachFlowersType),
            "Beach Flowers Type"
        );
    }
}