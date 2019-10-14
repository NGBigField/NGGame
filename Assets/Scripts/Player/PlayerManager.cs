using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : GameEntity
{
    public Inventory inventory;
    public PlayerCanvas playerCanvas;
    public Rigidbody rb;

    //public Vector3 startingPoint = new Vector3(0.0f, 10f, -80.0f);

    public float health;
    public float score;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public void RestartPlayer()
    {
        Vector3 startingPoint = new Vector3(0.0f, 15f, -8.0f);
        health = 1.0f;
        score = 0.0f;
        transform.position = startingPoint;

        /*  Fixess a bug where the player maintain his previous speed after death */
        gameObject.GetComponentInParent<PlayerLogic>().FreezePlayer(0.8f);

        playerCanvas.ShowCrosshair();
        playerCanvas.SetScore(0.0f);
        playerCanvas.SetHealth(1.0f);
        inventory.Reset();
        PlaySpawnAnimation();
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


        // Show the game over text
        var gameOverObject = transform.parent.Find("Player Canvas").Find("GameOver");

        var gameOverScoreText = gameOverObject.transform.Find("GameoverScore").GetComponent<Text>();
        gameOverScoreText.text = string.Format("YOUR SCORE IS {0}!", score);

        var gameOverAnimator = gameOverObject.GetComponent<Animator>();
        gameOverAnimator.SetBool("isGameOver", true);
    }

    public void IncreaseScore(float value)
    {
        this.score += value;
        playerCanvas.SetScore(score);
    }
}