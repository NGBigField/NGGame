/* Every Power up is only different with how it changes the screen when used: */
using UnityEngine;

public class LifePowerup : BasePowerup
{
    public static string NAME = "Life";

    public GameObject lifePrefab;

    public static float healthIncrease = 0.5f;

    public override string ItemName => LifePowerup.NAME;

    private void Awake()
    {
        lifePrefab = Resources.Load<GameObject>("Prefabs/Life");
    }

    private void Start()
    {

    }

    public override void Use()
    {
        Destroy(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}