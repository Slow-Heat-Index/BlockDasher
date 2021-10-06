using Sources.Util;
using UnityEngine;

namespace Player.Behaviour {
    public class PlayerMovementBehaviour : MonoBehaviour {
        public void Dash(Direction direction) {
            (direction != Direction.Up && direction != Direction.Down)
                .ValidateTrue($"Direction cannot be up or down! {direction}");
        }
    }
}