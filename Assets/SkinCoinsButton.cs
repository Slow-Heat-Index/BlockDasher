using Data;
using Sources.Identification;
using UnityEngine;
using UnityEngine.UI;

public class SkinCoinsButton : MonoBehaviour {
    [SerializeField] private int price;
    [SerializeField] private Identifier skin;

    private Button _button;

    private void Awake() {
        _button = GetComponent<Button>();
    }

    public void Pressed() {
        var data = PersistentDataContainer.PersistentData;
        if (data.availableSkins.Contains(skin)) return;
        if (data.coins < price) return;
        PersistentDataContainer.PersistentData.ModifyCoins(-price);
        data.availableSkins.Add(skin);
        FindObjectOfType<SkinStorageScrollView>().Refresh();
    }
}