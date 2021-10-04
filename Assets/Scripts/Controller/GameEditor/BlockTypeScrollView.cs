using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Identification;
using Sources.Level;
using Sources.Registration;
using UnityEngine;

namespace Controller.GameEditor {
    [RequireComponent(typeof(RectTransform))]
    public class BlockTypeScrollView : MonoBehaviour {
        public int columns = 3;
        public GameObject prefab;

        private RectTransform _rectTransform;
        private int _row, _column;

        private Dictionary<Vector2Int, RectTransform> _transforms = new Dictionary<Vector2Int, RectTransform>();

        private void Start() {
            _rectTransform = GetComponent<RectTransform>();

            var list = Registry.Get<BlockType>(Identifiers.ManagerBlock).ToList();
            list.Sort((o1, o2) =>
                string.Compare(o1.Name, o2.Name, StringComparison.Ordinal));
            
            list.ForEach(AddElement);
        }


        private void Update() {
            var contentWidth = _rectTransform.rect.width;
            var slice = contentWidth / columns;

            foreach (var pair in _transforms) {
                var rect = pair.Value.rect;
                pair.Value.localPosition = new Vector2(
                    pair.Key.x * slice + slice / 2,
                    -pair.Key.y * rect.height - rect.height / 2
                );
            }
        }

        private void AddElement(BlockType type) {
            var instance = Instantiate(prefab, transform);

            var display = instance.GetComponent<BlockTypeDisplay>();
            if (display != null) {
                display.BlockType = type;
            }

            var rectTransform = (RectTransform)instance.transform;
            _transforms.Add(new Vector2Int(_column, _row), rectTransform);

            _column++;
            if (_column == columns) {
                _column = 0;
                _row++;
            }
            
            if (_transforms.Count == 0) {
                _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
            }
            else {
                // ReSharper disable once PossibleLossOfFraction
                var height = (_transforms.Count / 3 + 1) * _transforms.First().Value.rect.height;
                _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            }
        }
    }
}