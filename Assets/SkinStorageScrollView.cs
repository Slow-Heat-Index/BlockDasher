using System.Collections.Generic;
using System.Linq;
using Data;
using Sources.Identification;
using UnityEngine;

public class SkinStorageScrollView : MonoBehaviour {
    public GameObject prefab;

    private readonly List<Identifier> _presentIdentifiers = new List<Identifier>();

    private void Start() {
        var list = PersistentDataContainer.PersistentData.availableSkins;
        _presentIdentifiers.AddRange(list);
        list.ForEach(AddElement);
    }

    public void Refresh() {
        var list = PersistentDataContainer.PersistentData.availableSkins
            .Where(it => !_presentIdentifiers.Contains(it)).ToList();
        _presentIdentifiers.AddRange(list);
        list.ForEach(AddElement );
    }

    private void AddElement(Identifier identifier) {
        var instance = Instantiate(prefab, transform);
        var display = instance.GetComponent<SkinTypeDisplay>();
        if (display != null) {
            display.Identifier = identifier;
        }
    }
}