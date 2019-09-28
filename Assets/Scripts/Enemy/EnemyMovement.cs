﻿using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;

    Rigidbody rb;
    private float movement = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var playerPosition = player.transform.position;
        var enemyPosition = transform.position;
        var delta = playerPosition - enemyPosition;
        delta.y = 0;
        rb.AddForce(delta * Time.deltaTime * movement, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector3(0, rb.velocity.y, 0);

        if (transform.position.y < -5.0f)
        {
            Destroy(this.gameObject);
            GameManager.IncreaseScore();
        }
    }
}
