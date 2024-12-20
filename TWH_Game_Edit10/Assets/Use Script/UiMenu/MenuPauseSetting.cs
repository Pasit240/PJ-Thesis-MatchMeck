using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPauseSetting : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject runMenu;

    public bool _isPaused;
    public bool _isRuned;

    void Start()
    {
        pauseMenu.SetActive(false);
        runMenu.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _isPaused)
        {
            Continue();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && _isRuned)
        {
            Puase();
        }       
    }

    public void Continue()
    {
        _isPaused = false;
        _isRuned = true;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        runMenu.SetActive(true);
    }

    public void Puase()
    {
        _isPaused = true;
        _isRuned = false;
        Time.timeScale = 0f;
        runMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
