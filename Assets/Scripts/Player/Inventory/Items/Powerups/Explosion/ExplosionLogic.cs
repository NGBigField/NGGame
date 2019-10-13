using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLogic : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    private float alphaValue = 0.5f;
    private float alphaIncrementRatio = 0.8f;
    private float curExplosionRadius;

    private float explosionForce = 80.0f;
    private float finalExplosionRadius = 15.0f;
    private float explosionRadiusInc = 0.4f;
    private float explosionLifeTime = 1.4f;

    // Start is called before the first frame update
    void Start()
    {
        // go through each enemy, and add explosion force
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            var rigidBody = enemy.GetComponent<Rigidbody>();
            rigidBody.AddExplosionForce(explosionForce, transform.position, finalExplosionRadius);
        }

        curExplosionRadius = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {

        if (curExplosionRadius < finalExplosionRadius)
        {
            curExplosionRadius += explosionRadiusInc;
            this.transform.localScale = new Vector3(curExplosionRadius, curExplosionRadius, curExplosionRadius);

            if (alphaValue > 0.0f && curExplosionRadius > (finalExplosionRadius / 5))
            {

                alphaValue = alphaValue*alphaIncrementRatio;
                alphaValue = Mathf.Max(0, alphaValue);
            }
            meshRenderer.material.SetFloat("_Opacity", alphaValue); //update Opacity

        }
        else
        {
            Destroy(this.gameObject, explosionLifeTime);
        }

    }
}