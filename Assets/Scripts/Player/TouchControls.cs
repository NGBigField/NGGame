using UnityEngine;

public class TouchControls : MonoBehaviour
{
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
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        }
    }
}
