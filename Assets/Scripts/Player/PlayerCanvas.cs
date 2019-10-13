using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    public Image damageBlinkImage;
    public CanvasGroup crosshair;

    public Text scoreText;

    public HealthBar healthBar;
    public ExplosionIcon explosionIcon;

    private void Start()
    {
        // --> Comment this line to allow platform specific modifications take place
        // DoPlatformModifications();
    }

    ///<summary>
    /// Make changes according to the platform this game is being played on. For example, don't show touch controls on desktop.
    /// </summary>
    private void DoPlatformModifications()
    {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX // If we are on desktop
        // Hide the cursor on desktop
        Cursor.visible = false;

        // Destroy the touch controls on desktop
        var touchControls = transform.Find("Touch Controls");
        Destroy(touchControls.gameObject);
#endif
    }
    public void HideCrosshair()
    {
        crosshair.alpha = 0.0f;
    }

    public void ShowCrosshair()
    {
        crosshair.alpha = 1.0f;
    }

    public void SetHealth(float value)
    {
        healthBar.SetValue(value);
    }

    public void SetScore(float value)
    {
        scoreText.text = string.Format("Score: {0}", value);
    }

    public void DoDamageBlink()
    {
        StartCoroutine(DamageBlinkScreen());
    }

    private IEnumerator DamageBlinkScreen()
    {
        var color = damageBlinkImage.color;
        color.a = 0.9f;
        damageBlinkImage.color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 0f;
        damageBlinkImage.color = color;
    }
}