using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLogic : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    private float alphaValue = 0.3f;
    private float alphaValueInc = 0.025f;

    private float explosionForce = 80.0f;
    private float explosionRadius = 15.0f;
    private float explosionInc = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // go through each enemy, and add explosion force
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            var rigidBody = enemy.GetComponent<Rigidbody>();
            rigidBody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.localScale.x < explosionRadius)
        {
            this.transform.localScale =
            new Vector3(
                this.transform.localScale.x + explosionInc,
                this.transform.localScale.y,
                this.transform.localScale.z + explosionInc
                 );

            var color = meshRenderer.material.color;
            if (alphaValue > 0.0f)  alphaValue -= alphaValueInc;
            color.a = alphaValue;

            meshRenderer.material.color = color;
        }
        else
        {
            Destroy(this.gameObject, 1.6f);
        }

    }
}
