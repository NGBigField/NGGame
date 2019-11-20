using UnityEngine;

public class GravityWeapon : BaseWeapon {
    private bool isHoldingObject = false;

    /// <summary>
    /// True if the held item is close enough and now captured.
    /// </summary>
    ///
    private bool isHeldItemCaptured = false;

    private Rigidbody heldRigidbody;

    /// <summary>
    /// The currently raycasted object (the item the crosshair is pointing to) or already held object.
    /// </summary>
    private GameObject raycastedObject;
    public float maxRayDistance = 25;
    public float gravityPullForce = 10.0f;

    public float gravityShootForce = 50.0f;

    public float itemCapturedRotationSpeed = 100.0f;

    public override string Name => "Gravity";

    public override string IconPath => "Icons/S_divine";

    private bool IsRaycastedObjectGrabbable => !!heldRigidbody;

    protected override void Awake () {
        base.Awake ();
        bullets = BaseWeapon.INFINITE_BULLETS;
    }

    protected void FixedUpdate () {
        // If the gravity weapon is not holding something
        if (isHoldingObject) {
            // If this object contains a rigidbody
            if (heldRigidbody) {
                // If we still haven't captured the item
                if (!isHeldItemCaptured)
                    PullObject ();
                else
                    ItemCapturedLogic ();
            }
        } else FindObjectToHold ();
    }

    /// <summary>
    /// The logic to perform when the item is already captured.
    /// </summary>
    private void ItemCapturedLogic () {
        KeepItemAtCapturePoint ();
        RotateItemAroundItself ();
    }

    /// <summary>
    /// Keeps the item close to us without using rigidbody.
    /// </summary>
    private void KeepItemAtCapturePoint () {
        raycastedObject.transform.position = GetCaptureToVector ();
    }

    /// <summary>
    /// Rotates the held item to create the gravity effect.
    /// </summary>
    private void RotateItemAroundItself () {
        raycastedObject.transform.Rotate (Vector3.up * itemCapturedRotationSpeed * Time.deltaTime, Space.Self);
    }

    /// <summary>
    /// Pulls the item we want to magnet torwards us using it's rigidbody.
    /// </summary>
    private void PullObject () {
        // --> This code pulls the object using forces instead
        // var forceVector = (GetPullToVector () - heldRigidbody.transform.position) * gravityPullForce * Time.deltaTime;
        // heldRigidbody.AddForce (forceVector, ForceMode.Force);

        // Disable the gravity for the object to allow pulling it to the exact location we want
        heldRigidbody.useGravity = false;

        var captureToVector = GetCaptureToVector ();

        // Pull the object towards the pull position
        raycastedObject.transform.position = Vector3.MoveTowards (raycastedObject.transform.position, captureToVector, gravityPullForce * Time.deltaTime);

        // Check if the item moved to the destination
        if (raycastedObject.transform.position == captureToVector) {
            isHeldItemCaptured = true;
            heldRigidbody.isKinematic = true;
        }
    }

    /// <summary>
    /// Returns the vector we want the object to move torwards to and be captured in.
    /// </summary>
    /// <returns></returns>
    private Vector3 GetCaptureToVector () {
        return transform.position + playerControl.getFireVector () * 3.5f;
    }

    /// <summary>
    /// Detect any object we can use to hold using raycast.
    /// </summary>
    private void FindObjectToHold () {
        // If we are not hodling any object, detect if there is something we can grab
        var fireVec = playerControl.getFireVector ();

        // Draws a line to show the raycast
        // Debug.DrawLine (transform.position, transform.position + fireVec * maxRayDistance, Color.red);

        // Create a raycast
        var ray = new Ray (transform.position, fireVec * maxRayDistance);

        // Store the hitted object if there is any
        RaycastHit hit;

        // If we hit a gameobject using raycast, we can lift the object using the gravity weapon
        if (Physics.Raycast (ray, out hit, maxRayDistance)) {
            raycastedObject = hit.collider.gameObject;
            heldRigidbody = raycastedObject.GetComponent<Rigidbody> ();
        } else
            raycastedObject = null;
    }

    public override void OnShootDown (Vector3 fireVec, Transform playerTransform, bool isTouch = false) {
        if (isHoldingObject) {
            // If we are holding the object and it's a touch, release it as on mobile OnShootDown is only called
            if (isTouch) StopGrabbingObject (fireVec);
            return;
        }

        if (raycastedObject) {
            StartGrabbingObject ();
        } else
            audioSource.PlayOneShot (GameRepository.Instance.gravityWeaponMissSound);
    }

    private void StartGrabbingObject () {
        isHoldingObject = true;

        // If this object is actually grab-able
        if (IsRaycastedObjectGrabbable) {
            audioSource.PlayOneShot (GameRepository.Instance.gravityWeaponHoldingSound);
        }
        // If it's not just play the shoot miss sound
        else audioSource.PlayOneShot (GameRepository.Instance.gravityWeaponMissSound);
    }

    public override void OnShootUp (Vector3 fireVec, Transform playerTransform, bool isTouch = false) {
        if (isHoldingObject) StopGrabbingObject (fireVec);
    }

    private void StopGrabbingObject (Vector3 fireVec) {
        audioSource.Stop ();
        isHoldingObject = false;

        // Shoot the object away
        if (heldRigidbody) {

            // Return the gravity back to the object
            heldRigidbody.useGravity = true;

            // Return the physics back to the object
            heldRigidbody.isKinematic = false;

            // If this item has been captured, we can throw it away
            if (isHeldItemCaptured) {
                // Add a force to shoot it away
                heldRigidbody.AddForce (fireVec * gravityShootForce, ForceMode.Force);

                // Play associated shooting sound
                audioSource.PlayOneShot (GameRepository.Instance.gravityWeaponShootingSound);
            }
        }

        // Clear all flags for capturing
        isHeldItemCaptured = false;
        heldRigidbody = null;
        raycastedObject = null;
    }
}