using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour {
    private List<EnemyIndicator> indicators = new List<EnemyIndicator> ();

    // Start is called before the first frame update
    void Start () {
        this.updateAllPlayersOnInit ();
    }

    // Update is called once per frame
    void Update () {

    }

    private void OnDestroy () {
        this.updateAllPlayerOnDestroy ();
    }

    /// <summary>
    /// Updates all the players in the games about this enemy.
    /// </summary>
    private void updateAllPlayersOnInit () {
        foreach (var player in GameObject.FindGameObjectsWithTag ("Player")) {
            var playerManager = player.GetComponent<PlayerManager> ();

            // Add enemy indicator for this enemy to the player
            this.indicators.Add (playerManager.playerCanvas.CreateEnemyIndicator (this.gameObject));
        }
    }

    private void updateAllPlayerOnDestroy () {
        // Destroy the indicator
        foreach (var indicator in this.indicators)
            Destroy (indicator);
    }
}