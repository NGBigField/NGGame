using UnityEngine;

public class PlasmaChargeWeapon : BaseWeapon {
    private float chargeStartTime;
    private GameObject chargedBullet;

    public override string Name => "Plasma";

    private void Awake () {
        bulletPrefab = Resources.Load<GameObject> ("Prefabs/PlasmaChargeBullet");
    }

    protected override void Shoot (Vector3 fireVec, Transform playerTransform) {

    }

    public override void OnShootDown (Vector3 fireVec, Transform playerTransform) {
        chargeStartTime = Time.time;

        /* Create Bullet  */
        chargedBullet = Instantiate (bulletPrefab, playerTransform.position + Vector3.up * 0.8f, Quaternion.identity);
    }

    public override void OnShootUp (Vector3 fireVec, Transform playerTransform) {
        var deltaTime = Time.time - chargeStartTime;

        /* Give Bullet Speed */
        var bulletRigidbody = chargedBullet.GetComponent<Rigidbody> ();
        bulletRigidbody.AddForce (fireVec * kickVelocityFactor, ForceMode.Impulse);
    }
}