using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour
{
    public Rigidbody2D playerRb;
    private Vector2 moveInput;
    public float speed;
    public float input;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    bool facingRight = true;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(input));

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        
        moveInput.Normalize();

        playerRb.velocity = moveInput * speed;


        input = Input.GetAxisRaw("Horizontal");
        if (input < 0 && facingRight)
        {
            flip();
        }
        if (input > 0 && !facingRight)
        {
            flip();
        }
    }

    
}

