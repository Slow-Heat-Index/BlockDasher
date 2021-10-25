using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldSelection : MonoBehaviour {
    [SerializeField] public HubWorld[] worlds;
    [SerializeField] private int currentWorld;
    
    private int previousGameWorld;
    private RectTransform _rectTransform;
    private TitleScreen _titleScreen;
    [SerializeField] private TextMeshProUGUI worldName;

    [SerializeField] private Button nextWorldb;
    [SerializeField] private Button previousWorldb;
    private ScreensTransitions _screensTransitions;

    
    private void Awake() {
        _titleScreen = FindObjectOfType<TitleScreen>();
        currentWorld = previousGameWorld;
        worldName.text = worlds[currentWorld].worldName;
        _rectTransform = GetComponent<RectTransform>();
        _screensTransitions = GetComponent<ScreensTransitions>();

        if (currentWorld == 0)
            previousWorldb.interactable = false;
        
        if(currentWorld == worlds.Length-1)
            nextWorldb.interactable = false;
        
        _rectTransform.anchoredPosition = new Vector2(0, -_rectTransform.rect.height);
    }

    private void ShowPreviousGameWorld() {
        worlds[previousGameWorld].hubWorldAnim.PopUp();
    }

    public void ShowNext() {
        var previous = currentWorld;
        currentWorld++;
        
        worlds[currentWorld].hubWorldAnim.PopUp();
        worlds[previous].hubWorldAnim.PopDown();
        worldName.text = worlds[currentWorld].worldName;
        
        if (currentWorld == worlds.Length - 1) {
            nextWorldb.interactable = false;
        }
        previousWorldb.interactable = true;
    }

    public void ShowPrevious() {
        var previous = currentWorld;
        currentWorld--;
        
        worlds[currentWorld].hubWorldAnim.PopUp();
        worlds[previous].hubWorldAnim.PopDown();
        worldName.text = worlds[currentWorld].worldName;
        
        if (currentWorld == 0) {
            previousWorldb.interactable = false;
        }
        nextWorldb.interactable = true;
    }

    public int GetCurrentWorld() {
        return currentWorld;
    }


    private void OnEnable() {
        _titleScreen.onGameBegun.AddListener(ShowPreviousGameWorld);
        _titleScreen.onGameBegun.AddListener(_screensTransitions.ScreenIn);
    }

    private void OnDisable() {
        _titleScreen.onGameBegun.RemoveListener(ShowPreviousGameWorld);
        _titleScreen.onGameBegun.RemoveListener(_screensTransitions.ScreenIn);
    }
}
