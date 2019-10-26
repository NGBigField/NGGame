using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchJoystick : MonoBehaviour
{
    public PlayerControl playerControl;
    public Joystick joystick;

    // Update is called once per frame
    void Update()
    {
        var horizontal = joystick.Horizontal;
        var vertical = joystick.Vertical;

        playerControl.Move(horizontal, vertical);
    }
}
