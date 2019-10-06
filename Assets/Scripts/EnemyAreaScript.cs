using UnityEngine;

public class EnemyAreaScript : MonoBehaviour
{
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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isSpawnEnemies = true;
        }
    }

}