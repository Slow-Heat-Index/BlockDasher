using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.Events;

public class TitleScreen : MonoBehaviour {
    public UnityEvent onGameBegun;
    private RectTransform _rectTransform;
    [SerializeField] private HubWorldAnim hubWorldAnim;
    

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        
    }

    
    void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame) {
            _rectTransform.DOAnchorPosY(_rectTransform.rect.height, 0.5f)
                .OnComplete(() => {
                    gameObject.SetActive(false);
                    onGameBegun.Invoke();
                });
            
        }
    }

    private void OnEnable() {
        onGameBegun.AddListener(() => gameObject.SetActive(false));
    }

    private void OnDisable() {
        onGameBegun.RemoveListener(() => gameObject.SetActive(false));
    }
}
