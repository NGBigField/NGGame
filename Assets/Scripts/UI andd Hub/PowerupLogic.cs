﻿using UnityEngine;

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

            if (player.GetComponent<PlayerLogic>().powerupsInv.numExplosions < 1)
            {
                var audioSource = GetComponentInParent<AudioSource>();
                audioSource.PlayOneShot(pickupSound);

                var animator = GetComponentInParent<Animator>();
                animator.SetBool("isDissolve", true);
                Destroy(this.gameObject, (35f / 60f));

                //update number of powerups
                player.GetComponent<PlayerLogic>().powerupsInv.numExplosions++;
            }
        }

    }
}