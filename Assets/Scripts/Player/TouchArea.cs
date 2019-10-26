using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Responsible for clicking on anywhere on the screen. Responsible of moving the camera and shooting.
/// </summary>
public class TouchArea : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public PlayerControl playerControl;
    public float dragSensitivity = 1f;
    private Vector2 touchPositionDelta;
    public CameraMovement cameraMovement;
    public float movementSensitivity = 0.05f;


    public void OnDrag(PointerEventData eventData)
    {
        // Get movement of the finger since last frame
        var touchPositionDelta = eventData.delta * movementSensitivity;

        // Move the camera accordingly
        cameraMovement.MoveCamera(touchPositionDelta.x, touchPositionDelta.y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (GameManager.Instance.IsGameFreezed) return;
        if (!eventData.dragging) playerControl.FireDown();
    }
    public void OnPointerDown(PointerEventData eventData)
    {

    }
}