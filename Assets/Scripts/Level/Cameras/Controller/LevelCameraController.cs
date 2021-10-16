using Controller;
using Level.Cameras.Behaviour;
using UnityEngine;

namespace Level.Cameras.Controller {
    [RequireComponent(typeof(LevelCameraBehaviour))]
    public class LevelCameraController : ControllerAwareMonoBehaviour<Inputs> {
        private LevelCameraBehaviour _behaviour;

        protected override void Awake() {
            base.Awake();
            _behaviour = GetComponent<LevelCameraBehaviour>();
            Input.Camera.RotateLeft.performed += _ => _behaviour.RotateLeft();
            Input.Camera.RotateRight.performed += _ => _behaviour.RotateRight();
        }

        protected override Inputs InitInput() => new Inputs();
    }
}