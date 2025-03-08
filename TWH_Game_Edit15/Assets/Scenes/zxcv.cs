using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class zxcv : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DelaySce(5f));
    }

    IEnumerator DelaySce(float duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene("MainMenu");
    }
}
