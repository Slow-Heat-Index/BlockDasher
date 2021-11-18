using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource), typeof(Button), typeof(RectTransform))]
public class ButtonFeedBack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private AudioSource _audioSource;
    private Button _button;
    private EventTrigger _eventTrigger;
    private RectTransform _rectTransform;
    private Vector2 _size;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _button = GetComponent<Button>();
        _eventTrigger = GetComponent<EventTrigger>();
        _rectTransform = GetComponent<RectTransform>();
        _size = _rectTransform.localScale;

        
    }

    private void OnEnable() {
        _button.onClick.AddListener(_audioSource.Play);
    }
    
    private void OnDisable() {
        _button.onClick.AddListener(_audioSource.Play);
    }
    

    public void OnPointerEnter(PointerEventData eventData) {
        _rectTransform.DOScale(_size * 1.2f, 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData) {
        _rectTransform.DOScale(_size, 0.3f);
    }
}
