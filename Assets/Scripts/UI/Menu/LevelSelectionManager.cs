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
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private GameObject _levelData;
    
    
    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = new Vector2(0, -_rectTransform.rect.height);
        _worldSelection = FindObjectOfType<WorldSelection>();
        _previousButton.interactable = false;
    }

    public void SetLevelSelector() {
        currentWorld = _worldSelection.GetCurrentWorld();
        _hubMovement = _worlds.transform.GetChild(currentWorld).GetComponent<HubWorld>().hubMovement;
        _hubMovement.OnLevelReached.AddListener(ShowUI);
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

    public void HideUI() {
        _nextButton.gameObject.SetActive(false);
        _previousButton.gameObject.SetActive(false);
        _playButton.gameObject.SetActive(false);
        _levelData.SetActive(false);
        _backButton.gameObject.SetActive(false);
    }
    
    public void ShowUI() {
        _nextButton.gameObject.SetActive(true);
        _previousButton.gameObject.SetActive(true);
        _playButton.gameObject.SetActive(true);
        _levelData.SetActive(true);
        _backButton.gameObject.SetActive(true);
    }
    
    
    
}
