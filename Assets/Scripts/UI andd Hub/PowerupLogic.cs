using UnityEngine;

public class PowerupLogic : MonoBehaviour
{
    public AudioClip pickupSound;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var player = other;

            bool isPickedUp = player.GetComponent<PlayerManager>().PickupPowerup(); //also let's player manage the logic of what to do
            if (isPickedUp)
            {
                var audioSource = GetComponentInParent<AudioSource>();
                audioSource.PlayOneShot(pickupSound);

                var animator = GetComponentInParent<Animator>();
                animator.SetBool("isDissolve", true);
                Destroy(this.gameObject, (35f / 60f));
            }
        }

    }
}
