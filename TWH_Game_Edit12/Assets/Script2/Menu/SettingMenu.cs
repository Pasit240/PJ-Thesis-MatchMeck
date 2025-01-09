using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Stage1");
        Time.timeScale = 1f;
    }

    public void Mainmenu()
    {
        SceneManager.LoadScene("Mainmenu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Time.timeScale = 1f;
    }
}
