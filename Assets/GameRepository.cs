using UnityEngine;
using UnityEngine.Animations;

public class GameRepository : MonoBehaviour
{
    public static GameRepository Instance;

    public AudioClip spawnSound;
    public RuntimeAnimatorController spawnController;

    public GameObject smallExplosionPrefab;

    public GameObject lifePowerupPrefab;

    public GameObject enemyPrefab;

    public GameObject explosionPowerupPrefab;

    public GameObject explosionPowerupEffectPrefab;

    public GameObject plasmaBulletPrefab;

    public GameObject simpleWeaponPrefab;

    private void Awake()
    {
        Instance = this;
    }
}
