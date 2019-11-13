using UnityEngine;

/// <summary>
/// This is a the basic object for all living entities in the game, creates a common infrastructure for common functions like spawn.
/// </summary>
public class GameEntity : MonoBehaviour {
    public AudioSource audioSource;
    public Animator animator;

    public Renderer renderer;

    public RuntimeAnimatorController spawnController;

    public AudioClip spawnSound;

    public bool disableAutoSpawnAnimation = false;

    protected virtual void Awake () {
        audioSource = GetComponent<AudioSource> ();
        animator = GetComponent<Animator> ();
        renderer = GetComponent<Renderer> ();
        spawnController = GameRepository.Instance.spawnController;
        spawnSound = GameRepository.Instance.spawnSound;
    }

    protected virtual void Start () {
        if (!disableAutoSpawnAnimation) PlaySpawnAnimation ();
    }

    public void PlaySpawnAnimation () {
        animator.runtimeAnimatorController = spawnController;
        audioSource.PlayOneShot (spawnSound);
    }

    public void PlayDissolveAnimation () {
        // TODO: Actually add dissolve animation
        animator.runtimeAnimatorController = spawnController;
        audioSource.PlayOneShot (spawnSound);
    }

    /// <summary>
    ///  Destroys the game objecct with the specified delay.!-- If withDissolveAnimation is set, dissolve animation is being played.
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="withDissolveAnimation"></param>
    public virtual void Kill (float delay = 0, bool withDissolveAnimation = true) {

    }
}