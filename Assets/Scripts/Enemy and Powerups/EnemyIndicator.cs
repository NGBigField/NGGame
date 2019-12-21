using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour {
    public GameObject enemy;

    // Update is called once per frame
    void Update () {
        // If there is an enemy specified
        if (enemy) {

        }
    }

    private void OnDestroy () {
        // Destory the enemy indicator prefab as well
        Destroy (gameObject);
    }
}