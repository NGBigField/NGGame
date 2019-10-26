using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    public EventSystem eventSystem;

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(GameObject.Find("PlayButton"));
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
