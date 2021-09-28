using Sources.Level.Data;
using Sources.Level.Raycast;
using UnityEngine;

namespace Level {
    public class BlockClicker : MonoBehaviour {
        private Camera _camera;

        private void Start() {
            _camera = GetComponent<Camera>();
        }

        private void Update() {
            if (Input.GetMouseButton(0)) {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                var caster = new BlockRaycaster(Test.World, ray.origin, ray.direction, 100);
                caster.Run();

                if (caster.Result != null) {
                    var position = caster.Result.Position;
                    position.World.PlaceBlock(new BlockData(null), position.Position);
                }
            }
        }
    }
}