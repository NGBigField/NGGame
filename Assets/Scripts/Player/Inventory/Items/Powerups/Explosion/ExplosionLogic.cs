using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLogic : MonoBehaviour {
    public MeshRenderer meshRenderer;

    private float alphaValue = 0.3f;
    private float alphaValueInc = 0.025f;

    private float explosionForce = 80.0f;
    private float explosionRadiusFinal = 15.0f;
    private float explosionRadiusInc = 1f;
    private float explosionLifeTime = 1.4f;

    // Start is called before the first frame update
    void Start () {
        // go through each enemy, and add explosion force
        var enemies = GameObject.FindGameObjectsWithTag ("Enemy");

        foreach (var enemy in enemies) {
            var rigidBody = enemy.GetComponent<Rigidbody> ();
            rigidBody.AddExplosionForce (explosionForce, transform.position, explosionRadiusFinal);
        }
    }

    // Update is called once per frame
    void Update () {
        if (this.transform.localScale.x < explosionRadiusFinal) {
            this.transform.localScale =
                new Vector3 (
                    this.transform.localScale.x + explosionRadiusInc,
                    this.transform.localScale.y + explosionRadiusInc,
                    this.transform.localScale.z + explosionRadiusInc
                );

            // var color = meshRenderer.material.color;
            // if (alphaValue > 0.0f) alphaValue -= alphaValueInc;
            // color.a = alphaValue;

            // meshRenderer.material.color = color;
        } else {
            // Destroy (this.gameObject, explosionLifeTime);
        }

    }
}