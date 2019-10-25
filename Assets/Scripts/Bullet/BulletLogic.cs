using UnityEngine;

public class BulletLogic : MonoBehaviour
{
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