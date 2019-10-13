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

            /*Collect that item only if playerInventory allowes it */
            if (playerInventory.AddItem(typeof(ExplosionPowerup))) //Successfully added that item
            {
                animator.SetBool("isDissolve", true);
                audioSource.PlayOneShot(pickupSound);
                Destroy(this.gameObject, (35f / 60f));
            }
        }
    }
}
