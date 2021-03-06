﻿using UnityEngine;

public class ObjectSpawner : MonoBehaviour {
    private GameObject spawnObject;
    private GameObject spawnArea;
    private float safeRadius;

    private float spawnDelay;

    private float lastSpawnTime;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (!spawnArea) return;

        lastSpawnTime += Time.deltaTime;
        if (lastSpawnTime >= spawnDelay) {
            Vector3 randomPosition = getRandomSpawnPosition (spawnArea, safeRadius);

            Instantiate (spawnObject, randomPosition, Quaternion.identity);

            lastSpawnTime = 0.0f;
        }
    }

    //Finds a random position for spawning that allow for a safeRadius around player:
    private Vector3 getRandomSpawnPosition (GameObject spawnArea, float safeRadius) {
        float x = Random.Range (spawnArea.transform.position.x - spawnArea.transform.localScale.x / 2, spawnArea.transform.position.x + spawnArea.transform.localScale.x / 2);
        float y = spawnArea.transform.position.y + spawnArea.transform.localScale.y + spawnObject.transform.localScale.y;
        float z = Random.Range (spawnArea.transform.position.z - spawnArea.transform.localScale.z / 2, spawnArea.transform.position.z + spawnArea.transform.localScale.z / 2);

        Vector3 result = new Vector3 (x, y, z);
        Vector3 distance = result - GameObject.FindGameObjectWithTag ("Player").transform.position; //the distance between the random result vector and the player

        if (distance.magnitude < safeRadius) // if not allowing for a safe radius
        {
            result = getRandomSpawnPosition (spawnArea, safeRadius); //try again recursively
        }

        return result;
    }

    //C'tor:
    public void set (ref GameObject spawnObject, ref GameObject spawnArea, float safeRadius, float spawnDelay) {
        this.spawnArea = spawnArea;
        this.safeRadius = safeRadius;
        this.spawnDelay = spawnDelay;
        this.spawnObject = spawnObject;
    }

}