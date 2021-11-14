using Level.Player.Data;
using Sources.Util;

namespace Level.Player.Behaviour {
    public class DashData {
        public PlayerData Player { get; }
        public PlayerMovementBehaviour MovementBehaviour { get; }
        public Direction Direction { get; }
        public bool Cancelled { get; private set; }

        public DashData(PlayerData player, PlayerMovementBehaviour movementBehaviour, Direction direction) {
            Player = player;
            MovementBehaviour = movementBehaviour;
            Direction = direction;
            Cancelled = false;
        }

        public void Cancel() {
            Cancelled = true;
        }

        public DashData With(Direction direction) {
            return new DashData(Player, MovementBehaviour, direction);
        }
    }
}