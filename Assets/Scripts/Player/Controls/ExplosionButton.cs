using UnityEngine;
using UnityEngine.EventSystems;

public class ExplosionButton : MonoBehaviour, IPointerDownHandler
{
    public PlayerControl playerControl;
    // Start is called before the first frame update
    public void OnPointerDown(PointerEventData eventData)
    {
        playerControl.UsePowerup(ExplosionPowerup.NAME);
    }
}
