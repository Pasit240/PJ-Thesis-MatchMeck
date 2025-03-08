using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEndGame : MonoBehaviour
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
        SceneManager.LoadScene("CSEnd");
        Time.timeScale = 1f;
        StartCoroutine(DelaySce(5.30f));
    }

    IEnumerator DelaySce(float duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene("MainMenu");
    }
}
