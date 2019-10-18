using UnityEngine;

public abstract class BaseInventoryItem : MonoBehaviour {
    protected Inventory inventory;

    protected PlayerManager playerManager;

    protected virtual void Awake () {
        inventory = GetComponent<Inventory> ();
        playerManager = GetComponent<PlayerManager> ();
    }

    protected virtual void Start () {

    }

    public abstract string ItemName { get; }

    public abstract void Use ();

    protected virtual void OnDestroy () {
        inventory.OnItemDestroy (this);
    }
}