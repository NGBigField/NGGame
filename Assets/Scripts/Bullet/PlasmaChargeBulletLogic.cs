using UnityEngine;

public class PlasmaChargeBulletLogic : SimpleBulletLogic
{
    // The minimum and miaximum size of the bullet
    public readonly float maxScale = 0.8f;
    public readonly float minScale = 0.3f;

    public readonly float minExplosionForce = 10.0f;

    public readonly float maxExplosionForce = 80.0f;

    public readonly float minExplosionRadius = 3.0f;
    public readonly float maxExplosionRadius = 6.0f;

    public AudioClip moveSound;

    public AudioClip explosionSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    public override void LaunchBullet(Vector3 force)
    {
        base.LaunchBullet(force);
        audioSource.clip = moveSound;
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Ground")
        {
            // The current scale of the bullet
            var currScale = transform.localScale.x;

            // FIXME: Fix algorithm for calculating force according to size

            // Calculate the radius and force according to the size of the bullet, the bigger the bullet, the stronger the force is
            var explosionForce = (currScale / maxScale) * (maxExplosionForce - minExplosionForce) + minExplosionForce;
            var explosionRadius = (currScale / maxScale) * (maxExplosionRadius - minExplosionRadius) + minExplosionRadius;

            // True if the bullet reached it's maximum size
            var isMaxScale = currScale >= maxScale;

            // Just a an explosion
            foreach (var enemy in GameUtils.GetAllEnemies())
            {
                var rigidBody = enemy.GetComponent<Rigidbody>();
                rigidBody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            // Play the explosion sound
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}