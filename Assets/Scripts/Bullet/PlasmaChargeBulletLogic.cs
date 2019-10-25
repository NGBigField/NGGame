using UnityEngine;

public class PlasmaChargeBulletLogic : SimpleBulletLogic
{
    // The minimum and miaximum size of the bullet
    public readonly float maxScale = 0.8f;
    public readonly float minScale = 0.3f;

    public readonly float minExplosionForce = 40.0f;

    public readonly float maxExplosionForce = 80.0f;

    public readonly float minExplosionRadius = 3.0f;
    public readonly float maxExplosionRadius = 6.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // The current scale of the bullet
            var curScale = transform.localScale.x;

            // Calculate the radius and force according to the size of the bullet, the bigger the bullet, the stronger the force is
            var explosionForce = (curScale / maxScale) * (maxExplosionForce - minExplosionForce) + minExplosionForce;
            var explosionRadius = (curScale / maxScale) * (maxExplosionRadius - minExplosionRadius) + minExplosionRadius;

            // True if the bullet reached it's maximum size
            var isMaxScale = curScale >= maxScale;

            // If the bullet is at it's maximum size
            if (isMaxScale)
            {
                // TODO: Add particles animation to explosion
                Destroy(other.gameObject);
            }
            else
            {
                // Just a an explosion
                foreach (var enemy in GameUtils.GetAllEnemies())
                {
                    var rigidBody = enemy.GetComponent<Rigidbody>();
                    rigidBody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}