using UnityEngine;

/// <summary>
/// Contains main player control methods like movement, fire, and secondary fire.
/// </summary>
public class PlayerControl : MonoBehaviour
{
    public AudioSource audioSource;
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


    private float fireAngle = 15f;

    public bool InputDisabled
    {
        get
        {
            return GameManager.Instance.IsGameFreezed;
        }
    }

    public void Move(float horizontal, float vertical)
    {
        if (InputDisabled) return;
        forwardVec = Camera.main.transform.forward;
        forwardVec.y = 0;
        sideVec = Quaternion.AngleAxis(90, Vector3.up) * forwardVec;

        rb.AddForce(forwardVec * force * Time.deltaTime * vertical, ForceMode.Impulse);
        rb.AddForce(sideVec * force * Time.deltaTime * horizontal, ForceMode.Impulse);
    }
    public void Freeze()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        rb.rotation = Quaternion.identity;
    }

    public void Jump()
    {
        if (InputDisabled) return;

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
        if (InputDisabled) return;

        /* Create Bullet  */
        var bullet = Instantiate(bulletPrefab, transform.position + Vector3.up * 0.8f, Quaternion.identity);
        var bulletRigidbody = bullet.GetComponent<Rigidbody>();

        /* Give Bullet Speed */
        bulletRigidbody.AddForce(fireVec * kickVelocityFactor, ForceMode.Impulse);
    }

    public void UsePowerup(string name)
    {
        if (InputDisabled) return;

        var inventory = GetComponent<Inventory>();
        var item = inventory.GetItemByName(name);
        if (item) item.Use();
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