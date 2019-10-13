using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameOver = false;

    private float gameOverTime;
    private float restartTime = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // If the game has ended and the user is pressing any key or touching the screen
        if (isGameOver && ((Input.anyKeyDown || Input.touchCount > 0) && (Time.time - gameOverTime) > restartTime)) // Wait at least 2 seconds before restarting the game show the game over animation
            RestartGame();
    }

    public void Endgame(float score)
    {
        gameOverTime = Time.time;
        isGameOver = true;

        // Kill all of the players in the arena
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            var playerManager = player.GetComponent<PlayerManager>();
            playerManager.KillPlayer();
        }

        /*Call the restart Method of the EnemyArea so that enemies won't respawn during "Game Lost" screen */
        foreach (var EnemyArea in GameObject.FindGameObjectsWithTag("EnemyArea"))
            EnemyArea.GetComponent<EnemyAreaScript>().restartGame();

        StartCoroutine(CleanGame());
    }

    /// <summary>
    /// Cleans all of the game scene, preparing for a new scense instead. Waits 2 seconds before cleaning the field to allow the game over animation take place.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CleanGame()
    {
        yield return new WaitForSeconds(2.0f);

        // Restart all players to their starting state
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            var playerManager = player.GetComponent<PlayerManager>();
            playerManager.RestartPlayer();
        }

        // Destroy all leftover objects
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(enemy);

        foreach (var bullet in GameObject.FindGameObjectsWithTag("Bullet"))
            Destroy(bullet);

        foreach (var powerup in GameObject.FindGameObjectsWithTag("Powerup"))
            Destroy(powerup);
    }

    public void RestartGame()
    {
        isGameOver = false;
        var gameOverAnimator = GameObject.Find("GameOver").GetComponent<Animator>();
        gameOverAnimator.SetBool("isGameOver", false);
    }
}