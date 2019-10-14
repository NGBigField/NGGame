using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameoverScreen : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Show(float score)
    {
        var scoreText = transform.Find("GameoverScore").GetComponent<Text>();
        scoreText.text = string.Format("YOUR SCORE IS {0}!", score);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        StartCoroutine(PlayHideAnimation());
    }

    private IEnumerator PlayHideAnimation()
    {
        animator.SetBool("isGameOver", false);
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        animator.SetBool("isGameOver", true);
    }
}
