public abstract class BasePowerup : BaseInventoryItem {
    public abstract bool IsImmediatePowerup { get; }

    protected override void Start () {
        base.Start ();

        // If it's an immediate powerup, use it right from the start
        if (IsImmediatePowerup) Use ();
    }
}