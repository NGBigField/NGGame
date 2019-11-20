using UnityEngine;

/// <summary>
/// Contains main player control methods like movement, fire, and secondary fire.
/// </summary>
public class PlayerControl : MonoBehaviour {
    public AudioSource audioSource;
    public Rigidbody rb;
    public Vector3 fireVec;
    public Vector3 forwardVec;
    public Vector3 sideVec;
    public AudioClip jumpSound;
    public AudioClip explosionSound;

    public GameObject bulletPrefab;

    public GameObject explosionPrefab;

    public PlayerManager playerManager;

    public CameraMovement cameraMovement;

    public float kickVelocityFactor = 2f;
    public float force = 20.0f;
    private float jumpVelocity = 6.7f;
    private bool isGrounded;
    public float airMovementFraction = 0.6f;

    private float fireAngle = 15f;

    public WeaponControl WeaponControl {
        get {
            return playerManager.weaponControl;
        }
    }

    public bool InputDisabled {
        get {
            return GameManager.Instance.IsGameFreezed;
        }
    }

    public void NextWeapon () {
        WeaponControl.NextWeapon ();
    }

    public void PreviousWeapon () {
        WeaponControl.PreviousWeapon ();
    }

    public void Move (float horizontal, float vertical) {
        if (InputDisabled) return;
        forwardVec = cameraMovement.transform.forward;
        forwardVec.y = 0;
        sideVec = Quaternion.AngleAxis (90, Vector3.up) * forwardVec;

        if (isGrounded) {
            rb.AddForce (forwardVec * force * Time.deltaTime * vertical, ForceMode.Impulse);
            rb.AddForce (sideVec * force * Time.deltaTime * horizontal, ForceMode.Impulse);
        } else //In Air
        {
            rb.AddForce (forwardVec * airMovementFraction * force * Time.deltaTime * vertical, ForceMode.Impulse);
            rb.AddForce (sideVec * airMovementFraction * force * Time.deltaTime * horizontal, ForceMode.Impulse);
        }

    }
    public void Freeze () {
        rb.velocity = new Vector3 (0, rb.velocity.y, 0);
        rb.rotation = Quaternion.identity;
    }

    public void Jump () {
        if (InputDisabled) return;

        if (isGrounded) {
            audioSource.PlayOneShot (jumpSound);
            rb.AddForce (Vector3.up * jumpVelocity, ForceMode.VelocityChange);
        }
    }

    public void UpdateFireCrosshair () {
        fireVec = getFireVector (); //update this no-matter if fires, so other scripts can use it
    }

    public Vector3 getFireVector () {
        return Quaternion.AngleAxis (fireAngle, -sideVec) * (cameraMovement.transform.forward);
    }

    public void FireDown (bool isTouch = false) {
        if (InputDisabled) return;

        WeaponControl.EquippedWeapon.OnShootDown (fireVec, transform, isTouch);
    }

    public void FireUp (bool isTouch = false) {
        if (InputDisabled) return;

        WeaponControl.EquippedWeapon.OnShootUp (fireVec, transform, isTouch);
    }

    public void UsePowerup (string name) {
        if (InputDisabled) return;

        var inventory = GetComponent<Inventory> ();
        var item = inventory.GetItemByName (name);
        if (item) item.Use ();
    }

    private void OnCollisionEnter (Collision other) {
        if (other.collider.tag == "Ground") isGrounded = true;
    }

    private void OnCollisionExit (Collision other) {
        if (other.collider.tag == "Ground") isGrounded = false;
    }
}