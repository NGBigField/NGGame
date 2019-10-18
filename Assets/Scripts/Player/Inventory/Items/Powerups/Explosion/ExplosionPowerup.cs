/* Every Power up is only different with how it changes the screen when used: */
using UnityEngine;

public class ExplosionPowerup : BasePowerup {
    public static string NAME = "Explosion";

    public GameObject explosionPrefab;

    public override string ItemName => ExplosionPowerup.NAME;

    public override bool IsImmediatePowerup => false;

    public override bool Reusable => false;

    protected override void Awake () {
        base.Awake ();
        explosionPrefab = Resources.Load<GameObject> ("Prefabs/Explosion");
    }

    protected override void Start () {
        // When this powerup is added, simply show the explosion icon
        SetIconVisibility (true);
    }

    private void SetIconVisibility (bool visible) {
        // TODO: Generate icon on the fly, instead of setting it staticly on the canvas
        var canvasIcon = GameObject.Find ("ExplosionIcon");
        if (!canvasIcon) return;

        var explosionIconScript = canvasIcon.GetComponent<ExplosionIcon> ();
        if (explosionIconScript) explosionIconScript.SetVisible (visible);
    }

    public override void Use () {
        Instantiate (explosionPrefab, transform.position, Quaternion.identity);
        base.Use ();
    }

    protected override void OnDestroy () {
        SetIconVisibility (false);
        base.OnDestroy ();
    }
}