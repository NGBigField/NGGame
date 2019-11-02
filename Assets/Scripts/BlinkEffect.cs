using UnityEngine;

public class BlinkEffect : MonoBehaviour {
    /// <summary>
    /// The renderer to perform the blink on. If not specified, uses this component renderer.
    /// </summary>
    public Renderer blinkRenderer;

    /// <summary>
    /// The audio source to use for the blink sound. If not specified, uses this component audio source.
    /// </summary>
    public AudioSource blinkAudioSource;

    private AudioClip blinkSound;

    public float blinkTime = 0.5f;

    private float lastBlinkTime;

    private void Awake () {
        if (!blinkRenderer) blinkRenderer = GetComponent<Renderer> ();
        if (!blinkAudioSource) blinkAudioSource = GetComponent<AudioSource> ();
        blinkSound = GameRepository.Instance.blinkSound;
    }

    private void Update () {
        var deltaTime = Time.time - lastBlinkTime;

        if (deltaTime > blinkTime) {
            lastBlinkTime = Time.time;
            blinkRenderer.enabled = !blinkRenderer.enabled;

            // If the renderer is enabled, and there is a blink audio source
            if (!blinkRenderer.enabled && blinkAudioSource)
                blinkAudioSource.PlayOneShot (blinkSound);
        }
    }
}