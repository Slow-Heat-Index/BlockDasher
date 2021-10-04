using System;
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

        private void Start() {
            _rectTransform = GetComponent<RectTransform>();

            var list = Registry.Get<BlockType>(Identifiers.ManagerBlock).ToList();
            list.Sort((o1, o2) =>
                string.Compare(o1.Name, o2.Name, StringComparison.Ordinal));


            list.ForEach(AddElement);
            list.ForEach(AddElement);
            list.ForEach(AddElement);
            list.ForEach(AddElement);
            list.ForEach(AddElement);
        }


        private void AddElement(BlockType type) {
            var instance = Instantiate(prefab, transform);

            var display = instance.GetComponent<BlockTypeDisplay>();
            if (display != null) {
                display.BlockType = type;
            }

            var rectTransform = (RectTransform)instance.transform;
            var rect = rectTransform.rect;

            var slice = _rectTransform.rect.width / (2 * columns);
            rectTransform.pivot = new Vector2(slice * (_column + 1), _row * rect.height + rect.height / 2);


            _column++;
            if (_column == columns) {
                _column = 0;
                _row++;
            }
        }
    }
}