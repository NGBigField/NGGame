using UnityEngine;

public class ExplosionIcon : MonoBehaviour
{
    private GameObject icon;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        icon = gameObject;
        player = icon.transform.parent.gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setVisible(bool isVisible)  //its taking int because maybe we'll want more objects
    {
        if (isVisible)
        {
            icon.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            icon.transform.localScale = new Vector3(0, 0, 0);
        }
    }

}
