using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputLogic : MonoBehaviour {
    public PlayerControl playerControl;
    public PlayerInput playerInput;
    private InputActionMap inputActionMap;

    private InputAction lookAction;

    private Vector2 moveAxis;
    private Vector2 lookAxis;

    private void Awake () {
        inputActionMap = playerInput.actions.FindActionMap ("Player Controls");
        lookAction = inputActionMap.FindAction ("Look");
    }

    private void OnJump () {
        playerControl.Jump ();
    }

    private void OnAltFire (InputValue value) {
        // TODO: Add alt-fire in PlayerControl script instead of use powerup
        playerControl.UsePowerup (ExplosionPowerup.NAME);
    }

    private void OnFire (InputValue value) {
        if (value.isPressed) playerControl.FireDown ();
        else playerControl.FireUp ();
    }

    private void OnSwitchWeapon (InputValue value) {
        var inputValue = value.Get<Vector2> ().y;
        if (inputValue > 0) playerControl.NextWeapon ();
        else playerControl.PreviousWeapon ();
    }

    private void OnLook (InputValue value) {

    }

    private void OnPause () {
        GameManager.Instance.PauseGameToggle ();
    }

    private void OnMove (InputValue value) {
        // Store the last move axis
        moveAxis = value.Get<Vector2> ();
    }

    private void Update () {
        // Move the player accordingly
        playerControl.Move (moveAxis.x, moveAxis.y);

        // Update the fire crosshair
        playerControl.UpdateFireCrosshair ();
    }

    private void LateUpdate () {
        // Get the current look axis values
        lookAxis = lookAction.ReadValue<Vector2> ();

        // TODO: Check if we need to take the Time.deltaTime into consideration here?
        playerControl.cameraMovement.MoveCamera (lookAxis.x, lookAxis.y);

    }
}