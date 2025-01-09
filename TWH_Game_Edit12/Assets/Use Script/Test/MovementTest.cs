using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementTest : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float speed;
    public float inputX;
    public float inputY;
    public SpriteRenderer spriteRenderer;
    public float JumpForce;
    public bool isJumping;

    public float distance = 1f;
    public bool isMoveWithBox;
    public bool _isGrounded;

    public float HorizontalVelucity { get; private set; }
    public float VerticalVelocity { get; private set; }

    public PlayerMovementStats MoveStats;

    private bool _isTouchingWall;
    private RaycastHit2D _walldHit;
    [SerializeField] private Collider2D _bodyColl;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        JumpCheck();
        IsTouchingWall();
    }

    private void FixedUpdate()
    {
        if (isMoveWithBox)
        {
            //VerticalVelocity = Mathf.Clamp(VerticalVelocity, -MoveStats.MaxFallSpeed, 50f);
            if (_isGrounded)
            {              
                playerRb.velocity = new Vector2(inputX * speed * 1.2f, -speed/2f);
            }

            if (!_isGrounded && _isTouchingWall)
            {
                playerRb.velocity = new Vector2(inputX * speed, inputY * speed);
            }
        }
    }

    private void IsTouchingWall()
    {
        float originEndPoint;
        if (inputX == 1)
        {
            originEndPoint = _bodyColl.bounds.center.x;
        }
        else
        {
            originEndPoint = _bodyColl.bounds.center.x;
        }

        float adjustedHeight = _bodyColl.bounds.size.y * MoveStats.WallDetectionRayHeightMultiplier;

        Vector2 boxCastOrigin = new(originEndPoint, _bodyColl.bounds.center.y);
        Vector2 boxCastSize = new(MoveStats.WallDetectionRayLength, adjustedHeight);

        _walldHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, transform.right, MoveStats.WallDetectionRayLength, MoveStats.GroundLayerMask);

        if (_walldHit.collider != null)
        {
            _isTouchingWall = true;
            _isGrounded = false;
        }

        else
        {
            _isTouchingWall = false;
            _isGrounded = true;
        }

        if (MoveStats.DebugShowWallHitBox)
        {
            Color rayColor;
            if (_isTouchingWall)
            {
                rayColor = Color.white;
            }
            else
            {
                rayColor = Color.black;
            }

            Vector2 boxBottomLeft = new(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y - boxCastSize.y / 2);
            Vector2 boxBottomRight = new(boxCastOrigin.x + boxCastSize.x / 2, boxCastOrigin.y - boxCastSize.y / 2);
            Vector2 boxTopLeft = new(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y + boxCastSize.y / 2);
            Vector2 boxTopRight = new(boxCastOrigin.x + boxCastSize.x / 2, boxCastOrigin.y + boxCastSize.y / 2);

            Debug.DrawLine(boxBottomLeft, boxBottomRight, rayColor);
            Debug.DrawLine(boxBottomRight, boxTopRight, rayColor);
            Debug.DrawLine(boxTopRight, boxTopLeft, rayColor);
            Debug.DrawLine(boxTopLeft, boxBottomLeft, rayColor);
        }
    }

    private void JumpCheck()
    {
        //PressJump
        if (InputManager.JumpWasPressed)
        {
            isMoveWithBox = false;
            VerticalVelocity = MoveStats.IntitialJumpVelocity;
        }

    }

    //IEnumerator GravityActive(float duration)
    //{
    //    yield return new WaitForSeconds(duration);
    //    isMoveWithBox = true;
    //}

    //IEnumerator Active(float duration)
    //{
    //    yield return new WaitForSeconds(duration);
    //    isMoveWithBox = false;
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingP"))
        {
            isMoveWithBox = true;
            //StartCoroutine(GravityActive(0.1f));
        }
        else
        {
            isMoveWithBox = false;         
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingP"))
        {
            isMoveWithBox = false;
            //StartCoroutine(Active(0.1f));
        }
    }
}