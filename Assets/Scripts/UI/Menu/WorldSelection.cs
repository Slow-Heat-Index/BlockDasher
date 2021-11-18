using System;
using System.Collections;
using System.Collections.Generic;
using Data;
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
    [SerializeField] private Button selectWorldb;
    [SerializeField] private GameObject worldLocked;
    private ScreensTransitions _screensTransitions;

    
    private void Awake() {
        _titleScreen = FindObjectOfType<TitleScreen>();
        currentWorld = previousGameWorld;
        worldName.text = worlds[currentWorld].worldName;
        _rectTransform = GetComponent<RectTransform>();
        _screensTransitions = GetComponent<ScreensTransitions>();
        worldLocked.SetActive(false);

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

        selectWorldb.interactable = !(worlds[currentWorld].starsNeeded > PersistentDataContainer.PersistentData.totalStars);
        worldLocked.SetActive(worlds[currentWorld].starsNeeded > PersistentDataContainer.PersistentData.totalStars);
        
        if (currentWorld == worlds.Length - 1) {
            nextWorldb.interactable = false;
        }
        previousWorldb.interactable = true;
    }

    public void ShowPrevious() {
        var previous = currentWorld;
        currentWorld--;
        
        selectWorldb.interactable = !(worlds[currentWorld].starsNeeded > PersistentDataContainer.PersistentData.totalStars);
        worldLocked.SetActive(worlds[currentWorld].starsNeeded > PersistentDataContainer.PersistentData.totalStars);
        
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
