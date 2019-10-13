/* Every Power up is only different with how it changes the screen when used: */
using UnityEngine;

public class ExplosionPowerup : BasePowerup
{
    public static string NAME = "Explosion";

    public GameObject explosionPrefab;

    public override string ItemName => ExplosionPowerup.NAME;

    private void Awake()
    {
        explosionPrefab = Resources.Load<GameObject>("Prefabs/Explosion");
    }

    private void Start()
    {
        // When this powerup is added, simply show the explosion icon
        SetIconVisibility(true);
    }

    private void SetIconVisibility(bool visible)
    {
        // TODO: Generate icon on the fly, instead of setting it staticly on the canvas
        var canvasIcon = GameObject.Find("ExplosionIcon");
        if (!canvasIcon) return;

        var explosionIconScript = canvasIcon.GetComponent<ExplosionIcon>();
        if (explosionIconScript) explosionIconScript.SetVisible(visible);
    }

    public override void Use()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this);
    }

    protected override void OnDestroy()
    {
        SetIconVisibility(false);
        base.OnDestroy();
    }
}