using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : GameEntity
{
    public GameoverScreen gameoverScreen;

    public Inventory inventory;
    public PlayerCanvas playerCanvas;
    public Rigidbody rb;
    //public Vector3 startingPoint = new Vector3(0.0f, 10f, -80.0f);

    public float health;
    public float score;

    protected override void Awake()
    {
        base.Awake();
        disableAutoSpawnAnimation = true;
    }

    protected override void Start()
    {
        base.Start();
        RestartPlayer();
    }

    public void RestartPlayer()
    {
        Vector3 startingPoint = new Vector3(0.0f, 15f, -8.0f);
        health = 1.0f;
        score = 0.0f;
        transform.position = startingPoint;

        /*  Fixes a bug where the player maintain his previous speed after death */
        gameObject.GetComponentInParent<PlayerLogic>().FreezePlayer(0.8f);

        playerCanvas.ShowCrosshair();
        playerCanvas.SetScore(0.0f);
        playerCanvas.SetHealth(1.0f);
        inventory.Reset();
        PlaySpawnAnimation();

        // Hide the gameover screen
        gameoverScreen.Hide();
    }

    public void OnPlayerHit(float value)
    {
        playerCanvas.DoDamageBlink();
        this.health = Mathf.Max(0.0f, this.health - value);
        playerCanvas.SetHealth(this.health);

        if (health == 0.0f) GameManager.Instance.Endgame(score);
    }

    public void KillPlayer()
    {

        health = 0.0f;
        playerCanvas.HideCrosshair();

        // Show the gameover screen
        gameoverScreen.Show(score);
    }

    public void IncreaseScore(float value)
    {
        this.score += value;
        playerCanvas.SetScore(score);
    }

    public void PauseGameToggle()
    {
        GameManager.Instance.PauseGameToggle();
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}