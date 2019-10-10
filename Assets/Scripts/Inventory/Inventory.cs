using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public PlayerCanvas playerCanvas;

    public List<BaseInventoryItem> items = new List<BaseInventoryItem> ();

    public void Reset () {
        // Destory all of the items added
        foreach (var item in items)
            Destroy (item);
    }

    public BaseInventoryItem AddItem (System.Type itemType) {
        var item = (BaseInventoryItem) gameObject.AddComponent (itemType);
        items.Add (item);
        return item;
    }

    public void OnItemDestroy (BaseInventoryItem item) {
        items.Remove (item);
    }
}