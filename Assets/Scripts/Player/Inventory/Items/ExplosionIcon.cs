using UnityEngine;
using UnityEngine.EventSystems;

public class ExplosionIcon : MonoBehaviour, IPointerDownHandler
{
    public PlayerControl playerControl;

    // Start is called before the first frame update
    void Start()
    {
        SetVisible(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        playerControl.UsePowerup(ExplosionPowerup.NAME);
    }

    public void SetVisible(bool isVisible)  //its taking int because maybe we'll want more objects
    {
        if (isVisible)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
