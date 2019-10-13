using UnityEngine;

public class PowerupLogic : GameEntity
{
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var player = other;
            var playerInventory = player.GetComponent<Inventory>();

            var audioSource = GetComponentInParent<AudioSource>();
            audioSource.PlayOneShot(pickupSound);

            // TODO: Instantiate a generic powerup instead of an explosion
            playerInventory.AddItem(typeof(ExplosionPowerup));
            animator.SetBool("isDissolve", true);
            Destroy(this.gameObject, (35f / 60f));
        }
    }
}
