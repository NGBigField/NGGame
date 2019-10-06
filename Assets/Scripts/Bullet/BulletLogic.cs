using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5.0)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        //GameManager.IncreaseScore();
        Destroy(gameObject, 10);
    }
}
