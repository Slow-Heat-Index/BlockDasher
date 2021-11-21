using System;
using Sources.Util;
using UnityEngine;

namespace Sources.Identification {
    [Serializable]
    public class Identifier {
        public const string BlockDasherProvider = "block_dasher";

        [SerializeField] private string provider;
        [SerializeField] private string key;

        public string Provider => provider;

        public string Key => key;

        public Identifier(string provider, string key) {
            provider.ValidateNotNull("Provider cannot be null!");
            key.ValidateNotNull("Key cannot be null!");
            this.provider = provider;
            this.key = key;
        }

        public Identifier(string id) {
            var split = id.IndexOf(':');
            if (split == -1) throw new ArgumentException("Id format must be {provider}:{key}!");
            provider = id.Substring(0, split);
            key = id.Substring(split + 1);
        }

        public override string ToString() {
            return provider + ":" + key;
        }

        protected bool Equals(Identifier other) {
            return provider == other.provider && key == other.key;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Identifier)obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((Provider != null ? Provider.GetHashCode() : 0) * 397) ^ (Key != null ? Key.GetHashCode() : 0);
            }
        }

        public static bool operator ==(Identifier left, Identifier right) {
            return Equals(left, right);
        }

        public static bool operator !=(Identifier left, Identifier right) {
            return !Equals(left, right);
        }
    }
}