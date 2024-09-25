using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public GameObject footstep;
    void Start()
    {
        footstep.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        void footsteps()
        {
            footstep.SetActive(true);
        }

        void StopFootsteps()
        {
            footstep.SetActive(false);
        }

        if (Input.GetKey(KeyCode.W))
        {
            footsteps();
        }
        if (Input.GetKey(KeyCode.A))
        {
            footsteps();
        }
        if (Input.GetKey(KeyCode.S))
        {
            footsteps();
        }
        if (Input.GetKey(KeyCode.D))
        {
            footsteps();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            footsteps();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            footsteps();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            footsteps();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            footsteps();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            StopFootsteps();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            StopFootsteps();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            StopFootsteps();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            StopFootsteps();
        }
    }
}
