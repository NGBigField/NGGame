using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public PowerupInventory powerupsInv;
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

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Cursor.visible = false;

        powerupsInv = new PowerupInventory();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        MoveLogic();
        JumpLogic();
        FireLogic();
        ExplosionLogic();
    }

    void MoveLogic()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        forwardVec = Camera.main.transform.forward;
        forwardVec.y = 0;
        sideVec = Quaternion.AngleAxis(90, Vector3.up) * forwardVec;

        rb.AddForce(forwardVec * force * Time.deltaTime * vertical, ForceMode.Impulse);
        rb.AddForce(sideVec * force * Time.deltaTime * horizontal, ForceMode.Impulse);

        // If the player fell off the screen
        if (transform.position.y < -5) GameManager.Instance.Endgame(GetComponent<PlayerManager>().score);
    }

    void JumpLogic()
    {
        var jumpTriggered = Input.GetButtonDown("Jump");
        if (jumpTriggered && isGrounded)
        {
            audioSource.PlayOneShot(jumpSound);
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.VelocityChange);
        }
    }

    void FireLogic()
    {
        var playerPosition = this.transform.position;

        fireVec = Quaternion.AngleAxis(fireAngle, -sideVec) * (Camera.main.transform.forward); //update this no-matter if fires, so other scripts can use it

        if (Input.GetButtonDown("Fire1"))
        {

            /* Create Bullet  */
            var bullet = Instantiate(bulletPrefab, playerPosition + Vector3.up * 0.8f, Quaternion.identity); //Trying to make bullet exit more realisticaly
            var bulletRigidbody = bullet.GetComponent<Rigidbody>();

            /* Give Bullet Speed */
            bulletRigidbody.AddForce(fireVec * kickVelocityFactor, ForceMode.Impulse);

        }
    }

    void ExplosionLogic()
    {
        var playerPosition = transform.position;
        GameObject player = gameObject;

        if (Input.GetButtonDown("Fire2") && player.GetComponent<PlayerLogic>().powerupsInv.numExplosions > 0 )
        {
            Instantiate(explosionPrefab, playerPosition, Quaternion.identity);
            player.GetComponent<PlayerLogic>().powerupsInv.numExplosions--; //update number of explosions
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


public class PowerupInventory
{
    public int numExplosions = 0;

}
