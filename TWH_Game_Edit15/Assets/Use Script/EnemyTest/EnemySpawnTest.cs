using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnTest : MonoBehaviour
{
    public GameObject Spawn;
    public GameObject InviWall;

    public RespawnsTest RespawnPlayer;

    private void Update()
    {
        if (RespawnPlayer.respawnActive)
        {
            StartCoroutine(SetDelay(1.5f));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Spawn.SetActive(true);
        }

        if (collision.CompareTag("Player"))
        {
            InviWall.SetActive(false);
        }
    }

    private void Start()
    {
        Spawn.SetActive(false);
    }

    IEnumerator SetDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        Spawn.SetActive(false);
    }
}
