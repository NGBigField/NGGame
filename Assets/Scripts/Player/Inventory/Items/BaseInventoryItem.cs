using UnityEngine;

public abstract class BaseInventoryItem : MonoBehaviour
{
    public abstract void Use();

    private void OnDestroy()
    {
        var inventory = GetComponent<Inventory>();
        inventory.OnItemDestroy(this);
    }
}