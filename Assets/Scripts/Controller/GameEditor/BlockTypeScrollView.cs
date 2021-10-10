using System;
using Sources.Identification;
using Sources.Level;
using Sources.Registration;
using UnityEngine;

namespace Controller.GameEditor {
    [RequireComponent(typeof(RectTransform))]
    public class BlockTypeScrollView : MonoBehaviour {
        public GameObject prefab;


        private void Start() {
            var list = Registry.Get<BlockType>(Identifiers.ManagerBlock).ToList();
            list.Sort((o1, o2) =>
                string.Compare(o1.Name, o2.Name, StringComparison.Ordinal));

            list.ForEach(AddElement);
        }

        private void AddElement(BlockType type) {
            var instance = Instantiate(prefab, transform);
            var display = instance.GetComponent<BlockTypeDisplay>();
            if (display != null) {
                display.BlockType = type;
            }
        }
    }
}