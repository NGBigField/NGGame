using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHit : MonoBehaviour
{
    public GameObject damageBlink;

    public Image damageBlinkImage;

    // Start is called before the first frame update
    void Start()
    {
        var damageBlink = GameObject.FindGameObjectWithTag("DamageBlink");
        damageBlinkImage = damageBlink.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Enemy")
        {
            StartCoroutine(onEnemyHit());
        }
    }

    IEnumerator onEnemyHit()
    {
        var color = damageBlinkImage.color;
        color.a = 0.9f;
        damageBlinkImage.color = color;
        GameManager.OnPlayerHit(25.0f);
        yield return new WaitForSeconds(0.1f);
        color.a = 0f;
        damageBlinkImage.color = color;
    }
}
