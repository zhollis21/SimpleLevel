using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigateToMainMenu : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}