using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float speed;
    public float input;
    public SpriteRenderer spriteRenderer;
    public float JumpForce;
    //public Animator animator;
    public bool isJumping;
    //AudioSource audioSource;

    bool facingRight = true;


    public float distance = 1f;
    //GameObject box;

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody2D>();
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetFloat("Speed", Mathf.Abs(input));

        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down * transform.localScale.x, distance);

        if (Input.GetButtonDown("Jump") && hit.collider != null)
        {
            playerRb.velocity = Vector2.up * JumpForce;
        //    animator.SetBool("Is Jumping", true);
        //    //box = hit.collider.gameObject;
        //}
        //else
        //{
        //    animator.SetBool("Is Jumping", false);
        }

        input = Input.GetAxisRaw("Horizontal");
        //if (input < 0)
        //{
        //    spriteRenderer.flipX = true;
        //}
        //else if (input > 0)
        //{
        //    spriteRenderer.flipX = false;
        //}

        if(input < 0 && facingRight)
        {
            Flip();
        }
        if (input > 0 && !facingRight)
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            playerRb.velocity = Vector2.up * JumpForce;
        }

        //if (playerRb.velocity.x != 0)
        //{
        //    if (!audioSource.isPlaying)
        //    {
        //        audioSource.Play();
        //    }
        //}
        //else
        //{
        //    audioSource.Stop();
        //}

    }

    private void FixedUpdate()
    {
        playerRb.velocity = new Vector2(input * speed, playerRb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + distance * transform.localScale.y * Vector2.down);
    }

}