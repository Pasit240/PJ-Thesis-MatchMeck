using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayGame();
        }
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("Map02");
        Time.timeScale = 1f;
    }
}
