using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;

    Rigidbody rb;
    private float movementFactor = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isGameOver) return;
        var playerPosition = player.transform.position;
        var enemyPosition = transform.position;
        var delta = playerPosition - enemyPosition;
        delta.y = 0;
        rb.AddForce(delta.normalized * Time.deltaTime * movementFactor, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        if (transform.position.y < -5.0f)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            var playerManager = other.collider.GetComponent<PlayerManager>();
            playerManager.OnPlayerHit(0.25f);
        }
    }
}
