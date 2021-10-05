﻿namespace Sources.Identification {
    public static class Identifiers {
        public static readonly Identifier ManagerBlock =
            new Identifier(Identifier.BlockDasherProvider, "manager_block");

        public static readonly Identifier Start = new Identifier(Identifier.BlockDasherProvider, "start");
        public static readonly Identifier End = new Identifier(Identifier.BlockDasherProvider, "end");
        
        public static readonly Identifier Grass = new Identifier(Identifier.BlockDasherProvider, "grass");
        public static readonly Identifier Sand = new Identifier(Identifier.BlockDasherProvider, "sand");
        public static readonly Identifier Snow = new Identifier(Identifier.BlockDasherProvider, "snow");
        
        public static readonly Identifier Flowers = new Identifier(Identifier.BlockDasherProvider, "flowers");
    }
}