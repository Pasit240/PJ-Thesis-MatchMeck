using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    public Movement movement;
    public Movement1 movement1;
    private bool player1Active = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchPlayer();
        }
    }

    public void SwitchPlayer()
    {
        if (player1Active)
        {
            movement.enabled = false;
            movement1.enabled = true;
            player1Active = false;
        }
        else
        {
            movement.enabled = true;
            movement1.enabled = false;
            player1Active = true;
        }
    }
}
