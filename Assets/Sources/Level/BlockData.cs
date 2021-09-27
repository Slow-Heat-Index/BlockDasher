using System;
using System.Collections.Generic;
using Sources.Identification;
using UnityEngine;

namespace Sources.Level {
    public struct BlockData {
        public Identifier Identifier;
        private Dictionary<string, string> _metadata;

        public BlockData(Identifier identifier) {
            Identifier = identifier;
            _metadata = null;
        }

        public BlockData(Identifier identifier, IDictionary<string, string> dictionary) {
            Identifier = identifier;
            _metadata = dictionary == null || dictionary.Count == 0 ? null : new Dictionary<string, string>(dictionary);
        }

        public int GetMetadataSize() {
            return _metadata?.Count ?? 0;
        }

        public string GetMetadata(string key) {
            return _metadata?[key];
        }

        public bool HasMetadata(string key) {
            return _metadata?.ContainsKey(key) ?? false;
        }

        public Dictionary<string, string> GetMetadataCopy() {
            return _metadata == null ? new Dictionary<string, string>() : new Dictionary<string, string>(_metadata);
        }

        public void AddMetadata(string key, string value) {
            _metadata = _metadata == null
                ? new Dictionary<string, string>()
                : new Dictionary<string, string>(_metadata);
            _metadata.Add(key, value);
        }

        public void RemoveMetadata(string key, string value) {
            if (_metadata == null) return;
            if (_metadata.Count == 1 && _metadata.ContainsKey(key)) {
                _metadata = null;
                return;
            }

            _metadata = new Dictionary<string, string>(_metadata);
            _metadata.Remove(key);
        }

        public void ClearMetadata() {
            _metadata = null;
        }

        public void ForEachMetadata(Action<string, string> action) {
            if (_metadata == null) return;
            foreach (var pair in _metadata) {
                action(pair.Key, pair.Value);
            }
        }

        public override string ToString() {
            return JsonUtility.ToJson(this);
        }
    }
}