using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    protected Rigidbody rb;

    protected MeshRenderer meshRenderer;

    private Vector3 launchForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        // if (launchForce != null) LaunchBullet(launchForce);
    }

    /// <summary>
    /// Launches the bullet from it's current position by adding the required force.
    /// </summary>
    /// <param name="force"></param>
    public virtual void LaunchBullet(Vector3 force)
    {
        // If the bullet has not been instantiated yet
        if (!rb)
        {
            launchForce = force;
            return;
        }

        /* Give Bullet Speed */
        rb.AddForce(force, ForceMode.Impulse);
    }

    protected virtual void Update()
    {
        if (transform.position.y < -5.0)
            Destroy(this.gameObject);
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject, 10);
    }
}