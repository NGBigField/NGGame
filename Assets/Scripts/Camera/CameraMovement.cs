using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float turnSpeedX = 4.0f;
    public float turnSpeedY = 2.0f;
    public Transform playerTransform;

    public Vector3 offset;
    private float camereDistance = 4.47f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + offset;
    }

    void LateUpdate()
    {
        var forwardVec = transform.forward;
        forwardVec.y = 0;
        var sideVec = Quaternion.AngleAxis(90, Vector3.up) * forwardVec;


        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        offset = Quaternion.AngleAxis(mouseX * turnSpeedX, Vector3.up) * offset;

        // TODO: Explain this code
        offset = Quaternion.AngleAxis(mouseY * turnSpeedY, -sideVec) * offset;
        offset.y = Mathf.Max(Mathf.Min(offset.y, 2.38f), 0.0f);
        offset = offset.normalized * camereDistance;
        transform.position = playerTransform.position + offset;
        transform.LookAt(playerTransform.position);
    }
}
