using UnityEditor.Animations;
using UnityEngine;

/// <summary>
/// This is a the basic object for all living entities in the game, creates a common infrastructure for common functions like spawn.
/// </summary>
public class GameEntity : MonoBehaviour {
    public AudioSource audioSource;
    public Animator animator;

    public AnimatorController spawnController;

    public AudioClip spawnSound;

    private void Awake () {
        audioSource = GetComponent<AudioSource> ();
        animator = GetComponent<Animator> ();
        spawnController = (AnimatorController) Resources.Load ("Animations/SpawnController");
        spawnSound = (AudioClip) Resources.Load ("Sounds/Electric Sfx/Wav/Whoosh_Electric/Whoosh_Electric_00");
    }

    private void Start () {
        PlaySpawnAnimation ();
    }

    public void PlaySpawnAnimation () {
        animator.runtimeAnimatorController = spawnController;
        audioSource.PlayOneShot (spawnSound);
    }
}