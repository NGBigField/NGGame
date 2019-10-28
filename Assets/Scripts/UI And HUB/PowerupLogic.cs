using UnityEngine;

public class PowerupLogic : GameEntity {
    public string powerupName;
    public AudioClip pickupSound;

    private void OnTriggerEnter (Collider other) {
        if (other.tag == "Player") {
            /* Do nothing yet, just prepare the variables */
            var player = other;
            var playerManager = player.GetComponent<PlayerManager> ();
            var audioSource = GetComponentInParent<AudioSource> ();

            /*Collect that item and play animation&sound only if playerInventory allowes it */
            if (playerManager.PickPowerup(getPowerupTypeByName (powerupName))) {
                animator.SetBool("isDissolve", true);
                audioSource.PlayOneShot (pickupSound);
                Destroy (this.gameObject, (35f / 60f));
            }
        }
    }

    //TODO: this is a very bad way of doing it. But Unity didn't let me store a powerups type as a public variable. ):
    public System.Type getPowerupTypeByName (string powerupName) {
        if (powerupName == "Explosion") return typeof (ExplosionPowerup);
        else if (powerupName == "Life") return typeof (LifePowerup);
        else return null;
    }
}