using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public Rigidbody rb;

    public Vector3 forwardVec;
    public Vector3 sideVec;
    public AudioClip jumpSound;

    public GameObject bulletPrefab;
    public float kickVelocityFactor = 2f;
    public float force = 20.0f;
    private float jumpVelocity = 5.0f;
    private bool isGrounded;

    private AudioSource audioSource;
    private float fireAngle = 15f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        MoveLogic();
        JumpLogic();
        FireLogic();
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

        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 fireVec = Quaternion.AngleAxis(fireAngle, -sideVec) * (Camera.main.transform.forward);

            /* Create Bullet  */
            //Trying to make bullet exit more realisticaly
            //var bullet = Instantiate(bulletPrefab, playerPosition + fireVec * 0.2f, Quaternion.identity);
            var bullet = Instantiate(bulletPrefab, playerPosition + Vector3.up * 0.8f, Quaternion.identity);
            var bulletRigidbody = bullet.GetComponent<Rigidbody>();

            /* Give Bullet Speed */
            bulletRigidbody.AddForce(fireVec * kickVelocityFactor, ForceMode.Impulse);

            // var fireVector = Camera.main.transform.forward;
            // fireVector.y = 0;


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
