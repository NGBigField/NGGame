using UnityEngine;

public class PickupLogic : GameEntity {
    /// <summary>
    /// The timeout before the item will be destroyed. Set to -1 for infinite.
    /// </summary>
    /// <value></value>
    public int pickTimeout = 15;

    /// <summary>
    /// The renderer we use for the timeout blink effect. If none specified, uses the current game object renderer.
    /// </summary>
    public Renderer blinkRenderer;

    public BlinkEffect blinkEffect;

    protected float spawnTime;

    public string powerupName;
    public AudioClip pickupSound;

    protected override void Awake () {
        base.Awake ();

        if (!blinkRenderer) blinkRenderer = GetComponent<Renderer> ();

        if (!blinkEffect) {
            blinkEffect = gameObject.AddComponent<BlinkEffect> ();
            blinkEffect.enabled = false;
            blinkEffect.blinkRenderer = blinkRenderer;
        }
    }

    protected override void Start () {
        base.Start ();
        spawnTime = Time.time;
    }

    protected virtual void Update () {
        // If the pick timeout passed, destroy the item
        if (pickTimeout > -1) {
            var timePassed = Time.time - spawnTime;
            var timeLeft = pickTimeout - timePassed;
            var minBlinkTimeStart = pickTimeout * 0.5f; // The minimum blink effect time to start

            // if (blinkRenderer) blinkRenderer.enabled = false;

            if (timeLeft <= 0)
                GameObject.Destroy (this.gameObject);
            else if (timePassed > minBlinkTimeStart) {
                if (!blinkEffect.enabled) blinkEffect.enabled = true;

                // Starts blinking faster when object is about to be destroyed
                blinkEffect.blinkTime = (1 - ((timePassed - minBlinkTimeStart) / (pickTimeout - minBlinkTimeStart))) * 0.3f + 0.1f;
            }
        }

    }

    private void OnTriggerEnter (Collider other) {
        if (other.tag == "Player") {
            /* Do nothing yet, just prepare the variables */
            var player = other;
            var playerManager = player.GetComponent<PlayerManager> ();
            var audioSource = GetComponentInParent<AudioSource> ();

            /*Collect that item and play animation&sound only if playerInventory allowes it */
            if (playerManager.PickPowerup (getPowerupTypeByName (powerupName))) {
                animator.SetBool ("isDissolve", true);
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