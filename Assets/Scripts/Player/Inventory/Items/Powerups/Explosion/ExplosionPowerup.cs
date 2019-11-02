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
        explosionPrefab = GameRepository.Instance.explosionPowerupEffectPrefab;
    }

    protected override void Start () {
        base.Start ();

        // When this powerup is added, simply show the explosion icon
        SetIconVisibility (true);
    }

    private void SetIconVisibility (bool visible) {
        // Set it to active or inactive accordingly
        playerManager.playerCanvas.explosionIcon.SetActive (visible);
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