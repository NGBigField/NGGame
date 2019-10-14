using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool isAlive;
    public GameObject player;

    Rigidbody rb;
    Transform EnemyTransform;

    public float maxVelocity = 8.0f;

    private float movementFactor = 10.0f;
    private float size;
    private float lastCollisionTime;
    private float afterCollisionWaitTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        EnemyTransform = GetComponent<Transform>();
        SpawnAnimation();
        this.isAlive = true;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsGameFreezed || !isMoveable) return;
        var playerPosition = player.transform.position;
        var enemyPosition = transform.position;
        var delta = playerPosition - enemyPosition;
        delta.y = 0;
        float enemySpeed = rb.velocity.magnitude;

        /* Update Force only if cube is not too fast */
        if (enemySpeed < maxVelocity)
        {
            Vector3 newForce = delta.normalized * Time.deltaTime * movementFactor;
            newForce.y = 0; //Enemies can't fly towards you!
            rb.AddForce(newForce, ForceMode.VelocityChange);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*update size after spawn */
        if (size < 1.0f) size += 0.02f;
        EnemyTransform.localScale = new Vector3(size, size, size);


        if (GameManager.Instance.isGameOver) return;

        if (transform.position.y < -2.0f && isAlive)
        {
            player.GetComponent<PlayerManager>().IncreaseScore(1.0f);
            this.isAlive = false;
        }

        if (transform.position.y < -20.0f)
            Destroy(this.gameObject);

    }

    private bool isMoveable
    {
        get
        {
            return Time.time - lastCollisionTime > afterCollisionWaitTime;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isMoveable) return;

        if (other.collider.tag == "Player")
        {
            var playerManager = other.collider.GetComponent<PlayerManager>();
            playerManager.OnPlayerHit(0.25f);

            /*On hit, stop enemy */
            rb.velocity = new Vector3(0, 0, 0);
            lastCollisionTime = Time.time;
        }
    }

    private void SpawnAnimation()
    {
        /*Sizing */
        size = 0.02f;
        EnemyTransform.localScale = new Vector3(size, size, size);

        /*Spinning */
        Vector3 Torque = Vector3.up * 2000.0f;  //Just a powerful spin around the up axis
        rb.AddTorque(Torque, ForceMode.Impulse);
    }

}
