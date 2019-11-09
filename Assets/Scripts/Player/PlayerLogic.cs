using UnityEngine;

public class PlayerLogic : MonoBehaviour {
    public PlayerControl playerControl;
    public PlayerCanvas playerCanvas;
    private float lastSpawnTime;

    private float spawnFreezeTime;

    private void Awake () {
        // Disables simulate mouse with touches to allow mouse fire to only work on desktop and not on mobile touch
        Input.simulateMouseWithTouches = false;
    }

    // Update is called once per frame
    void Update () {
        // Check if the game is currently freezed, if so, don't do anything
        if (GameManager.Instance.IsGameFreezed) return;

        CheckDeathLogic ();
    }

    void CheckDeathLogic () {
        // If the player fell off the screen
        if (transform.position.y < -5) GameManager.Instance.Endgame (GetComponent<PlayerManager> ().score);
    }

    public void FreezePlayer (float freezeTime) {
        lastSpawnTime = Time.time;
        spawnFreezeTime = freezeTime;
    }
}