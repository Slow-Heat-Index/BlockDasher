using System;
using Sources.Util;

namespace Sources.Identification {
    public class Identifier {
        public const string BlockDasherProvider = "block_dasher";
        
        public readonly string Provider;
        public readonly string Key;

        public Identifier(string provider, string key) {
            provider.ValidateNotNull("Provider cannot be null!");
            key.ValidateNotNull("Key cannot be null!");
            Provider = provider;
            Key = key;
        }

        public Identifier(string id) {
            var split = id.IndexOf(':');
            if (split == -1) throw new ArgumentException("Id format must be {provider}:{key}!");
            Provider = id.Substring(0, split);
            Key = id.Substring(split + 1);
        }

        public override string ToString() {
            return Provider + ":" + Key;
        }

        protected bool Equals(Identifier other) {
            return Provider == other.Provider && Key == other.Key;
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