using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnsTest : MonoBehaviour
{
    public Animator anim;

    public bool respawnActive;

    Vector2 checkpointPos;
    Rigidbody2D playerRb;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        checkpointPos = transform.position;
        anim.SetBool("Die", false);
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
            respawnActive = true;
        }

        else
        {
            respawnActive = false;
            anim.SetBool("Die", false);
        }
    }

    void Die()
    {
        StartCoroutine(TimeCount(0f));
        StartCoroutine(Respawn(2f));
    }

    IEnumerator TimeCount(float duration)
    {
        anim.SetBool("Die", true);
        yield return new WaitForSeconds(duration);
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.simulated = false;
        yield return new WaitForSeconds(duration);
        transform.localScale = new Vector3(0, 0, 0);
        //yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        transform.localScale = new Vector3(1, 1, 1);
        playerRb.simulated = true;
    }
}