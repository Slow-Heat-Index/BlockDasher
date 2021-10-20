using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameEditor {
    [RequireComponent(typeof(InputField))]
    class MovementsTextField : MonoBehaviour {
        private InputField _textField;
        private uint _movements = 20u;
        private EditorData _editorData;

        private void Start() {
            _editorData = FindObjectOfType<EditorData>();
            _textField = GetComponent<InputField>();
        }

        private void FixedUpdate() {
            if (_editorData.World.InitialMoves == _movements) return;
            _movements = _editorData.World.InitialMoves;
            _textField.text = _movements.ToString();
        }


        public void OnInput() {
            if (int.TryParse(_textField.text, out var number)) {
                if (number > 0) {
                    _movements = (uint)number;
                    _editorData.World.InitialMoves = _movements;
                    return;
                }
            }

            _textField.SetTextWithoutNotify(_movements.ToString());
        }
    }
}