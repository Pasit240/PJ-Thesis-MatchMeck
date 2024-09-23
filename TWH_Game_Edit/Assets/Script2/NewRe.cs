using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRe : MonoBehaviour
{
    public RespawnPlayer respawn;
    public RespawnPlayer2 respawn2;


    private void OnTriggerEnter(Collider colision)
    {
        if (colision.CompareTag("Player"))
        {
            respawn.enabled = false;
            respawn2.enabled = true;
        }
    }
}
