using UnityEngine;

public class gunMovement : MonoBehaviour
{
    public GameObject gun;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = gun.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        /*Gun position */
        Vector3 playerPosition = player.transform.position;
        Vector3 newPosition = new Vector3(playerPosition.x, playerPosition.y + 0.7f, playerPosition.z + 0.0f);

        /*Gun Orientation */
        Vector3 fireVec = player.GetComponent<PlayerLogic>().fireVec;   //pointing gun relative to fire vector

        gun.transform.SetPositionAndRotation(newPosition, Quaternion.identity);
        gun.transform.LookAt(playerPosition + fireVec * 1.1f + Vector3.up * (-2.0f + fireVec.y * 5));

    }
}
