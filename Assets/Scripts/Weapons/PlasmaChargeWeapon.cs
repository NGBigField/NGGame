using UnityEngine;

public class PlasmaChargeWeapon : BaseWeapon {
    private float chargeStartTime;
    private GameObject chargedBullet;

    private Transform playerTransform;

    public override string Name => "Plasma";

    private void Awake () {
        bulletPrefab = Resources.Load<GameObject> ("Prefabs/PlasmaChargeBullet");
    }

    protected override void Shoot (Vector3 fireVec, Transform playerTransform) {

    }

    public override void OnShootDown (Vector3 fireVec, Transform playerTransform) {
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
        var deltaTime = Time.time - chargeStartTime;

        /* Give Bullet Speed */
        var bulletRigidbody = chargedBullet.GetComponent<Rigidbody> ();
        bulletRigidbody.AddForce (fireVec * kickVelocityFactor, ForceMode.Impulse);

        chargedBullet = null;
    }
}