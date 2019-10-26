using UnityEngine;

public class PlasmaChargeWeapon : BaseWeapon
{
    private float chargeStartTime;
    private GameObject chargedBullet;

    private Transform playerTransform;

    public override string Name => "Plasma";

    public override string IconPath => "Icons/S_royal_hit";

    protected override void Awake()
    {
        base.Awake();
        bulletPrefab = GameRepository.Instance.plasmaBulletPrefab;
        bullets = 3;
    }

    protected override void Shoot(Vector3 fireVec, Transform playerTransform)
    {
        // If there is already a charged bullet
        if (chargedBullet)
        {
            var deltaTime = Time.time - chargeStartTime;

            var bulletLogic = chargedBullet.GetComponent<BulletLogic>();
            bulletLogic.LaunchBullet(fireVec * kickVelocityFactor);

            chargedBullet = null;
            --bullets;
        } // If the magazine is empty
        else if (IsMagazineEmpty)
            audioSource.PlayOneShot(emptyClipSound);
        else // If there is no charged bullet, charge it
            OnShootDown(fireVec, playerTransform);
    }

    private Vector3 GetBulletStartPosition(Transform playerTransform)
    {
        // FIXME: Set an offset from the position the player is looking
        return playerTransform.position + Vector3.up * 0.8f;
    }

    private void Update()
    {
        if (chargedBullet) chargedBullet.transform.position = GetBulletStartPosition(playerTransform);
    }

    public override void OnShootDown(Vector3 fireVec, Transform playerTransform)
    {
        // If we can't shoot or there is already a bullet charging, don't do anything
        if (!CanShoot) return;

        // There is already a bullet charged, call shoot-up to release it
        if (chargedBullet)
        {
            OnShootUp(fireVec, playerTransform);
            return;
        }

        chargeStartTime = Time.time;
        this.playerTransform = playerTransform;

        /* Create Bullet  */
        chargedBullet = Instantiate(bulletPrefab, GetBulletStartPosition(playerTransform), Quaternion.identity);
    }

    public override void OnShootUp(Vector3 fireVec, Transform playerTransform)
    {
        Shoot(fireVec, playerTransform);
    }
}