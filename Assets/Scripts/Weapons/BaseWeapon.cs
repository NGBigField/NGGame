using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public abstract string Name { get; }

    private void Awake()
    {

    }

    /// <summary>
    /// The power of the bullet shot from the weapon.
    /// </summary>
    public float kickVelocityFactor = 2f;

    public GameObject bulletPrefab;

    /// <summary>
    /// Shoots the bullet by instantiating it's bullet prefab.
    /// </summary>
    /// <param name="fireVec"></param>
    protected virtual void Shoot(Vector3 fireVec, Transform playerTransform)
    {
        /* Create Bullet  */
        var bullet = Instantiate(bulletPrefab, playerTransform.position + Vector3.up * 0.8f, Quaternion.identity);
        var bulletLogic = bullet.GetComponent<BulletLogic>();

        bulletLogic.LaunchBullet(fireVec * kickVelocityFactor);
    }

    /// <summary>
    /// Called when shoot button is held down.
    /// </summary>
    /// <param name="fireVec"></param>
    public abstract void OnShootDown(Vector3 fireVec, Transform playerTransform);

    /// <summary>
    /// Called when shoot button is released.
    /// </summary>
    /// <param name="fireVec"></param>
    public abstract void OnShootUp(Vector3 fireVec, Transform playerTransform);
}