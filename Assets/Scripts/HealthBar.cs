using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;

    // Start is called before the first frame update
    private void Start()
    {
        bar = transform.Find("Bar");
    }

    public void SetValue(float value)
    {
        bar.localScale = new Vector3(value, 1.0f);
    }
}
