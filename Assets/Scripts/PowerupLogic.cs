using UnityEngine;

public class PowerupLogic : MonoBehaviour
{
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var player = other;

            var audioSource = GetComponentInParent<AudioSource>();
            audioSource.PlayOneShot(pickupSound);

            var animator = GetComponentInParent<Animator>();
            animator.SetBool("isDissolve", true);
            Destroy(gameObject, (35f / 60f));
        }
    }
}
