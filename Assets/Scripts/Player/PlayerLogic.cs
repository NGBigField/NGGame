using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public PlayerControl playerControl;
    public PlayerCanvas playerCanvas;
    private float lastSpawnTime;

    private float spawnFreezeTime;

    private void Awake()
    {
        // Disables simulate mouse with touches to allow mouse fire to only work on desktop and not on mobile touch
        Input.simulateMouseWithTouches = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Always check pause logic
        CheckPauseLogic();

        // Check if the game is currently freezed, if so, don't do anything
        if (GameManager.Instance.IsGameFreezed) return;

        MoveLogic();
        JumpLogic();
        FireLogic();
        ExplosionLogic();
        CheckDeathLogic();
    }


    void MoveLogic()
    {
        if (Time.time - lastSpawnTime < spawnFreezeTime) // If player needs to be frozen
        {
            playerControl.Freeze();
        }
        else
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            playerControl.Move(horizontal, vertical);
        }
    }

    void JumpLogic()
    {
        var jumpTriggered = Input.GetButtonDown("Jump");
        if (jumpTriggered) playerControl.Jump();
    }

    void FireLogic()
    {
        playerControl.UpdateFireCrosshair();

        if (Input.GetButtonDown("Fire1"))
            playerControl.Fire();
    }

    void ExplosionLogic()
    {
        if (Input.GetButtonDown("Fire2")) playerControl.UsePowerup(ExplosionPowerup.NAME);
    }

    void CheckDeathLogic()
    {
        // If the player fell off the screen
        if (transform.position.y < -5) GameManager.Instance.Endgame(GetComponent<PlayerManager>().score);
    }

    void CheckPauseLogic()
    {
        if (Input.GetButtonDown("Cancel")) GameManager.Instance.PauseGameToggle();
    }

    public void FreezePlayer(float freezeTime)
    {
        lastSpawnTime = Time.time;
        spawnFreezeTime = freezeTime;
    }
}