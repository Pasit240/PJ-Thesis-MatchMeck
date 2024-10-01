using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D playerRb;
    Collider2D player_ObjectCollider;

    private void Start()
    {
        player_ObjectCollider = GetComponent<Collider2D>();
    }

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRb.gravityScale = 3f;
            player_ObjectCollider.isTrigger = true;

            StartCoroutine(DestroyPlatformAfterDelay(2f));
        }
    }

    IEnumerator DestroyPlatformAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}
