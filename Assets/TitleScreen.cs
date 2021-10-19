using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class TitleScreen : MonoBehaviour {
    [SerializeField] private RectTransform _hubRt;
    private RectTransform _rectTransform;
    [SerializeField] private HubWorldAnim hubWorldAnim;
    

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        
    }

    
    void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame) {
            _rectTransform.DOAnchorPosY(_rectTransform.rect.height, 1)
                .OnComplete(() => {
                    gameObject.SetActive(false);
                    hubWorldAnim.PopUp();
                });
            _hubRt.DOAnchorPosY(0, 1);
            
        }
    }
}
