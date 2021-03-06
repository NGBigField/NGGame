using UnityEngine;

public abstract class BaseInventoryItem : MonoBehaviour {
    public abstract bool Reusable { get; }

    protected Inventory inventory;

    protected PlayerManager playerManager;

    protected virtual void Awake () {
        inventory = GetComponent<Inventory> ();
        playerManager = GetComponent<PlayerManager> ();
    }

    protected virtual void Start () {

    }

    protected virtual void Update () {
        
    }

    /// <summary>
    /// Returns true if the user can pickup this item.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool CanPickup (PlayerManager player) {
        return true;
    }

    public abstract string ItemName { get; }

    /// <summary>
    /// Uses the item. If the item is not reusable, destroys the item and removes it from the inventory.
    /// </summary>
    public virtual void Use () {
        if (!Reusable) {
            inventory.OnItemDestroy (this);
            Destroy (this);
        }
    }

    protected virtual void OnDestroy () { }
}