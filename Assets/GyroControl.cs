using UnityEngine;

/// <summary>
/// This script moves the camera according to the device gyroscope sensor, if available.
/// </summary>
public class GyroControl : MonoBehaviour {
    private bool gyroEnabled;
    private Gyroscope gyro;
    private Quaternion rot;

    private void Start () {
        gyroEnabled = EnableGyro ();
    }

    private void Update () {
        if (gyroEnabled) {
            // TODO: Insert our update code according to the gyroscope location
        }
    }

    private bool EnableGyro () {
        if (SystemInfo.supportsGyroscope) {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }

        return false;
    }
}