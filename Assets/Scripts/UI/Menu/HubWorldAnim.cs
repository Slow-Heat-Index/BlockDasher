using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class HubWorldAnim : MonoBehaviour {
    public UnityEvent OnLevelUp;
    [SerializeField] private float _tweenTime;
    [SerializeField] private Transform initialPos;

    private void Awake() {
        transform.position += Vector3.back * 10;
    }

    public void PopUp() {
        transform.position = initialPos.position;
        transform.DOMoveY(0, _tweenTime);
        transform.DORotate(new Vector3(0, 180, 0), _tweenTime, RotateMode.Fast)
            .SetEase(Ease.Linear)
            .OnComplete(() => OnLevelUp.Invoke());
        
    }

    public void PopDown() {
        transform.DOMove(initialPos.position, _tweenTime);
        transform.DORotate(Vector3.zero, _tweenTime, RotateMode.Fast)
            .SetEase(Ease.Linear).OnComplete(() => transform.position += Vector3.back*10);
    }


}
