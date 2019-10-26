using UnityEngine;

public class PlasmaChargeWeapon : BaseWeapon {
    private float chargeStartTime;
    private GameObject chargedBullet;

    private Transform playerTransform;

    public override string Name => "Plasma";

    private void Awake () {
        bulletPrefab = Resources.Load<GameObject> ("Prefabs/PlasmaChargeBullet");
        bullets = 3;
    }

    protected override void Shoot (Vector3 fireVec, Transform playerTransform) {
        if (chargedBullet) {
            var deltaTime = Time.time - chargeStartTime;

            var bulletLogic = chargedBullet.GetComponent<BulletLogic> ();
            bulletLogic.LaunchBullet (fireVec * kickVelocityFactor);

            chargedBullet = null;
            --bullets;
        }
    }

    public override void OnShootDown (Vector3 fireVec, Transform playerTransform) {
        if (!CanShoot) return;
        chargeStartTime = Time.time;
        this.playerTransform = playerTransform;

        /* Create Bullet  */
        chargedBullet = Instantiate (bulletPrefab, GetBulletStartPosition (playerTransform), Quaternion.identity);
    }

    private Vector3 GetBulletStartPosition (Transform playerTransform) {
        // FIXME: Set an offset from the position the player is looking
        return playerTransform.position + Vector3.up * 0.8f;
    }

    private void Update () {
        if (chargedBullet) chargedBullet.transform.position = GetBulletStartPosition (playerTransform);
    }

    public override void OnShootUp (Vector3 fireVec, Transform playerTransform) {
        Shoot (fireVec, playerTransform);
    }
}