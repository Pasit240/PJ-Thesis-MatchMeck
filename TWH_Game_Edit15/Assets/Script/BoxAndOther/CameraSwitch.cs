using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public GameObject camera1;
    public GameObject camera2;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            camera1.SetActive(false);
            camera2.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            camera1.SetActive(true);
            camera2.SetActive(false);
        }
    }
}
