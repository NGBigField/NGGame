using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Inventory inventory;
    public PlayerCanvas playerCanvas;
    public Rigidbody rb;

    //public Vector3 startingPoint = new Vector3(0.0f, 10f, -80.0f);

    public float health;
    public float score;

    private void Start()
    {
        RestartPlayer();
    }

    public void RestartPlayer()
    {
        Vector3 startingPoint = new Vector3(0.0f, 10f, -8.0f);
        health = 1.0f;
        score = 0.0f;
        transform.position = startingPoint;

        /*  Fixess a bug where the player maintain his previous speed after death */
        rb.velocity = Vector3.zero;
        rb.rotation = Quaternion.identity;

        playerCanvas.ShowCrosshair();
        playerCanvas.SetScore(0.0f);
        playerCanvas.SetHealth(1.0f);
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
        inventory.Reset();

        health = 0.0f;
        playerCanvas.HideCrosshair();

        // Show the game over text
        var gameOverObject = transform.Find("PlayerCanvas").Find("GameOver");

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