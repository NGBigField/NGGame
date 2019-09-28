using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float force = 20.0f;
    private float jumpVelocity = 5.0f;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        var forwardVec = Camera.main.transform.forward;
        forwardVec.y = 0;
        var sideVec = Quaternion.AngleAxis(90, Vector3.up) * forwardVec;

        rb.AddForce(forwardVec * force * Time.deltaTime * vertical  , ForceMode.Impulse);
        rb.AddForce(sideVec    * force * Time.deltaTime * horizontal, ForceMode.Impulse);

        //Jump:
        var jumpTriggered = Input.GetButtonDown("Jump");
        if (jumpTriggered && isGrounded)
        {

            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.VelocityChange);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Ground") isGrounded = true;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "Ground") isGrounded = false;
    }
}
