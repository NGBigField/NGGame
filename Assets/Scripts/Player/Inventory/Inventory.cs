using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public PlayerCanvas playerCanvas;

    public List<BaseInventoryItem> items = new List<BaseInventoryItem>();

    public void Reset()
    {
        // Destory all of the items added
        foreach (var item in items)
            Destroy(item);

        items.Clear();
    }

    public BaseInventoryItem AddItem(System.Type itemType)
    {
        var item = (BaseInventoryItem)gameObject.AddComponent(itemType);
        items.Add(item);
        return item;
    }

    public BaseInventoryItem GetItemByName(string name)
    {
        foreach (var item in items)
            if (name == item.ItemName) return item;

        return null;
    }

    public void OnItemDestroy(BaseInventoryItem item)
    {
        if (items.Contains(item))
            items.Remove(item);
    }
}