using UnityEngine;

public class WeaponStand : MonoBehaviour {
    /// <summary>
    ///  The offset on the y axis from the ball.
    /// </summary>
    public float yOffset = 0.234f;

    public Transform weaponTransform;

    protected Transform playerTransform;

    public PlayerControl playerControl;

    private void Awake () {
        playerTransform = transform.parent.transform;
        playerControl = playerTransform.GetComponent<PlayerControl> ();
        weaponTransform = transform.Find ("Weapon");
    }

    // Update is called once per frame
    void Update () {
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3 (playerTransform.position.x,
            playerTransform.position.y + yOffset,
            playerTransform.position.z);

        /* Gun Orientation */
        Vector3 fireVec = playerControl.fireVec; //pointing gun relative to fire vector
        weaponTransform.LookAt (playerTransform.position + fireVec * 1.1f + Vector3.up * (-2.0f + fireVec.y * 5));
        weaponTransform.Rotate (-65, 0, 0);
    }
}