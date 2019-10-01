using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Image damageBlinkImage;

    public HealthBar healthBar;


    private float health = 1.0f;
    private float score = 0.0f;

    private void Start()
    {
        var damageBlink = GameObject.FindGameObjectWithTag("DamageBlink");
        damageBlinkImage = damageBlink.GetComponent<Image>();
    }

    public void OnPlayerHit(float value)
    {
        StartCoroutine(BlinkScreen());
        this.health = Mathf.Max(0.0f, this.health - value);
        healthBar.SetValue(this.health);

        if (health == 0.0f) GameManager.Instance.Endgame();
    }

    private IEnumerator BlinkScreen()
    {
        var color = damageBlinkImage.color;
        color.a = 0.9f;
        damageBlinkImage.color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 0f;
        damageBlinkImage.color = color;
    }

    public void IncreaseScore(float value)
    {
        this.score += value;
    }
}