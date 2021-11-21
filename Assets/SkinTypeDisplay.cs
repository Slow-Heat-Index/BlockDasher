using System.Collections;
using System.Collections.Generic;
using Data;
using Sources.Identification;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SkinTypeDisplay : MonoBehaviour {
    public Identifier Identifier;
    private Image image;

    // Start is called before the first frame update
    void Start() {
        image = GetComponent<Image>();

        image.sprite = Resources.Load<Sprite>("SkinPictures/" + Identifier.ToString().Replace(":", "-"));
    }

    public void SetSkin() {
        PersistentDataContainer.PersistentData.skin = Identifier;
    }

}
