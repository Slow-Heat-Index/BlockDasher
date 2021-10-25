namespace Sources.Identification {
    public static class Identifiers {
        public static readonly Identifier ManagerBlock =
            new Identifier(Identifier.BlockDasherProvider, "manager_block");

        public static readonly Identifier Start = new Identifier(Identifier.BlockDasherProvider, "start");
        public static readonly Identifier End = new Identifier(Identifier.BlockDasherProvider, "end");
        
        public static readonly Identifier Grass = new Identifier(Identifier.BlockDasherProvider, "grass");
        public static readonly Identifier Sand = new Identifier(Identifier.BlockDasherProvider, "sand");
        public static readonly Identifier Snow = new Identifier(Identifier.BlockDasherProvider, "snow");
        
        public static readonly Identifier Fence = new Identifier(Identifier.BlockDasherProvider, "fence");
        public static readonly Identifier FenceCorner = new Identifier(Identifier.BlockDasherProvider, "fence_corner");
        
        public static readonly Identifier Flowers = new Identifier(Identifier.BlockDasherProvider, "flowers");
        public static readonly Identifier Tree = new Identifier(Identifier.BlockDasherProvider, "tree");
        public static readonly Identifier TallGrass = new Identifier(Identifier.BlockDasherProvider, "tall_grass");
        public static readonly Identifier Sign = new Identifier(Identifier.BlockDasherProvider, "sign");
    }
}