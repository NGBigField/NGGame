using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject enemyPrefab;

    public float enemySpawnTime = 1.0f;
    private float lastEnemySpawnTime;

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
        if (!isGameOver)
            EnemySpawnLogic();

        // If the game has ended and the user is pressing any key
        else if (Input.anyKeyDown && (Time.time - gameOverTime) > 2.0f) // Wait at least 2 seconds before restarting the game show the game over animation
            RestartGame();
    }

    private void EnemySpawnLogic()
    {
        lastEnemySpawnTime += Time.deltaTime;
        if (lastEnemySpawnTime >= 3.0f)
        {
            var randomPosition = new Vector3(Random.Range(-4f, 4f), 2f, Random.Range(3.5f, 4.5f));
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

            lastEnemySpawnTime = 0;
        }
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

        var gameOverObject = GameObject.Find("GameOver");

        var gameOverScoreText = gameOverObject.transform.Find("GameoverScore").GetComponent<Text>();
        gameOverScoreText.text = string.Format("YOUR SCORE IS {0}!", score);

        var gameOverAnimator = gameOverObject.GetComponent<Animator>();
        gameOverAnimator.SetBool("isGameOver", true);
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

        var gameOverAnimator = GameObject.Find("GameOver").GetComponent<Animator>();
        gameOverAnimator.SetBool("isGameOver", false);
    }
}
