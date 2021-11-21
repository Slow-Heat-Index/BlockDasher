using System.Collections;
using System.Collections.Generic;
using Data;
using Sources.Identification;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SkinTypeDisplay : MonoBehaviour {
    public Identifier Identifier;
    public Image image;

    void Start() {
        image.sprite = Resources.Load<Sprite>("SkinPictures/" 
                                              + Identifier.ToString().Replace(":", "-"));
    }

    public void SetSkin() {
        PersistentDataContainer.PersistentData.skin = Identifier;
    }

}
