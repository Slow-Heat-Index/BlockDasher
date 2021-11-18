using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class ScreensTransitions : MonoBehaviour {
    private RectTransform _rectTransform;
    [SerializeField] private float tweenTime;
    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void ScreenIn() {
        _rectTransform.DOAnchorPosY(0, tweenTime);
    }

    public void ScreenDown() {
        _rectTransform.DOAnchorPosY(-_rectTransform.rect.height, tweenTime);
    }
    
    public void ScreenUp() {
        _rectTransform.DOAnchorPosY(_rectTransform.rect.height, tweenTime);
    }

    public float GetTweenTime() {
        return tweenTime;
    }
}
