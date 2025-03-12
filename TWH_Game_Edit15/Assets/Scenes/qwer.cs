using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class qwer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DelaySce(40f));

        
    }

    public void Update()
    {
        if (InputManager._isDebugOpen)
        {
            Skip();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Skip();
        }
    }

    IEnumerator DelaySce(float duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene("Map01");
    }

    public void Skip()
    {
        SceneManager.LoadScene("Map01");
    }

}
