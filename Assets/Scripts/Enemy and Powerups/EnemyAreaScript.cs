﻿using UnityEngine;

public class EnemyAreaScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject ExplosionPowerupPrefab;

    public GameObject LifePowerupPrefab;
    public GameObject EnemyArea
    ;
    private ObjectSpawner enemySpawner;
    private ObjectSpawner ExplosionPowerupSpawner;
    private ObjectSpawner LifePowerupSpawner;
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
        Destroy(ExplosionPowerupSpawner);
        Destroy(LifePowerupSpawner);
    }

    void OnTriggerEnter(Collider other)
    {
        if (_isSpawnEnemies == false && other.tag == "Player") //On first land
        {
            enemySpawner = gameObject.AddComponent(typeof(ObjectSpawner)) as ObjectSpawner;
            ExplosionPowerupSpawner = gameObject.AddComponent(typeof(ObjectSpawner)) as ObjectSpawner;
            LifePowerupSpawner = gameObject.AddComponent(typeof(ObjectSpawner)) as ObjectSpawner;

            enemySpawner.set(ref enemyPrefab, ref EnemyArea, 8.0f, 1.25f);
            ExplosionPowerupSpawner.set(ref ExplosionPowerupPrefab, ref EnemyArea, 3.0f, 16.0f);
            LifePowerupSpawner.set(ref LifePowerupPrefab, ref EnemyArea, 4.0f, 26.0f);

            _isSpawnEnemies = true; //stop making more spawning Objects
        }
    }

}