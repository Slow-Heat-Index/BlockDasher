using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DisappearSmoothly : MonoBehaviour {
    [SerializeField] private float tweenTime;
    private Vector2 _previousSize;
    private RectTransform _rectTransform;

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        _previousSize = _rectTransform.localScale;
        
    }

    public void Play() {
        _rectTransform.DOScale(Vector3.zero, tweenTime)
            .OnComplete(() => {
                _rectTransform.localScale = _previousSize;
                gameObject.SetActive(false);
            });
    }
}
