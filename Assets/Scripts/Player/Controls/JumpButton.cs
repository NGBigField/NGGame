using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler
{
    public PlayerControl playerControl;
    // Start is called before the first frame update
    public void OnPointerDown(PointerEventData eventData)
    {
        playerControl.Jump();
    }
}
