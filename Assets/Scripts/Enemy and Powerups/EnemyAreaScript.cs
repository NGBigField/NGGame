using UnityEngine;

public class EnemyAreaScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject explosionPowerupPrefab;
    public GameObject lifePowerupPrefab;
    private ObjectSpawner enemySpawner;
    private ObjectSpawner explosionPowerupSpawner;
    private ObjectSpawner lifePowerupSpawner;
    private bool _isSpawnEnemies = false;

    private void Awake()
    {
        lifePowerupPrefab = GameRepository.Instance.lifePowerupPrefab;
        enemyPrefab = GameRepository.Instance.enemyPrefab;
        explosionPowerupPrefab = GameRepository.Instance.explosionPowerupPrefab;
    }

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
        Destroy(explosionPowerupSpawner);
        Destroy(lifePowerupSpawner);
    }

    private void OnCollisionStay(Collision other)
    {
        if (_isSpawnEnemies == false && other.gameObject.tag == "Player") //On first land
        {
            var enemyArea = this.gameObject;

            enemySpawner = gameObject.AddComponent(typeof(ObjectSpawner)) as ObjectSpawner;
            explosionPowerupSpawner = gameObject.AddComponent(typeof(ObjectSpawner)) as ObjectSpawner;
            lifePowerupSpawner = gameObject.AddComponent(typeof(ObjectSpawner)) as ObjectSpawner;

            enemySpawner.set(ref enemyPrefab, ref enemyArea, 8.0f, 4f);
            explosionPowerupSpawner.set(ref explosionPowerupPrefab, ref enemyArea, 3.0f, 16.0f);
            lifePowerupSpawner.set(ref lifePowerupPrefab, ref enemyArea, 4.0f, 26.0f);

            _isSpawnEnemies = true; //stop making more spawning Objects
        }
    }

}