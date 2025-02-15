using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject runMenu;

    //public GameObject EndDemo;

    //private void Update()
    //{

    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        Continue();
    //    }
    //}

    public bool _isPause;
    //public bool _isEndDemo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);




        }
        //if (collision.CompareTag("Player"))
        //{
        //    pauseMenu.SetActive(false);
        //    EndDemo.SetActive(true);
        //    Time.timeScale = 0f;
        //    _isEndDemo = true;
        //}
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        _isPause = false;
        //EndDemo.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.Escape) && !_isPause /*&& !_isEndDemo*/)
        {
            Puase();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && _isPause /*&& !_isEndDemo*/)
        {
            Continue();
        }
    }

    public void RestartGame()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        Time.timeScale = 1;
        //SceneManager.GetActiveScene();
        //Time.timeScale = 1f;
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
