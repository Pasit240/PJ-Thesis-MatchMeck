using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightJumppad : MonoBehaviour
{
    public float bounce = 35;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * bounce, ForceMode2D.Impulse);
        }
    }
}
