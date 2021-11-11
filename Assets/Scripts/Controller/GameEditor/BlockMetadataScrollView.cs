using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Level.Blocks;
using Sources.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameEditor {
    public class BlockMetadataScrollView : MonoBehaviour {
        private static readonly Dictionary<Type, Action<BlockMetadataScrollView, string, string, string>> Functions =
            new Dictionary<Type, Action<BlockMetadataScrollView, string, string, string>> {
                { typeof(bool), (m, k, v, n) => m.GenerateBooleanForm(k, v, n) },
                { typeof(Direction), (m, k, v, n) => m.GenerateDirectionForm(k, v, n) },
                { typeof(TreeBlock.TreeType), (m, k, v, n) => m.GenerateEnumForm<TreeBlock.TreeType>(k, v, n) },
                { typeof(SkullBlock.SkullType), (m, k, v, n) => m.GenerateEnumForm<SkullBlock.SkullType>(k, v, n) }
            };

        public GameObject booleanForm;
        public GameObject enumForm;

        private readonly List<GameObject> _children = new List<GameObject>();
        private EditorData _editorData;

        private void Start() {
            _editorData = FindObjectOfType<EditorData>();
            RefreshElements(_editorData.Metadata);
            _editorData.OnMetadataChange += RefreshElements;
        }

        private void OnDestroy() {
            _editorData.OnMetadataChange -= RefreshElements;
        }

        private void RefreshElements(Dictionary<string, string> metadata) {
            foreach (var child in _children) {
                Destroy(child);
            }

            foreach (var pair in _editorData.SelectedBlockType.DefaultMetadata) {
                var key = pair.Key;
                var value = pair.Value.Value;
                var type = pair.Value.Type;
                var keyName = pair.Value.Name;

                if (metadata.TryGetValue(key, out var other)) {
                    value = other;
                }

                if (Functions.TryGetValue(type, out var function)) {
                    function(this, key, value, keyName);
                }
            }
        }

        private void GenerateBooleanForm(string key, string value, string keyName) {
            var form = Instantiate(booleanForm, transform);

            var toggle = form.GetComponentInChildren<Toggle>();
            var text = form.GetComponentInChildren<Text>();
            text.text = keyName;
            toggle.isOn = bool.TryParse(value, out var result) && result;
            toggle.onValueChanged.AddListener(b => _editorData.Metadata[key] = b.ToString());

            _children.Add(form);
        }

        private void GenerateEnumForm<T>(string key, string value, string keyName) where T : Enum {
            var form = Instantiate(enumForm, transform);

            var drop = form.GetComponentInChildren<Dropdown>();

            var text = form.GetComponentInChildren<Text>();
            text.text = keyName;

            var values = Enum.GetValues(typeof(T));
            var list = (from int o in values select Enum.GetName(typeof(T), o)).ToList();
            drop.AddOptions(list);
            drop.value = list.IndexOf(value);
            drop.onValueChanged.AddListener(i => _editorData.Metadata[key] = list[i]);

            _children.Add(form);
        }

        private void GenerateDirectionForm(string key, string value, string keyName) {
            var form = Instantiate(enumForm, transform);

            var drop = form.GetComponentInChildren<Dropdown>();
            var text = form.GetComponentInChildren<Text>();
            text.text = keyName;

            var list = new List<string> { "North", "East", "South", "West" };
            drop.AddOptions(list);
            drop.value = list.IndexOf(value);
            drop.onValueChanged.AddListener(i => _editorData.Metadata[key] = list[i]);

            _children.Add(form);
        }
    }
}