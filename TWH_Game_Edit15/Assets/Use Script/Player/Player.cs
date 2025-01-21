using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movespeed = 10;    

    [Header("Jump")]
    [SerializeField] private float jumpforce = 18;
    [SerializeField] private float jumptime = 0.1f;

    [Header("TurnCheck")]
    [SerializeField] private GameObject Lleg;
    [SerializeField] private GameObject Rleg;
    [HideInInspector] public bool isFacingRight;

    [Header("GroumdCheck")]
    [SerializeField] private float extraHeight = 0.2f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPullable;
    //[SerializeField] private LayerMask whatIsWall;
    //[SerializeField] private LayerMask whatIsLadder;
    public bool canjump = true;

    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    private float moveInput;

    private bool isJumping;
    private bool isFalling;
    private float jumpTimeCounter;
    private RaycastHit2D groundHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        StartDilectionCheck();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        moveInput = UserInput.instance.moveInput.x;

        if (moveInput > 0 || moveInput < 0)
        {
            anim.SetBool("isMove", true);
            TurnCheck();
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        rb.velocity = new Vector2 (moveInput * movespeed, rb.velocity.y);
    }

    private void Jump()
    {
        if (UserInput.instance.playercontrol.Jumping.Jump.WasPressedThisFrame() && IsGrounded() && canjump == true)
        {
            isJumping = true;
            jumpTimeCounter = jumptime;
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);

            anim.SetTrigger("isJump");
        }

        if (UserInput.instance.playercontrol.Jumping.Jump.WasPressedThisFrame() && IsPullable() && canjump == true)
        {
            isJumping = true;
            jumpTimeCounter = jumptime;
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);

            anim.SetTrigger("isJump");
        }

        if (UserInput.instance.playercontrol.Jumping.Jump.IsPressed())
        {
            if (jumpTimeCounter > 0 && isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                jumpTimeCounter -= Time.deltaTime;

                anim.SetTrigger("isJump");
            }

            else if (jumpTimeCounter == 0)
            {
                isJumping = false;
                isFalling = true;
            }

            else
            {
                isJumping = false;
            }
        }

        if (UserInput.instance.playercontrol.Jumping.Jump.WasReleasedThisFrame())
        {
            isJumping = false;
            isFalling = true;

            anim.SetTrigger("isLand");
        }

        if (!isJumping && CheckForLand())
        {
            anim.SetTrigger("isLand");
        }

        DrawGroundCheck();
    }

    private bool IsGrounded()
    {
        groundHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, extraHeight, whatIsGround);

        if (groundHit.collider != null)
        {
            canjump = true;
            return true;
        }

        else
        {
            canjump = false;
            return false;
        }
    }

    private bool IsPullable()
    {
        groundHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, extraHeight, whatIsPullable);

        if (groundHit.collider != null)
        {
            canjump = true;
            return true;
        }

        else
        {
            canjump = false;
            return true;
        }
    }

    private bool CheckForLand()
    {
        if (isFalling)
        {
            if (IsGrounded())
            {
                isFalling = false;

                return true;
            }

            else 
            { 
                return false; 
            }
        }

        else
        {
            return false;
        }
    }

    private void DrawGroundCheck ()
    {
        Color rayColor;
        if (IsGrounded())
        {
            rayColor = Color.green;
        }

        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(coll.bounds.center + new Vector3(coll.bounds.extents.x, 0), Vector2.down * (coll.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(coll.bounds.center - new Vector3(coll.bounds.extents.x, 0), Vector2.down * (coll.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(coll.bounds.center - new Vector3(coll.bounds.extents.x, coll.bounds.extents.y + extraHeight), Vector2.right * (coll.bounds.extents.x * 2), rayColor);
    }

    private void StartDilectionCheck()
    {
        if (Rleg.transform.position.x > Lleg.transform.position.x)
        {
            isFacingRight = true;
        }

        else
        {
            isFacingRight = false;
        }
    }

    private void TurnCheck()
    {
        if(UserInput.instance.moveInput.x > 0 && !isFacingRight)
        {
            Turn();
        }

        else if (UserInput.instance.moveInput.x < 0 && isFacingRight)
        {
            Turn();
        }
    }

    private void Turn()
    {
        if(isFacingRight)
        {
            Vector3 rotator = new(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        }

        else
        {
            Vector3 rotator = new(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        }
    }
}
