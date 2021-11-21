using System;
using System.Collections;
using System.Collections.Generic;
using Controller.GameEditor;
using Sources.Identification;
using Sources.Level;
using Sources.Registration;
using Sources.Skins;
using UnityEngine;

public class SkinStorageScrollView : MonoBehaviour
{
    public GameObject prefab;


    private void Start() {
        var list = Registry.Get<Skin>(Identifiers.ManagerSkin).ToList();
        list.Sort((o1, o2) =>
            string.Compare(o1.Name, o2.Name, StringComparison.Ordinal));

        list.ForEach(AddElement);
    }

    private void AddElement(Skin skin) {
        var instance = Instantiate(prefab, transform);
        var display = instance.GetComponent<SkinTypeDisplay>();
        if (display != null) {
            display.Identifier = skin.Identifier;
        }
    }
}
