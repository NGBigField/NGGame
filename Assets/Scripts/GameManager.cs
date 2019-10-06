using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public GameObject EnemyArea
    ;

    private float lastSpawnTime;

    public bool isGameOver = false;

    private float gameOverTime;

    // Start is called before the first frame update
    void Start()
    {
        EnemyArea = GameObject.FindGameObjectWithTag("EnemyArea");
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        bool isSpawnEnemies = EnemyArea.GetComponent<EnemyAreaScript>().isSpawnEnemies();
        if (!isGameOver && isSpawnEnemies)
        {
          //spawnLogic(spawnObject   ,spawnArea  , safeRadius, spawnDelay)
            spawnLogic(powerupPrefab , EnemyArea , 8.0f , 2.0f);
            spawnLogic(enemyPrefab   , EnemyArea , 9.0f , 1.0f);
        }


        // If the game has ended and the user is pressing any key
        else if (isGameOver && Input.anyKeyDown && (Time.time - gameOverTime) > 2.0f) // Wait at least 2 seconds before restarting the game show the game over animation
            RestartGame();
    }


    private void spawnLogic(GameObject spawnObject, GameObject spawnArea, float safeRadius, float spawnDelay)
    {
        Debug.Log(spawnObject);

        lastSpawnTime += Time.deltaTime;
        if (lastSpawnTime >= spawnDelay)
        {
            Vector3 randomPosition = getRandomPosition_recursive(spawnArea, safeRadius);

            Instantiate(spawnObject, randomPosition, Quaternion.identity);

            lastSpawnTime = 0;
        }
    }

    //Finds a random position for spawning that allow for a safeRadius around player:
    private Vector3 getRandomPosition_recursive(GameObject spawnArea, float safeRadius)
    {
        Debug.Log(spawnArea.transform.localScale);
        float x = Random.Range(spawnArea.transform.position.x - spawnArea.transform.localScale.x/2, spawnArea.transform.position.x + spawnArea.transform.localScale.x/2 );
        float y = spawnArea.transform.position.y + spawnArea.transform.localScale.y/2;
        float z = Random.Range(spawnArea.transform.position.z - spawnArea.transform.localScale.z/2, spawnArea.transform.position.z + spawnArea.transform.localScale.z/2 );

        Vector3 result = new Vector3(x, y, z);
        Vector3 distance = result - GameObject.FindGameObjectWithTag("Player").transform.position;  //the distance between the random result vector and the player

        if (distance.magnitude < safeRadius)  // if not allowing for a safe radius
        {
            result = getRandomPosition_recursive(spawnArea, safeRadius);  //try again recursively
        }

        return result;
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
