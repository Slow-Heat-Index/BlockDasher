using System;
using System.Collections.Generic;
using Sources.Identification;
using Sources.Util;

namespace Sources.Registration {
    public class Manager<T> : IManager where T : IIdentifiable {
        protected Dictionary<Identifier, T> _dictionary = new Dictionary<Identifier, T>();
        public Identifier Identifier { get; }
        public Type ManagedType => typeof(T);

        public Manager(Identifier identifier) {
            Identifier = identifier;
        }


        public T Get(Identifier identifier) {
            identifier.ValidateNotNull("Identifier cannot be null!");
            return _dictionary[identifier];
        }

        public bool Contains(Identifier identifier) {
            return identifier != null && _dictionary.ContainsKey(identifier);
        }

        public bool Register(T element) {
            element.ValidateNotNull("Element cannot be null!");
            if (_dictionary.ContainsKey(element.Identifier)) return false;
            _dictionary[element.Identifier] = element;
            return true;
        }

        public bool Unregister(Identifier identifier) {
            identifier.ValidateNotNull("Identifier cannot be null!");
            return _dictionary.Remove(identifier);
        }

        public void ForEach(Action<Identifier, T> action) {
            action.ValidateNotNull("Action cannot be null!o");
            foreach (var pair in _dictionary) {
                action(pair.Key, pair.Value);
            }
        }

        public T this[Identifier identifier] => Get(identifier);
    }
}