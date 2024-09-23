using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfDemo : MonoBehaviour
{
    public void LoadNewScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndGame");
    }

}
