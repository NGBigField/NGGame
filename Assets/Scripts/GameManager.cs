using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private static int score = 0;
    private float lastEnemySpawnTime;

    public float enemySpawnTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EnemySpawnLogic();
    }

    public static void IncreaseScore()
    {
        ++score;
        var scoreObj = GameObject.FindGameObjectWithTag("Score");
        var txt = scoreObj.GetComponent<Text>();
        txt.text = string.Format("Score: {0}", score);
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

}
