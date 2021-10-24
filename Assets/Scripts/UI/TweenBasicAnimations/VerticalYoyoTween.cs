using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class VerticalYoyoTween : MonoBehaviour {
    [SerializeField] private float _offset;
    [SerializeField] private float _tweenTime;
    
    private RectTransform _rectTransform;
    

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start() {
        YoyoMove();
    }


    void YoyoMove() {
        _rectTransform.DOAnchorPosY(_rectTransform.anchoredPosition.y + _offset, _tweenTime)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
