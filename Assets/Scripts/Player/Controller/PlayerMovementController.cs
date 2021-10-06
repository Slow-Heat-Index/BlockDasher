using System;
using Controller;
using Player.Behaviour;
using Sources.Util;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Controller {
    
    [RequireComponent(typeof(PlayerMovementBehaviour))]
    public class PlayerMovementController : ControllerAwareMonoBehaviour<Inputs> {
        private PlayerMovementBehaviour _behaviour;

        private Vector2 _touchscreenStart;

        protected override void Awake() {
            base.Awake();
            _behaviour = GetComponent<PlayerMovementBehaviour>();

            Input.Player.KeyboardUp.performed += OnKeyboardInput;
            Input.Player.KeyboardDown.performed += OnKeyboardInput;
            Input.Player.KeyboardLeft.performed += OnKeyboardInput;
            Input.Player.KeyboardRight.performed += OnKeyboardInput;

            Input.Player.TouchscreenPress.performed += OnTouchscreenPress;
            Input.Player.TouchscreenPress.canceled += OnTouchscreenEndsPress;
        }

        protected override Inputs InitInput() => new Inputs();

        protected void OnKeyboardInput(InputAction.CallbackContext context) {
            var direction = GetDirectionByAction(context.action);
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
            _touchscreenStart = Input.Player.TouchscreenPosition.ReadValue<Vector2>();
        }

        protected void OnTouchscreenEndsPress(InputAction.CallbackContext context) {
            var end = Input.Player.TouchscreenPosition.ReadValue<Vector2>();
            var delta = end - _touchscreenStart;
            if (delta.sqrMagnitude < 10000) return;

            var direction = Direction.North;
            var dot = -2.0f;

            DirectionUtils.ForEach(current => {
                if (current == Direction.Up || current == Direction.Down) return;
                var result = Vector2.Dot(current.Get2DVector(), delta);
                if (!(result > dot)) return;
                direction = current;
                dot = result;
            });

            _behaviour.Dash(direction);
        }
    }
}