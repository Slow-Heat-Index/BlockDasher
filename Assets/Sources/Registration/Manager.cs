using System.Collections.Generic;
using Sources.Identification;

namespace Sources.Registration {
    public class Manager<T> : Dictionary<Identifier, T>, IManager where T : IIdentifiable {
        public Identifier Identifier { get; }

        public Manager(Identifier identifier) {
            Identifier = identifier;
        }
    }
}