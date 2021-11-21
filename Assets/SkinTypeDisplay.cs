using Data;
using Sources.Identification;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SkinTypeDisplay : MonoBehaviour {
    public Identifier Identifier;
    public Image background;
    public Image image;
    public bool selected = false;

    public Color color, selectedColor;

    void Start() {
        image.sprite = Resources.Load<Sprite>("SkinPictures/" 
                                              + Identifier.ToString().Replace(":", "-"));
    }
    
    void Update() {
        var sel = PersistentDataContainer.PersistentData.skin == Identifier;
        if(sel == selected) return;
        selected = sel;
        background.color = selected ? selectedColor : color;
    }

    public void SetSkin() {
        PersistentDataContainer.PersistentData.skin = Identifier;
    }

}
