using System.Collections.Generic;
using System.Linq;
using Sources.Identification;
using Sources.Level.Skyboxes;
using Sources.Registration;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameEditor {
    [RequireComponent(typeof(Dropdown))]
    public class SkyboxComboBox : MonoBehaviour {
        private EditorData _editorData;
        private Dropdown _dropdown;
        private SkyboxWrapper _wrapper;
        private List<string> _identifiers;

        private void Start() {
            _editorData = FindObjectOfType<EditorData>();

            _dropdown = GetComponent<Dropdown>();

            _wrapper = _editorData.World.Skybox;

            var values = Registry.Get<SkyboxWrapper>(Identifiers.ManagerSkybox).ToList();
            _identifiers = (from SkyboxWrapper o in values select o.Identifier.ToString()).ToList();
            var list = (from SkyboxWrapper o in values select o.Name).ToList();
            _dropdown.AddOptions(list);
            _dropdown.value = _identifiers.IndexOf(_wrapper.Identifier.ToString());
            _dropdown.onValueChanged.AddListener(i => {
                _editorData.World.Skybox = values[i];
                RenderSettings.skybox = values[i].Skybox;
            });
        }

        private void FixedUpdate() {
            if (_editorData.World.Skybox.Identifier != _wrapper.Identifier) {
                _wrapper = _editorData.World.Skybox;
                _dropdown.value = _identifiers.IndexOf(_wrapper.Identifier.ToString());
            }
        }
    }
}