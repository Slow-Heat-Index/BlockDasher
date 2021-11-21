using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour {
    private Image _image;
    [SerializeField] private Sprite[] sprites;

    private void Awake() {
        _image = GetComponent<Image>();
    }

    public void SetSprite(int i) {
        _image.sprite = sprites[i];
    }
}
