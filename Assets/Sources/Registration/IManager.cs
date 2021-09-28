using System;
using Sources.Identification;

namespace Sources.Registration {
    internal interface IManager : IIdentifiable {
        Type ManagedType { get; }
    }
}