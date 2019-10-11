using UnityEngine;

/// <summary>
/// Allows controling the player via the gyroscope sensor.
/// TODO: Move this code with the camera movement script instead of 2 seperate scripts
/// </summary>
public class CameraGyroControl : MonoBehaviour {
    public Transform playerTransform;
    private bool gyroEnabled;
    private Gyroscope gyro;
    private Quaternion rot;

    public Vector3 offset;

    private void Start () {
        gyroEnabled = EnableGyro ();
    }

    private void Update () {
        if (gyroEnabled) {
            transform.localRotation = gyro.attitude * rot;
            transform.position = playerTransform.position + offset;
        }
    }

    private bool EnableGyro () {
        if (SystemInfo.supportsGyroscope) {
            gyro = Input.gyro;
            gyro.enabled = true;
            rot = new Quaternion (0, 0, 1, 0);
            return true;
        }

        return false;
    }
}