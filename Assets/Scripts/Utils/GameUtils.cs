using UnityEngine;

/// <summary>
/// General game utils;
/// </summary>
public class GameUtils
{
    /// <summary>
    /// Returns all of the enemies in the scene.
    /// </summary>
    /// <returns></returns>
    public static GameObject[] GetAllEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy");
    }
}