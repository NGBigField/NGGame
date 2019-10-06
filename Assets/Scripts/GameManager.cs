using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameOver = false;

    private float gameOverTime;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // If the game has ended and the user is pressing any key
        if (isGameOver && Input.anyKeyDown && (Time.time - gameOverTime) > 2.0f) // Wait at least 2 seconds before restarting the game show the game over animation
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
    }

    public void RestartGame()
    {
        isGameOver = false;

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

        /*Call the restart Method of the EnemyArea so that enemies won't respawn during "Game Lost" screen */
        foreach (var EnemyArea in GameObject.FindGameObjectsWithTag("EnemyArea"))
            EnemyArea.GetComponent<EnemyAreaScript>().restartGame();

        var gameOverAnimator = GameObject.Find("GameOver").GetComponent<Animator>();
        gameOverAnimator.SetBool("isGameOver", false);
    }
}
