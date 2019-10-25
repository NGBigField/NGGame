using UnityEngine;

public class PlasmaChargeBulletLogic : SimpleBulletLogic
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // TODO: Add enemy particles animation, if bullet is smaller than largest scale, create explosion force instead
            Destroy(other.gameObject);
        }
    }
}