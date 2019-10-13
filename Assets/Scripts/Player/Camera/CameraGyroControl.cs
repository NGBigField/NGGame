using UnityEngine;

/// <summary>
/// Allows controling the player via the gyroscope sensor.
/// TODO: Move this code with the camera movement script instead of 2 seperate scripts
/// </summary>
public class CameraGyroControl : MonoBehaviour
{
    public Transform playerTransform;
    private bool gyroEnabled;
    private Gyroscope gyro;
    private Quaternion rot;

    private GameObject cameraContainer;

    public Vector3 offset;
    private float offsetRadius;

    private void Start()
    {
        gyroEnabled = EnableGyro();
        offsetRadius = offset.magnitude;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            /* calc rotation */
            transform.localRotation = gyro.attitude * rot;


            /*calc position */
            float x = transform.localRotation.eulerAngles.x;
            //if (x > 180f)  x-=360f;
            float y = transform.localRotation.eulerAngles.y;
            float z = transform.localRotation.eulerAngles.z;
            z -= 180f;

            Vector3 lookVec = new Vector3(x, y, z);

            x = playerTransform.position.x - offsetRadius * lookVec.normalized.x;

            //y = playerTransform.position.y  - offsetRadius * lookVec.normalized.y;
            y = playerTransform.position.y;

            z = playerTransform.position.z;// - offsetRadius * lookVec.normalized.z;

            cameraContainer.transform.position = new Vector3(x, y, z);

            Debug.Log(new Vector2(transform.localRotation.eulerAngles.z, lookVec.z));
            Debug.Log(transform.localRotation.eulerAngles);


        }
    }


    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            cameraContainer = new GameObject("Camera Container");
            cameraContainer.transform.position = transform.position;
            transform.SetParent(cameraContainer.transform);
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 270f);
            rot = new Quaternion(0, 0, 1, 0);
            return true;
        }

        return false;
    }
}