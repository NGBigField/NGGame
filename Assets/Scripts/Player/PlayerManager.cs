using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public PowerupInventory powerupsInv;
    public PlayerCanvas playerCanvas;
    public Rigidbody rb;

    //public Vector3 startingPoint = new Vector3(0.0f, 10f, -80.0f);

    public float health;
    public float score;

    private void Start()
    {
        RestartPlayer();
        powerupsInv = new PowerupInventory();
    }

    public void RestartPlayer()
    {
        Vector3 startingPoint = new Vector3(0.0f, 10f, -8.0f);
        health = 1.0f;
        score = 0.0f;
        transform.position = startingPoint;

        /*  Fixess a bug where the player maintain his previous speed after death */
        rb.velocity = Vector3.zero;
        rb.rotation = Quaternion.identity;

        playerCanvas.ShowCrosshair();
        playerCanvas.SetScore(0.0f);
        playerCanvas.SetHealth(1.0f);
    }


    //optional type of powerup  //returns true if player picked it up
    public bool PickupPowerup()
    {
        if (powerupsInv.numExplosions < 1)
        {
            //update number of powerups
            powerupsInv.numExplosions++;
            playerCanvas.SetNumExplosionIcons(1);
            return true;
        }
        else
        {
            return false;
        }
    }



    public void OnPlayerHit(float value)
    {
        playerCanvas.DoDamageBlink();
        this.health = Mathf.Max(0.0f, this.health - value);
        playerCanvas.SetHealth(this.health);

        if (health == 0.0f) GameManager.Instance.Endgame(score);
    }

    public void KillPlayer()
    {
        powerupsInv.resetInventory();

        health = 0.0f;
        playerCanvas.HideCrosshair();

        // Show the game over text
        var gameOverObject = transform.Find("PlayerCanvas").Find("GameOver");

        var gameOverScoreText = gameOverObject.transform.Find("GameoverScore").GetComponent<Text>();
        gameOverScoreText.text = string.Format("YOUR SCORE IS {0}!", score);

        var gameOverAnimator = gameOverObject.GetComponent<Animator>();
        gameOverAnimator.SetBool("isGameOver", true);
    }

    public void IncreaseScore(float value)
    {
        this.score += value;
        playerCanvas.SetScore(score);
    }
}

public class PowerupInventory
{
    public int numExplosions = 0;

    public void resetInventory()
    {
        numExplosions = 0 ;
    }
}


public class Powerup : MonoBehaviour
{
    public string _name;
    protected int _num;

    public GameObject _player;

    public void reset(){ _num = 0 ;}

    public void setPlayer(ref GameObject player) { this._player = player;}

    protected virtual void updateCanvas(){}  //depand on which powerup;


    public static Powerup  operator ++(Powerup x)
    {
        x._num++;
        x.updateCanvas();
        return x;
    }

    public static Powerup  operator --(Powerup x)
    {
        x._num--;
        x.updateCanvas();
        return x;
    }
}

public class ExplosionPowerup : Powerup
{
    protected override void updateCanvas()
    {
        _player.GetComponentInChildren<PlayerCanvas>().SetNumExplosionIcons(_num);
    }
}