using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControls : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public PlayerControl playerControl;
    public float dragSensitivity = 1f;
    private Vector2 touchPositionDelta;
    public CameraMovement cameraMovement;
    public float movementSensitivity = 0.1f;

    private void Start()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Get movement of the finger since last frame
        var touchPositionDelta = eventData.delta * movementSensitivity;

        // Move the camera accordingly
        cameraMovement.MoveCamera(touchPositionDelta.x, touchPositionDelta.y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!eventData.dragging) playerControl.Fire();
    }
}
