using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;

    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar");
    }

    void SetHealth(float value)
    {
        bar.localScale = new Vector3(value, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
