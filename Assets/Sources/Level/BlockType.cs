using Sources.Identification;
using Sources.Level.Data;

namespace Sources.Level {
    public abstract class BlockType : IIdentifiable {
        public Identifier Identifier { get; }

        protected BlockType(Identifier identifier) {
            Identifier = identifier;
        }

        public abstract Block CreateBlock(BlockPosition position, BlockData data);
    }
}