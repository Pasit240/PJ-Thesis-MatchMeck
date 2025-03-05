using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnsTest : MonoBehaviour
{
    [SerializeField] Animator transitionRe;

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
        }
    }

    void Die()
    {
        StartCoroutine(TimeCount(1f));
        StartCoroutine(Respawn(3f));
    }

    IEnumerator TimeCount(float duration)
    {
        transitionRe.SetTrigger("Start");
        yield return new WaitForSeconds(duration);
        transitionRe.SetTrigger("Respawn");
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.simulated = false;
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        playerRb.simulated = true;
    }
}