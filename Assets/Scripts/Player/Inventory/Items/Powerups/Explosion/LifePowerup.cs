/* Every Power up is only different with how it changes the screen when used: */
using UnityEngine;

public class LifePowerup : BasePowerup
{
    public static string NAME = "Life";

    public static float healthIncrease = 0.5f;

    public override string ItemName => LifePowerup.NAME;

    public override bool IsImmediatePowerup => true;

    public override bool Reusable => false;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Use()
    {
        playerManager.SetHealth(playerManager.health + healthIncrease);
        base.Use();
    }
}