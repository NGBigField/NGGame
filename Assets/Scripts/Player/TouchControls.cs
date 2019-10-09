using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControls : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public CameraMovement cameraMovement;
    public float movementSensitivity = 0.1f;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Get movement of the finger since last frame
        var touchDeltaPosition = eventData.delta * movementSensitivity;

        // Move the camera accordingly
        cameraMovement.MoveCamera(touchDeltaPosition.x, touchDeltaPosition.y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
