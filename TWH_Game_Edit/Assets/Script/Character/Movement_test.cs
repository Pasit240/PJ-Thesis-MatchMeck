using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Movement_test : MonoBehaviour
{
    public Animator animator;

    private float horizontal;
    public float speed = 7f;
    public float jumpingPower = 12f;
    private bool isFacingRight = true;

    public bool canJump;

    public float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    public float _velocity;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask pullableLayer;

    PlayerState state;
    bool StateComplete;
    enum PlayerState {Idle, Move, Jump }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void SelectAnimation()
    {
        StateComplete = false;
        if (groundCheck)
        {
            if(horizontal == 0)
            {
                state = PlayerState.Idle;
                StartIdle();
            }
            else
            {
                state = PlayerState.Move;
                StartMove();
            }
        }
        else
        {
            state = PlayerState.Jump;
            StartJump();
        }
    }

    void StartIdle()
    {
        animator.Play("Idle");
    }
    void StartMove()
    {
        animator.Play("Move");
    }
    void StartJump()
    {
        animator.Play("Jump");
    }

    void UpdateAnimation()
    {
        switch (state)
        {
            case PlayerState.Idle:
                updateIdle();
                break;
            case PlayerState.Move:
                updateMove();
                break;
            case PlayerState.Jump:
                updateJump();
                break;
        }

    }
    void updateIdle()
    {
        if (horizontal != 0)
        {
            StateComplete = true;
        }
    }
    void updateMove()
    {
        if (horizontal == 0)
        {
            state = PlayerState.Idle;
            StateComplete = true;
        }
        if (groundCheck)
        {
            StateComplete = true;
        }
    }
    void updateJump()
    {
        if (groundCheck)
        {
            StateComplete = true;
        }
    }

    void Update()
    {
        UpdateAnimation();
        SelectAnimation();

        if (IsGrounded())
        {
            canJump = true;
        }
        if (IsPullabled())
        {
            canJump = true;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonDown("Jump") && IsPullabled())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f && canJump == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * gravityMultiplier * Time.deltaTime);

            //_velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }
        Flip();
    }

    //IEnumerator Falling(float duration)
    //{
    //    yield return new WaitForSeconds(duration);
    //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * gravityMultiplier * Time.deltaTime);
    //}

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        this.gameObject.SetActive(true);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsPullabled()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, pullableLayer);
    }


    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}

