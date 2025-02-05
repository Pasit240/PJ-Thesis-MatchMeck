using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject runMenu;

    //private void Update()
    //{

    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        Continue();
    //    }
    //}

    public bool _isPause;

    private void Start()
    {
        pauseMenu.SetActive(false);
        _isPause = false;
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        runMenu.SetActive(true);
        _isPause = false;
    }

    public void Puase()
    {
        Time.timeScale = 0f;
        runMenu.SetActive(false);
        pauseMenu.SetActive(true);
        _isPause = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isPause)
        {
            Puase();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && _isPause)
        {
            Continue();
        }
    }

    public void RestartGame()
    {
        SceneManager.GetActiveScene();
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
