using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;

    Rigidbody rb;
    Transform EnemyTransform;

    public float maxVelocity = 8.0f;

    private float movementFactor = 10.0f;
    private float size;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        EnemyTransform = GetComponent<Transform>();
        SpawnAnimation();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isGameOver) return;
        var playerPosition = player.transform.position;
        var enemyPosition = transform.position;
        var delta = playerPosition - enemyPosition;
        delta.y = 0;
        float enemySpeed = rb.velocity.magnitude;

        /* Update Force only if cube is not too fast */
        if (enemySpeed < maxVelocity) rb.AddForce(delta.normalized * Time.deltaTime * movementFactor, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        /*update size after spawn */
        if (size < 1.0f) size += 0.02f;
        EnemyTransform.localScale = new Vector3(size, size, size);


        if (GameManager.Instance.isGameOver) return;

        if (transform.position.y < -5.0f)
        {
            Destroy(this.gameObject);
            player.GetComponent<PlayerManager>().IncreaseScore(1.0f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            var playerManager = other.collider.GetComponent<PlayerManager>();
            playerManager.OnPlayerHit(0.25f);
        }
    }

    private void SpawnAnimation()
    {
        /*Sizing */
        size = 0.02f;
        EnemyTransform.localScale = new Vector3(size, size, size);

        /*Spinning */
        Vector3 Torque = Vector3.up;  //Just a powerful spin around the up axis
        rb.AddTorque(Torque, ForceMode.Impulse);
    }

}
