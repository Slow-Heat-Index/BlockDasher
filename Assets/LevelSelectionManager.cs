using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour {
    private RectTransform _rectTransform;
    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = new Vector2(0, -_rectTransform.rect.height);
    }
}
