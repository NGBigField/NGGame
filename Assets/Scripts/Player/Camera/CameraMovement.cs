using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    /* Global */
    public bool autoUseGyroscope;
    public Transform playerTransform;
    private float camereDistance = 4.47f;


    /* For Gyro Movement */
    private bool gyroEnabled;
    private Gyroscope gyro;
    private GameObject cameraContainer;
    private Quaternion GyroRotationFactor;



    /* For mouse/touch movement */
    public Vector3 offset;
    public float turnSpeedX = 4.0f;
    public float turnSpeedY = 2.0f;




    void Start()
    {
        if (autoUseGyroscope)
        {
            gyroEnabled = EnableGyro();
        }
        else
        {
            gyroEnabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        if (gyroEnabled)
        {
            /* calc rotation */
            transform.localRotation = gyro.attitude * GyroRotationFactor;

            /*calc position */
            Vector3 lookVec = transform.forward;
            cameraContainer.transform.position = playerTransform.position - camereDistance * lookVec;
        }
        else
        {
            transform.position = playerTransform.position + offset;
        }
    }

    public void MoveCamera(float axisX, float axisY)
    {
        if (gyroEnabled) return;

        if (GameManager.Instance.isGameOver) return;
        var forwardVec = transform.forward;
        forwardVec.y = 0;
        var sideVec = Quaternion.AngleAxis(90, Vector3.up) * forwardVec;

        offset = Quaternion.AngleAxis(axisX * turnSpeedX, Vector3.up) * offset;

        // Shynet: Explain this code
        /* Bigfield: Yes master!  Limit camera hight while still maintaining constant distance from player*/
        offset = Quaternion.AngleAxis(axisY * turnSpeedY, -sideVec) * offset;
        offset.y = Mathf.Max(Mathf.Min(offset.y, 2.38f), 0.0f);
        offset = offset.normalized * camereDistance;
        transform.position = playerTransform.position + offset;
        transform.LookAt(playerTransform.position);
    }

    void LateUpdate()
    {
        if (gyroEnabled) return;

        // If it's a touch pointer movement and not a mouse movement, ignore it
        if (Input.touchSupported && Input.touchCount > 0) return;

        // If it's a normal mouse or joystick movement, accept it and move accordingly
        var axisX = Input.GetAxis("Mouse X");
        var axisY = Input.GetAxis("Mouse Y");
        MoveCamera(axisX, axisY);
    }

    private bool EnableGyro()
    {
        if (autoUseGyroscope && SystemInfo.supportsGyroscope)
        {
            cameraContainer = new GameObject("Camera Container");
            cameraContainer.transform.position = transform.position;
            transform.SetParent(cameraContainer.transform);
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 270f);
            GyroRotationFactor = new Quaternion(0, 0, 1, 0);
            return true;
        }

        return false;
    }
}