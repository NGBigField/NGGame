using UnityEngine;

public class PlasmaChargeBulletLogic : MonoBehaviour {
    public const float MaxSize = 1f;

    private float currentSize;

    private float sizeIncValue = 0.05f;

    // Start is called before the first frame update
    void Start () {
        // Take the start value from the local scale X value
        currentSize = transform.localScale.x;
    }

    // Update is called once per frame
    void Update () {
        currentSize = Mathf.Min (currentSize + (sizeIncValue * Time.deltaTime), MaxSize);
        transform.localScale = new Vector3 (1, 1, 1) * currentSize;
    }
}