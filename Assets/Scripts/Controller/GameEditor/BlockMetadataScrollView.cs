using System;
using System.Collections.Generic;
using Sources;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameEditor {
    public class BlockMetadataScrollView : MonoBehaviour {
        private static readonly Dictionary<Type, Action<BlockMetadataScrollView, string, string>> _functions =
            new Dictionary<Type, Action<BlockMetadataScrollView, string, string>> {
                { typeof(bool), (m, k, v) => m.GenerateBooleanForm(k, v) }
            };

        public GameObject booleanForm;

        private readonly List<GameObject> _children = new List<GameObject>();

        private void Start() {
            RefreshElements(EditorData.Metadata);
            EditorData.OnMetadataChange += RefreshElements;
        }

        private void OnDestroy() {
            EditorData.OnMetadataChange -= RefreshElements;
        }

        private void RefreshElements(Dictionary<string, string> metadata) {
            foreach (var child in _children) {
                Destroy(child);
            }

            for (var i = 0; i < 5; i++) {
                foreach (var pair in EditorData.SelectedBlockType.DefaultMetadataValues) {
                    var key = pair.Key;
                    var value = pair.Value;

                    if (metadata.TryGetValue(key, out var other)) {
                        value = other;
                    }

                    var type = EditorData.SelectedBlockType.DefaultMetadataTypes[key];
                    if (_functions.TryGetValue(type, out var function)) {
                        function(this, key, value);
                    }
                }
            }
        }

        private void GenerateBooleanForm(string key, string value) {
            var form = Instantiate(booleanForm, transform);

            var toggle = form.GetComponentInChildren<Toggle>();
            var text = form.GetComponentInChildren<Text>();
            text.text = key;
            toggle.isOn = bool.TryParse(value, out var result) && result;
            toggle.onValueChanged.AddListener(b => EditorData.Metadata[key] = b.ToString());

            _children.Add(form);
        }
    }
}