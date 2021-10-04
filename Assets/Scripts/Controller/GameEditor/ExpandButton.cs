using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameEditor {
    [RequireComponent(typeof(Button))]
    public class ExpandButton : MonoBehaviour {
        public GameObject scrollView;

        private Button _button;
        private bool _expanded = true;

        private void Start() {
            _button = GetComponent<Button>();
            var rect = (RectTransform)transform;
            var sRect = (RectTransform)scrollView.transform;
            _button.onClick.AddListener(() => {
                if (_expanded) {
                    var local = rect.anchoredPosition;
                    local.x = rect.rect.width / 2;
                    rect.anchoredPosition = local;
                    sRect.anchoredPosition = new Vector2(-10000, 0);
                }
                else {
                    var local = rect.anchoredPosition;
                    local.x = sRect.rect.width + rect.rect.width / 2;
                    rect.anchoredPosition = local;
                    sRect.anchoredPosition = new Vector2(sRect.rect.width / 2, 0);
                }
                _expanded = !_expanded;
            });
        }
    }
}