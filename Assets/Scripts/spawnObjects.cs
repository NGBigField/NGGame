using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObjects : MonoBehaviour
{
    private GameObject _spawnObjects;
    private GameObject _spawnArea;
    private float _safeRadius;

    private float _spawnDelay;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    spawnObjects(GameObject spawnObject, GameObject spawnArea, float safeRadius, float spawnDelay)
    {
        this._spawnArea = spawnArea;
        this._safeRadius = safeRadius;
        this._spawnDelay = spawnDelay;
        this._spawnObjects = spawnObject;
    }

}
