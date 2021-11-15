using System;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameEditor {
    [RequireComponent(typeof(InputField))]
    public class RankingTextField : MonoBehaviour {
        public RankingType rankingType;
        private InputField _textField;
        private int _data = -1;
        private EditorData _editorData;

        private void Start() {
            _editorData = FindObjectOfType<EditorData>();
            _textField = GetComponent<InputField>();
        }

        private void FixedUpdate() {
            var data = Get();
            if (data == _data) return;
            _data = data;
            _textField.text = _data.ToString();
        }


        public void OnInput() {
            if (int.TryParse(_textField.text, out var number)) {
                if (number > 0) {
                    _data = number;
                    Set(_data);
                    return;
                }
            }

            _textField.SetTextWithoutNotify(_data.ToString());
        }


        private int Get() {
            switch (rankingType) {
                case RankingType.Gold:
                    return _editorData.World.GoldMoves;
                case RankingType.Silver:
                    return _editorData.World.SilverMoves;
                case RankingType.Bronze:
                    return _editorData.World.BronzeMoves;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Set(int number) {
            switch (rankingType) {
                case RankingType.Gold:
                    _editorData.World.GoldMoves = number;
                    break;
                case RankingType.Silver:
                    _editorData.World.SilverMoves = number;
                    break;
                case RankingType.Bronze:
                    _editorData.World.BronzeMoves = number;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum RankingType {
        Gold,
        Silver,
        Bronze
    }
}