using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class qwer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelaySce(40f));
    }

    IEnumerator DelaySce(float duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene("MainMenu");
    }

}
