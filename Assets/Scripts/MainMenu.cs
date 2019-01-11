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
        //ToDo: Replace with Level 1
        SceneManager.LoadScene("Zach's Scene");
    }

    public void LevelSelectionClicked()
    {
        //ToDo: Create Level Selction Menu
    }

    public void OptionsClicked()
    {
        //ToDo: Create Options
    }
}
