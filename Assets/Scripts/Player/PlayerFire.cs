using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float kickVelocityFactor = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var playerPosition = this.transform.position;

        if (Input.GetButtonDown("Fire1"))
        {
            var fireVector = Camera.main.transform.forward;
            fireVector.y = 0;

            //Position
            var bullet = Instantiate(bulletPrefab, playerPosition + fireVector * 0.2f - Vector3.up * 0.6f, Quaternion.identity);

            //Velocity
            var bulletRigidbody = bullet.GetComponent<Rigidbody>();

            bulletRigidbody.AddForce(fireVector * kickVelocityFactor, ForceMode.Impulse);
            //bulletRigidbody.AddForce(Vector3.up * kickVelocityFactor * 0.0001f, ForceMode.Impulse);
        }
    }
}
