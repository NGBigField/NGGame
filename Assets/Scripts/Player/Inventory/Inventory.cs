using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<BaseInventoryItem> items = new List<BaseInventoryItem>();

    public void Reset()
    {
        // Destory all of the items added
        foreach (var item in items)
            Destroy(item);

        items.Clear();
    }

    public bool AddItem(System.Type itemType)
    {
        if (GetItemByType(itemType)) return false;// there is a powerup of same type

        var item = (BaseInventoryItem)gameObject.AddComponent(itemType);
        items.Add(item);
        return true;
    }



    /// <summary>
    /// Returns the first item of a specific type
    /// </summary>
    /// <param name="itemType"></param>
    /// <returns></returns>
    public BaseInventoryItem GetItemByType(System.Type itemType)
    {
        foreach (BaseInventoryItem item in items)
            if (itemType == item.GetType()) return item;

        return null;
    }

    /// <summary>
    /// Returns the first item of a specific name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
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