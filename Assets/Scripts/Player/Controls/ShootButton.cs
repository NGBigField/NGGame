using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public PlayerControl playerControl;
    // Start is called before the first frame update
    public void OnPointerDown (PointerEventData eventData) {
        playerControl.FireDown ();
    }

    public void OnPointerUp (PointerEventData eventData) {
        playerControl.FireUp ();
    }
}