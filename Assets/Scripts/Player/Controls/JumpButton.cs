using UnityEngine;

public class JumpButton : MonoBehaviour {
    public PlayerControl playerControl;

    public void OnClick () {
        playerControl.Jump ();
    }
}