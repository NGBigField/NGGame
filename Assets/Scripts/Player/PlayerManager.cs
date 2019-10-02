using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public PlayerCanvas playerCanvas;

    public Vector3 startingPoint = new Vector3(0.0f, 9.25f, -12.0f);

    public float health;
    public float score;

    private void Start()
    {
        RestartPlayer();
    }

    public void RestartPlayer()
    {
        health = 1.0f;
        score = 0.0f;
        transform.position = startingPoint;
        playerCanvas.ShowCrosshair();
    }

    public void OnPlayerHit(float value)
    {
        playerCanvas.DoDamageBlink();
        this.health = Mathf.Max(0.0f, this.health - value);
        playerCanvas.setHealth(this.health);

        if (health == 0.0f) GameManager.Instance.Endgame(score);
    }

    public void KillPlayer()
    {
        health = 0.0f;
        playerCanvas.HideCrosshair();
    }

    public void IncreaseScore(float value)
    {
        this.score += value;
        playerCanvas.setScore(score);
    }
}