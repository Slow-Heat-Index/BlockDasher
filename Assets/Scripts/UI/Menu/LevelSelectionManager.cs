using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour {
    private RectTransform _rectTransform;
    public int currentWorld;
    [SerializeField] private HubMovement _hubMovement;
    private WorldSelection _worldSelection;
    [SerializeField] private GameObject _worlds;

    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;
    
    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = new Vector2(0, -_rectTransform.rect.height);
        _worldSelection = FindObjectOfType<WorldSelection>();
        _previousButton.interactable = false;
    }

    public void SetLevelSelector() {
        currentWorld = _worldSelection.GetCurrentWorld();
        _hubMovement = _worlds.transform.GetChild(currentWorld).GetComponent<HubWorld>().hubMovement;
        _hubMovement.InitEverything();
    }
    
    public void Next() {
        _hubMovement.GoNext();

        if (_hubMovement.GetCurrentLevel() == _hubMovement.GetNumLevels()-1) {
            _nextButton.interactable = false;
        }

        _previousButton.interactable = true;
    }

    public void Previous() {
        _hubMovement.GoPrevious();
        
        if (_hubMovement.GetCurrentLevel() == 0) {
            _previousButton.interactable = false;
        }

        _nextButton.interactable = true;
    }
    
    
    
}
