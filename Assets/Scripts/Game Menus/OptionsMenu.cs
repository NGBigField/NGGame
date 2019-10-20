using TMPro;
using UnityEngine;

public class OptionsMenu : MonoBehaviour {
    private GameSettings gameSettings { get { return GameSettings.Instance; } }

    private void Start () {
        UpdateControlModeText ();
        UpdateEnablePostProcessingText ();
    }

    public void ChangeControlMode () {
        var controlMode = gameSettings.controlMode;
        if (controlMode == GameControlMode.Joystick && SystemInfo.supportsGyroscope) controlMode = GameControlMode.Gyroscope;
        else controlMode = GameControlMode.Joystick;
        gameSettings.controlMode = controlMode;
        gameSettings.SaveSettings ();
        UpdateControlModeText ();
    }

    private void UpdateControlModeText () {
        var controlsButtonText = transform.Find ("ControlsButton").Find ("Text").GetComponent<TextMeshProUGUI> ();
        controlsButtonText.text = string.Format ("CONTROLS: {0}", (gameSettings.controlMode == GameControlMode.Gyroscope ? "Gyroscope" : @"Joystick\Mouse"));
    }

    public void TogglePostProcessing () {
        gameSettings.enablePostProcessing = !gameSettings.enablePostProcessing;
        gameSettings.SaveSettings ();
        UpdateEnablePostProcessingText ();
    }

    private void UpdateEnablePostProcessingText () {
        var enablePostProcessingText = transform.Find ("EnablePostProcessing").Find ("Text").GetComponent<TextMeshProUGUI> ();
        enablePostProcessingText.text = string.Format ("Post Processing: {0}", (gameSettings.enablePostProcessing ? "Enabled" : "Disabled"));
    }
}