using UnityEngine;

public class EnemyAreaScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public GameObject EnemyArea
    ;
    private spawnObjects enemySpawner;
    private spawnObjects powerupSpawner;
    private bool _isSpawnEnemies = false;

    // Start is called before the first frame update
    void Start()
    {
        _isSpawnEnemies = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool isSpawnEnemies()
    {
        return _isSpawnEnemies;
    }

    public void restartGame()
    {
        _isSpawnEnemies = false;
        Destroy(enemySpawner);
        Destroy(powerupSpawner);
    }

    void OnTriggerEnter(Collider other)
    {
        if (_isSpawnEnemies == false && other.tag == "Player") //On first land
        {
            enemySpawner = gameObject.AddComponent(typeof(spawnObjects)) as spawnObjects;
            powerupSpawner = gameObject.AddComponent(typeof(spawnObjects)) as spawnObjects;

            enemySpawner.set(ref enemyPrefab, ref EnemyArea, 8.0f, 1.25f);
            powerupSpawner.set(ref powerupPrefab, ref EnemyArea, 5.0f, 2.0f);

            _isSpawnEnemies = true; //stop making more spawning Objects
        }
    }

}