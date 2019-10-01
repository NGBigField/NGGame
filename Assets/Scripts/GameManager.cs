using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject enemyPrefab;

    public float enemySpawnTime = 1.0f;
    private float lastEnemySpawnTime;

    public bool isGameOver = false;

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

    public void Endgame()
    {
        isGameOver = true;

        var gameOverAnimator = GameObject.Find("GameOver").GetComponent<Animator>();
        gameOverAnimator.SetBool("isGameOver", true);
    }
}
