using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideEnemy : MonoBehaviour
{
    private float vertical;

    public float stealthCooldown = 2f;

    private bool isHiding = false;
    private bool canHide = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //void Update()
    //{
    //    vertical = Input.GetAxis("Vertical");
    //    if (canHide && Mathf.Abs(vertical) > 0f)
    //    {
    //        isHiding = true;
    //    }
    //}

    void Update()
    {
        if (isHiding)
        {
            if (canHide && Input.GetKey(KeyCode.W))
            {
                rb.gravityScale = 0f;
                rb.velocity = new Vector2(0,0);
                CheckHide();
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                rb.gravityScale = 0f;
            }
        }
    }

    void CheckHide()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("StealthObject"))
            {
                Hide();
                break;
            }
        }
    }

    void Hide()
    {
        isHiding = true;
        canHide = false;

        GetComponent<Collider2D>().enabled = false;

        Invoke("ResetHideCooldown", stealthCooldown);
    }

    void ResetHideCooldown()
    {
        isHiding = false;
        canHide = true;

        GetComponent<Collider2D>().enabled = true;
    }
}
