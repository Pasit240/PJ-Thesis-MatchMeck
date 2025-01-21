using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D playerRb;
    FixedJoint2D Freeze;

    Vector2 checkpointPos;

    public bool test;

    public RespawnsTest RespawnPlayer;

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }

    private void Update()
    {
        if (RespawnPlayer.respawnActive)
        {
            Freeze.enabled = true;
            playerRb.simulated = false;
            transform.position = new Vector3(0, 0, 0);
            transform.position = checkpointPos;
            playerRb.simulated = true;
        }
    }

    private void FixedUpdate()
    {
        playerRb.gravityScale = 1f;
    }

    private void Start()
    {
        Freeze = GetComponent<FixedJoint2D>();
        Freeze.enabled = true;
        checkpointPos = transform.position;
    }

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //StartCoroutine(DestroyPlatformAfterDelay(10f));
            StartCoroutine(GravityActive(2f));
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }

    }

    IEnumerator GravityActive(float duration)
    {
        yield return new WaitForSeconds(duration);
        Freeze.enabled = false;
        playerRb.gravityScale = 1f;

    }



    //IEnumerator DestroyPlatformAfterDelay(float delay)
    //{
    //    yield return new WaitForSeconds(delay);

    //    Destroy(gameObject);
    //}
}