using System;
using Data;
using Sources.Identification;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SkinCoinsButton : MonoBehaviour {
    [SerializeField] private int price;
    [SerializeField] private Identifier skin;
    [SerializeField] private Sprite boughtSprite;

    private Button _button;
    private Image _image;

    private void Awake() {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    private void Start() {
        var present = PersistentDataContainer.PersistentData.availableSkins.Contains(skin);
        if (present) {
            _image.sprite = boughtSprite;
        }
    }

    public void Pressed() {
        var data = PersistentDataContainer.PersistentData;
        if (data.availableSkins.Contains(skin)) return;
        if (data.coins < price) return;
        PersistentDataContainer.PersistentData.ModifyCoins(-price);
        data.availableSkins.Add(skin);
        _image.sprite = boughtSprite;
        FindObjectOfType<SkinStorageScrollView>().Refresh();
    }
}