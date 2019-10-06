using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObjects : MonoBehaviour
{
    private GameObject _spawnObject;
    private GameObject _spawnArea;
    private float _safeRadius;

    private float _spawnDelay;

    private float _lastSpawnTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_spawnObject);

        _lastSpawnTime += Time.deltaTime;
        if (_lastSpawnTime >= _spawnDelay)
        {
            Vector3 randomPosition = getRandomPosition_recursive(_spawnArea, _safeRadius);

            Instantiate(_spawnObject, randomPosition, Quaternion.identity);

            _lastSpawnTime = 0.0f;
        }
    }


    //Finds a random position for spawning that allow for a safeRadius around player:
    private Vector3 getRandomPosition_recursive(GameObject spawnArea, float safeRadius)
    {
        float x = Random.Range(spawnArea.transform.position.x - spawnArea.transform.localScale.x / 2, spawnArea.transform.position.x + spawnArea.transform.localScale.x / 2);
        float y = spawnArea.transform.position.y + spawnArea.transform.localScale.y / 2;
        float z = Random.Range(spawnArea.transform.position.z - spawnArea.transform.localScale.z / 2, spawnArea.transform.position.z + spawnArea.transform.localScale.z / 2);

        Vector3 result = new Vector3(x, y, z);
        Vector3 distance = result - GameObject.FindGameObjectWithTag("Player").transform.position;  //the distance between the random result vector and the player

        if (distance.magnitude < safeRadius)  // if not allowing for a safe radius
        {
            result = getRandomPosition_recursive(spawnArea, safeRadius);  //try again recursively
        }

        return result;
    }

    //C'tor:
    public void set(ref GameObject spawnObject, ref GameObject spawnArea, float safeRadius, float spawnDelay)
    {
        this._spawnArea = spawnArea;
        this._safeRadius = safeRadius;
        this._spawnDelay = spawnDelay;
        this._spawnObject = spawnObject;
    }

}
