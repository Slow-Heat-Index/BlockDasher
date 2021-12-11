using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingSceneAnim : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private String[] textContent;
    

    private void Awake() {
        gameObject.SetActive(false);
    }
    
    

    public IEnumerator LoadingAnim() {
        int i = 0;

        while (true) {
            yield return new WaitForSeconds(0.5f);
            _textMeshProUGUI.text = textContent[i % 4];
            i++;
        }
    }

}
