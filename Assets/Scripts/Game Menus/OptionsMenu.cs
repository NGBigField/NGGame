using TMPro;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    private GameSettings gameSettings { get { return GameSettings.Instance; } }

    private void Start()
    {
        UpdateControlModeText();
    }

    public void ChangeControlMode()
    {
        var controlMode = gameSettings.ControlMode;
        if (controlMode == GameControlMode.Joystick && SystemInfo.supportsGyroscope) controlMode = GameControlMode.Gyroscope;
        else controlMode = GameControlMode.Joystick;
        gameSettings.ControlMode = controlMode;
        UpdateControlModeText();
    }

    private void UpdateControlModeText()
    {
        var controlsButtonText = transform.Find("ControlsButton").Find("Text").GetComponent<TextMeshProUGUI>();
        controlsButtonText.text = string.Format("CONTROLS: {0}", (gameSettings.ControlMode == GameControlMode.Gyroscope ? "Gyroscope" : @"Joystick\Mouse"));
    }
}
