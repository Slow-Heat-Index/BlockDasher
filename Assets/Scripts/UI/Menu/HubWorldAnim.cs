using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HubWorldAnim : MonoBehaviour {
    [SerializeField] private float _tweenTime;
    [SerializeField] private Transform initialPos;

   

    public void PopUp() {
        transform.DOMove(Vector3.zero, _tweenTime);
        transform.DORotate(new Vector3(0, 180, 0), _tweenTime, RotateMode.Fast)
            .SetEase(Ease.Linear)
            .OnComplete(Idle);
    }

    public void PopDown() {
        transform.DOMove(initialPos.position, _tweenTime);
        transform.DORotate(Vector3.zero, _tweenTime, RotateMode.Fast)
            .SetEase(Ease.Linear)
            .OnComplete(Idle);
    }

    void Idle() {
        
    }
}
