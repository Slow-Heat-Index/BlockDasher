using Data;
using UnityEngine;
using UnityEngine.UI;

public class CoinsButton : MonoBehaviour {
    [SerializeField] private int value;
    [SerializeField] private bool onlyOnce;
    private Button _button;

    private void Awake() {
        _button = GetComponent<Button>();
    }

    public void Pressed() {
        PersistentDataContainer.PersistentData.ModifyCoins(value);
        if (onlyOnce) {
            _button.enabled = false;
        }
    }
}