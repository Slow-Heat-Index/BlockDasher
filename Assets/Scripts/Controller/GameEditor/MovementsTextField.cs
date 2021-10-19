using Sources;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameEditor {
    [RequireComponent(typeof(InputField))]
    class MovementsTextField : MonoBehaviour {
        private InputField _textField;
        private uint _movements = 20u;

        private void Start() {
            _textField = GetComponent<InputField>();
        }

        private void FixedUpdate() {
            if (EditorData.World.InitialMoves == _movements) return;
            _movements = EditorData.World.InitialMoves;
            _textField.text = _movements.ToString();
        }


        public void OnInput() {
            if (int.TryParse(_textField.text, out var number)) {
                if (number > 0) {
                    _movements = (uint)number;
                    EditorData.World.InitialMoves = _movements;
                    return;
                }
            }

            _textField.SetTextWithoutNotify(_movements.ToString());
        }
    }
}