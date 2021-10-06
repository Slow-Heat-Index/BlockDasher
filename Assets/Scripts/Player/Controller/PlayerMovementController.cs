using System;
using Controller;
using Player.Behaviour;
using Sources.Util;
using UnityEngine.InputSystem;

namespace Player.Controller {
    public class PlayerMovementController : ControllerAwareMonoBehaviour<Inputs> {
        private PlayerMovementBehaviour _behaviour;

        protected override void Awake() {
            base.Awake();
            _behaviour = InitBehaviour();

            Input.Player.KeyboardUp.performed += OnKeyboardInput;
            Input.Player.KeyboardDown.performed += OnKeyboardInput;
            Input.Player.KeyboardLeft.performed += OnKeyboardInput;
            Input.Player.KeyboardRight.performed += OnKeyboardInput;

            Input.Player.TouchscreenPress.performed += OnTouchscreenPress;
            Input.Player.TouchscreenPress.canceled += OnTouchscreenEndsPress;
        }

        protected virtual PlayerMovementBehaviour InitBehaviour() {
            return gameObject.AddComponent<PlayerMovementBehaviour>();
        }

        protected override Inputs InitInput() => new Inputs();

        protected void OnKeyboardInput(InputAction.CallbackContext context) {
            var direction = GetDirectionByAction(context.action);
            print(direction);
            _behaviour.Dash(direction);
        }

        protected Direction GetDirectionByAction(InputAction action) {
            if (action == Input.Player.KeyboardUp) return Direction.North;
            if (action == Input.Player.KeyboardDown) return Direction.South;
            if (action == Input.Player.KeyboardLeft) return Direction.West;
            if (action == Input.Player.KeyboardRight) return Direction.East;
            throw new NotSupportedException();
        }

        protected void OnTouchscreenPress(InputAction.CallbackContext context) {
            print("uwu");
        }

        protected void OnTouchscreenEndsPress(InputAction.CallbackContext context) {
            print("owo");
        }
    }
}