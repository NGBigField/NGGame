using UnityEngine;

public class PowerupsInventory : MonoBehaviour
{
    public PlayerCanvas _playerCanvas;

    public ExplosionPowerup explosions;

    public void reset()
    {
        explosions.reset();
    }

    private void Start()
    {
        explosions = new ExplosionPowerup();
        set(ref _playerCanvas);
    }

    public void set(ref PlayerCanvas playerCanvas)
    {
        _playerCanvas = playerCanvas;
        explosions.set(ref playerCanvas);
    }

}




public class Powerup : MonoBehaviour
{
    protected int _num;

    public PlayerCanvas _PlayerCanvas;

    public int getNum() {return _num;}

    public void reset()
    {
        _num = 0 ;
        updateCanvas();
    }

    public void set(ref PlayerCanvas PlayerCanvas)
    {
        this._PlayerCanvas = PlayerCanvas;
        reset();
    }

    protected virtual void updateCanvas(){}  //depands on which powerup;


    public void increment()
    {
        _num++;
        updateCanvas();
    }

    public void decrement()
    {
        _num--;
        updateCanvas();
    }
}


/* Every Power up is only different with how it changes the screen when used: */
public class ExplosionPowerup : Powerup
{
    protected override void updateCanvas()
    {
        _PlayerCanvas.SetNumExplosionIcons(_num);
        Debug.Log(_num);
    }
}