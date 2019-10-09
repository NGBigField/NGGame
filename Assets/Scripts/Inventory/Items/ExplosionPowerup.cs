/* Every Power up is only different with how it changes the screen when used: */
using UnityEngine;

public class ExplosionPowerup : BasePowerup
{
    public static string Name = "Explosion";

    private void Start()
    {
        // When this powerup is added, simply show the explosion icon
        SetIconVisibility(true);
    }

    private void SetIconVisibility(bool visible)
    {
        var canvasIcon = GameObject.Find("ExplosionIcon");
        var explosionIconScript = canvasIcon.GetComponent<ExplosionIcon>();
        if (explosionIconScript) explosionIconScript.SetVisible(visible);
    }

    public override void Use()
    {
        GameObject explosionPrefab = Resources.Load<GameObject>("Prefabs/Explosion");
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this);
    }

    private void OnDestroy()
    {
        SetIconVisibility(false);
    }
}