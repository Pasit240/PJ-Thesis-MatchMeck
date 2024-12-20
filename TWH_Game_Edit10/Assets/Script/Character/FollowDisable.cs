using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDisable : MonoBehaviour
{
    public Subcharacter subcharacter;
    public GrabObjectsGhost grabObjectsGhost;

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
            grabObjectsGhost.enabled = true;
            subcharacter.enabled = false;
            player1Active = false;
        }
        else
        {
            grabObjectsGhost.enabled = false;
            subcharacter.enabled = true;
            player1Active = true;
        }
    }
}
