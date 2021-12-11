using Sources.Identification;
using Sources.Registration;

namespace Sources.Level.Manager {
    public class LevelManager : Manager<LevelSnapshot> {
        public static readonly LevelSnapshot Level11 =
            new LevelSnapshot(Identifiers.Level11, "Levels/level_1_1", Identifiers.Level12, true);

        public LevelManager() : base(Identifiers.ManagerLevel) {
            Register(Level11);
            Register(new LevelSnapshot(Identifiers.Level12, "Levels/level_1_2", Identifiers.Level13));
            Register(new LevelSnapshot(Identifiers.Level13, "Levels/level_1_3", Identifiers.Level14));
            Register(new LevelSnapshot(Identifiers.Level14, "Levels/level_1_4", Identifiers.Level15));
            Register(new LevelSnapshot(Identifiers.Level15, "Levels/level_1_5"));

            Register(new LevelSnapshot(Identifiers.Level21, "Levels/level_2_1", Identifiers.Level22));
            Register(new LevelSnapshot(Identifiers.Level22, "Levels/level_2_2", Identifiers.Level23));
            Register(new LevelSnapshot(Identifiers.Level23, "Levels/level_2_3", Identifiers.Level24));
            Register(new LevelSnapshot(Identifiers.Level24, "Levels/level_2_4", Identifiers.Level25));
            Register(new LevelSnapshot(Identifiers.Level25, "Levels/level_2_5"));

            Register(new LevelSnapshot(Identifiers.Level31, "Levels/level_3_1", Identifiers.Level32));
            Register(new LevelSnapshot(Identifiers.Level32, "Levels/level_3_2", Identifiers.Level33));
            Register(new LevelSnapshot(Identifiers.Level33, "Levels/level_3_3", Identifiers.Level34));
            Register(new LevelSnapshot(Identifiers.Level34, "Levels/level_3_4", Identifiers.Level35));
            Register(new LevelSnapshot(Identifiers.Level35, "Levels/level_3_5"));
        }
    }
}