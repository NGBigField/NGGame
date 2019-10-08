using UnityEngine;

public class TouchControls : MonoBehaviour
{
    public CameraMovement cameraMovement;

    // Update is called once per frame
    void Update()
    {

    }

    /**
Moves the camera according  */
    void MoveCameraLogic()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                // Get movement of the finger since last frame
                var touchDeltaPosition = touch.deltaPosition;
                cameraMovement.MoveCamera(touchDeltaPosition.x, touchDeltaPosition.y);
            }

        }
    }
}
