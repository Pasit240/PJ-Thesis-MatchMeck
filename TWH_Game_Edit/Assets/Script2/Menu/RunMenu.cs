using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RunMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject runMenu;

    void Start()
    {
        runMenu.SetActive(true);
    }

    public void Puase()
    {
        Time.timeScale = 1f;
        runMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Puase();
        }
    }
}
