using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGameClicked()
    {
        // ToDo: Create a new save file or wipe the old one
        SceneManager.LoadScene("GrassLevel1");
    }

    public void LoadGameClicked()
    {
        // ToDo: Load their game stats
        SceneManager.LoadScene("GrassOverworld");
    }

    public void QuitGameClicked()
    {
        Application.Quit();
    }
}
