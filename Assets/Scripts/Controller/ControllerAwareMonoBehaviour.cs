using Sources.Util;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller {
    public abstract class ControllerAwareMonoBehaviour<T> : MonoBehaviour where T : IInputActionCollection {
        protected T Input;

        protected virtual void Awake() {
            Input = InitInput();
            Input.ValidateNotNull("Input cannot be null!");
        }

        protected virtual void OnEnable() {
            Input.Enable();
        }

        protected virtual void OnDisable() {
            Input.Disable();
        }

        protected abstract T InitInput();
    }
}