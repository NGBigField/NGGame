using UnityEngine;

public class PowerupLogic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var player = other;
            Destroy(gameObject);
        }
    }
}
