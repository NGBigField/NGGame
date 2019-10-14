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
        var controlMode = gameSettings.controlMode;
        if (controlMode == GameControlMode.Joystick && SystemInfo.supportsGyroscope) controlMode = GameControlMode.Gyroscope;
        else controlMode = GameControlMode.Joystick;
        gameSettings.controlMode = controlMode;
        gameSettings.SaveSettings();
        UpdateControlModeText();
    }

    private void UpdateControlModeText()
    {
        var controlsButtonText = transform.Find("ControlsButton").Find("Text").GetComponent<TextMeshProUGUI>();
        controlsButtonText.text = string.Format("CONTROLS: {0}", (gameSettings.controlMode == GameControlMode.Gyroscope ? "Gyroscope" : @"Joystick\Mouse"));
    }
}
