using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public PlayerControl playerControl;

    public PowerupInventory powerupsInv;

    // Start is called before the first frame update
    void Start()
    {
        powerupsInv = new PowerupInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        MoveLogic();
        JumpLogic();
        FireLogic();
        ExplosionLogic();
    }

    void MoveLogic()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        playerControl.Move(horizontal, vertical);

        // If the player fell off the screen
        if (transform.position.y < -5) GameManager.Instance.Endgame(GetComponent<PlayerManager>().score);
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
        if (Input.GetButtonDown("Fire2")) playerControl.UsePowerup();
    }
}


public class PowerupInventory
{
    public int numExplosions = 0;
}
