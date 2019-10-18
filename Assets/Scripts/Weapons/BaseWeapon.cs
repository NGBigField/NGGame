using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour {
    /// <summary>
    /// The power of the bullet shot from the weapon.
    /// </summary>
    public float kickVelocityFactor = 2f;

    public GameObject bulletPrefab;

    /// <summary>
    /// Shoots the bullet by instantiating it's bullet prefab.
    /// </summary>
    /// <param name="fireVec"></param>
    protected void Shoot (Vector3 fireVec) {
        /* Create Bullet  */
        var bullet = Instantiate (bulletPrefab, transform.position + Vector3.up * 0.8f, Quaternion.identity);
        var bulletRigidbody = bullet.GetComponent<Rigidbody> ();

        /* Give Bullet Speed */
        bulletRigidbody.AddForce (fireVec * kickVelocityFactor, ForceMode.Impulse);
    }

    /// <summary>
    /// Called when shoot button is held down.
    /// </summary>
    /// <param name="fireVec"></param>
    public abstract void OnShootDown (Vector3 fireVec);

    /// <summary>
    /// Called when shoot button is released.
    /// </summary>
    /// <param name="fireVec"></param>
    public abstract void OnShootUp (Vector3 fireVec);
}