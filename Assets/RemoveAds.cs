using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class RemoveAds : MonoBehaviour {
    private Button button;

    private void Awake() {
        button = GetComponent<Button>();
        if (PersistentDataContainer.PersistentData.adsRemoved) {
            button.interactable = false;
        }
    }

    private void OnEnable() {
        button.onClick.AddListener(OnClick);
    }

    void OnClick() {
        PersistentDataContainer.PersistentData.adsRemoved = true;
        DataAccess.Save(PersistentDataContainer.PersistentData);
        button.interactable = false;
    }
}
