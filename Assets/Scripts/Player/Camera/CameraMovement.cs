using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public float turnSpeedX = 4.0f;
    public float turnSpeedY = 2.0f;
    public Transform playerTransform;

    public Vector3 offset;
    private float camereDistance = 4.47f;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (GameManager.Instance.isGameOver) return;
        transform.position = playerTransform.position + offset;
    }

    public void MoveCamera (float axisX, float axisY) {
        if (GameManager.Instance.isGameOver) return;
        var forwardVec = transform.forward;
        forwardVec.y = 0;
        var sideVec = Quaternion.AngleAxis (90, Vector3.up) * forwardVec;

        offset = Quaternion.AngleAxis (axisX * turnSpeedX, Vector3.up) * offset;

        // TODO: Explain this code
        offset = Quaternion.AngleAxis (axisY * turnSpeedY, -sideVec) * offset;
        offset.y = Mathf.Max (Mathf.Min (offset.y, 2.38f), 0.0f);
        offset = offset.normalized * camereDistance;
        transform.position = playerTransform.position + offset;
        transform.LookAt (playerTransform.position);
    }

    void LateUpdate () {
        // If it's a touch pointer movement and not a mouse movement, ignore it
        if (Input.touchSupported && Input.touchCount > 0) return;

        // If it's a normal mouse or joystick movement, accept it and move accordingly
        var axisX = Input.GetAxis ("Mouse X");
        var axisY = Input.GetAxis ("Mouse Y");
        MoveCamera (axisX, axisY);
    }
}