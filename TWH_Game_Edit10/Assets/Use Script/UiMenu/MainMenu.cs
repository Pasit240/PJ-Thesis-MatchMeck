using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Map00");
        Time.timeScale = 1f;
    }

    //public void Mainmenu()
    //{
    //    SceneManager.LoadScene("");
    //}

    public void QuitGame()
    {
        Application.Quit();
    }
}