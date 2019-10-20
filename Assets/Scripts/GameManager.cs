using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    public bool isGameOver = false;

    public bool isGamePaused = false;

    private float gameOverTime;
    private float restartTime = 2.0f;

    public bool IsGameFreezed { get { return isGamePaused || isGameOver; } }

    private void Awake () {
        Instance = this;

        // Set the timescale to normal
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update () {
        // If the game has ended and the user is pressing any key or touching the screen
        if (isGameOver && ((Input.anyKeyDown || Input.touchCount > 0) && (Time.time - gameOverTime) > restartTime)) // Wait at least 2 seconds before restarting the game show the game over animation
            RestartGame ();
    }

    public void Endgame (float score) {
        gameOverTime = Time.time;
        isGameOver = true;

        // Kill all of the players in the arena
        foreach (var player in GameObject.FindGameObjectsWithTag ("Player")) {
            var playerManager = player.GetComponent<PlayerManager> ();
            playerManager.KillPlayer ();
        }

        /*Call the restart Method of the EnemyArea so that enemies won't respawn during "Game Lost" screen */
        foreach (var EnemyArea in GameObject.FindGameObjectsWithTag ("EnemyArea"))
            EnemyArea.GetComponent<EnemyAreaScript> ().restartGame ();

        StartCoroutine (CleanGame (2.0f));
    }

    /// <summary>
    /// Cleans all of the game scene, preparing for a new scene instead. Waits 2 seconds before cleaning the field to allow the game over animation to take place.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CleanGame (float delay) {
        yield return new WaitForSeconds (delay);

        // Destroy all leftover objects
        foreach (var enemy in GameObject.FindGameObjectsWithTag ("Enemy"))
            Destroy (enemy);

        foreach (var bullet in GameObject.FindGameObjectsWithTag ("Bullet"))
            Destroy (bullet);

        foreach (var powerup in GameObject.FindGameObjectsWithTag ("Powerup"))
            Destroy (powerup);
    }

    public void RestartGame () {
        isGameOver = false;
        isGamePaused = false;
        Time.timeScale = 1.0f;

        // Restart all players to their starting state
        foreach (var player in GameObject.FindGameObjectsWithTag ("Player")) {
            var playerManager = player.GetComponent<PlayerManager> ();
            playerManager.RestartPlayer ();
        }

        // Restart the level
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
    }

    public void PauseGameToggle () {
        isGamePaused = !isGamePaused;
        Time.timeScale = (isGamePaused) ? 0.0f : 1.0f;

        foreach (var player in GameObject.FindGameObjectsWithTag ("Player")) {
            var playerCanvas = player.transform.parent.GetComponentInChildren<PlayerCanvas> ();
            if (isGamePaused) playerCanvas.ShowPauseMenu ();
            else playerCanvas.HidePauseMenu ();
        }
    }

    public void QuitGame () {
        // Before quitting the game, return variables to their normal state
        isGameOver = false;
        isGamePaused = false;
        Time.timeScale = 1.0f;

        // Load the main menu scene
        SceneManager.LoadScene ("MainMenu");
    }
}