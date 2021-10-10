using System;

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
    }
}