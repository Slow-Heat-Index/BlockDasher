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
    [SerializeField] private Image worldName;

    [SerializeField] private Button nextWorldb;
    [SerializeField] private Button previousWorldb;
    [SerializeField] private Button selectWorldb;
    [SerializeField] private GameObject worldLocked;
    [SerializeField] private GameObject touchToPlay;

    [Header("Stars Needed")] [SerializeField]
    private GameObject frameUI;
    [SerializeField] private  TextMeshProUGUI textUI; 
    
    private ScreensTransitions _screensTransitions;

    
    private void Awake() {
        _titleScreen = FindObjectOfType<TitleScreen>();
        currentWorld = previousGameWorld;
        worldName.sprite = worlds[currentWorld].imageName;
        _rectTransform = GetComponent<RectTransform>();
        _screensTransitions = GetComponent<ScreensTransitions>();
        RefreshUI();

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
        worldName.sprite = worlds[currentWorld].imageName;

        RefreshUI();
    }

    public void ShowPrevious() {
        var previous = currentWorld;
        currentWorld--;
        
        RefreshUI();
        
        worlds[currentWorld].hubWorldAnim.PopUp();
        worlds[previous].hubWorldAnim.PopDown();
        worldName.sprite = worlds[currentWorld].imageName;
    }

    public int GetCurrentWorld() {
        return currentWorld;
    }

    private void RefreshUI()
    {
        bool locked = worlds[currentWorld].starsNeeded > PersistentDataContainer.PersistentData?.totalStars;
        selectWorldb.interactable = !locked;
        worldLocked.SetActive(locked);
        touchToPlay.SetActive(!locked);
        frameUI.SetActive(locked);
        textUI.SetText(worlds[currentWorld].isLocked
            ? "Coming soon"
            : $"Stars to Unlock:\n{PersistentDataContainer.PersistentData?.totalStars}/{worlds[currentWorld].starsNeeded}");
        nextWorldb.interactable = currentWorld < worlds.Length - 1;
        previousWorldb.interactable = currentWorld > 0;
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
