using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeSpawn : MonoBehaviour
{
    public GameObject DeSpawn;
    public GameObject InviWall;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DeSpawn.SetActive(false);
        }

        if (collision.CompareTag("Player"))
        {
            InviWall.SetActive(true);
        }
    }
}
