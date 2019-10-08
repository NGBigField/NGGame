using UnityEditor;
using UnityEngine;

/**
Contains main player control methods like movement, fire, and secondary fire.
 */
public class PlayerControl : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 fireVec;
    public Vector3 forwardVec;
    public Vector3 sideVec;
    public AudioClip jumpSound;
    public AudioClip explosionSound;

    public GameObject bulletPrefab;

    public GameObject explosionPrefab;

    public float kickVelocityFactor = 2f;
    public float force = 20.0f;
    private float jumpVelocity = 6.7f;
    private bool isGrounded;

    private AudioSource audioSource;
    private float fireAngle = 15f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();


#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX // Only hide cursor on desktop
        Cursor.visible = false;
#endif
    }

    public void Move(float horizontal, float vertical)
    {
        forwardVec = Camera.main.transform.forward;
        forwardVec.y = 0;
        sideVec = Quaternion.AngleAxis(90, Vector3.up) * forwardVec;

        rb.AddForce(forwardVec * force * Time.deltaTime * vertical, ForceMode.Impulse);
        rb.AddForce(sideVec * force * Time.deltaTime * horizontal, ForceMode.Impulse);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            audioSource.PlayOneShot(jumpSound);
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.VelocityChange);
        }
    }

    public void UpdateFireCrosshair()
    {
        var playerPosition = this.transform.position;
        fireVec = Quaternion.AngleAxis(fireAngle, -sideVec) * (Camera.main.transform.forward); //update this no-matter if fires, so other scripts can use it
    }

    public void Fire()
    {
        /* Create Bullet  */
        var bullet = Instantiate(bulletPrefab, transform.position + Vector3.up * 0.8f, Quaternion.identity);
        var bulletRigidbody = bullet.GetComponent<Rigidbody>();

        /* Give Bullet Speed */
        bulletRigidbody.AddForce(fireVec * kickVelocityFactor, ForceMode.Impulse);
    }

    public void UsePowerup() // TODO: << Add param, don't always use explosion
    {
        // TODO: There is a better way of doing this
        var powerUpsInventory = GetComponent<PlayerManager>().powerupsInv;

        if (powerUpsInventory.explosions.getNum() > 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            powerUpsInventory.explosions.decrement();
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Ground") isGrounded = true;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "Ground") isGrounded = false;
    }
}
