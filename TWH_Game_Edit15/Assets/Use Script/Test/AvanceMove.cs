using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvanceMove : MonoBehaviour
{
    private HingeJoint2D _hj;

    public float _pushForce = 10;

    public bool attacth = false;
    public Transform attachedTo;
    private GameObject disregard;
    public bool _isGrapRope = false;


    public bool test;

    public bool _isLadder;
    public bool _isSnap;
    public bool _canSnap;
    private Animator anim;

    [Header("Reference")]
    public PlayerMovementStats MoveStats;
    [SerializeField] private Collider2D _feetColl;
    [SerializeField] private Collider2D _bodyColl;

    public Rigidbody2D _rb;

    public float HorizontalVelucity { get; private set; }
    private bool _isFacingRight;

    private RaycastHit2D _headHit;
    private RaycastHit2D _groundHit;
    private RaycastHit2D _walldHit;
    private RaycastHit2D _lastWallHit;
    private RaycastHit2D _jumpPadHit;
    private RaycastHit2D _boxHit;
    private bool _isGrounded;
    private bool _bumpHead;
    private bool _isTouchingWall;
    private bool _isTouchingjumpPad;
    private bool _isClimping;

    public float VerticalVelocity { get; private set; }
    public bool _isJumping;
    public bool _isFalling;
    private bool _isFastFalling;
    private float _fastFallingTime;
    private float _fastFallReleaseSpeed;
    private int _numberOfJumpUsed;

    private float _apexPoint;
    private float _timePastApexThreshold;
    private bool _isPastApexThreshold;

    private float _jumpBufferTimer;
    private bool _jumpReleaseDuringBuffer;

    private float _coyoteTimer;

    private bool _isWallSliding;
    private bool _isWallSlideFalling;

    private bool _useWallJumpMoveStats;
    private bool _isWallJumping;
    private float _wallJumpTime;
    private bool _isWallJumpFastFalling;
    private bool _isWallJumpFalling;
    private float _wallJumpFastFallTime;
    private float _wallJumpFastFallReleaseSpeed;

    private float _wallJumpPastBufferTime;

    private float _wallJumpApexPoint;
    private float _timePastWallJumpApexThreshold;
    private bool _isPastWallJumpApexThreshold;

    private bool _isDashing = false;

    float distance = 1;
    public LayerMask boxMask;
    public bool isPush;

    GameObject box;

    public float inputX;
    public float inputY;

    public bool MoveWithPlatform;

    public bool _canGrapRope;

    public bool _test;

    public bool _RopeFoce;

    public bool _isTouchingjumpPadLeft;
    public bool _isTouchingjumpPadRight;

    public float verticalForce = 1;
    public bool _canWallJump = false;

    //[SerializeField] private AudioClip _moveSound;
    //[SerializeField] private AudioClip _jumpSound;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _canSnap = true;
            _isClimping = true;
        }

        if (collision.CompareTag("MovingP"))
        {
            //transform.parent = collision.transform;
            MoveWithPlatform = true;
            if (_isTouchingWall)
            {
                _rb.gravityScale = 0f;
            }
            if (!_isTouchingWall)
            {
                _rb.gravityScale = 9f;
            }
        }

        if (collision.CompareTag("Pullable") && InputManager.GrabWasPressed)
        {
            collision.transform.SetParent(this.transform);
        }


        

            if (!attacth)
            {
                if (collision.gameObject.CompareTag("Rope") && _test)
                {
                    _canGrapRope = true;
                    if (attachedTo != collision.gameObject.transform.parent)
                    {
                        if (disregard == null || collision.gameObject.transform.parent.gameObject != disregard)
                        {
                            //if (InputManager.SnapWasPressed)
                            //{
                                Attach(collision.gameObject.GetComponent<Rigidbody2D>());
                                anim.SetBool("isClimp", true);
                            //}

                            //Attach(collision.gameObject.GetComponent<Rigidbody2D>());
                            //anim.SetBool("isClimp", true);
                        }
                    }
                }
            }

        if (collision.gameObject.CompareTag("JumpPadLeft"))
        {
            _isTouchingjumpPadLeft = true;
        }
        if (collision.gameObject.CompareTag("JumpPadRight"))
        {
            _isTouchingjumpPadRight = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<BoxPull>().beingPushed = false;
            isPush = false;
        }      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && isPush)
        {
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<BoxPull>().beingPushed = false;
            isPush = false;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _isLadder = false;
            _canSnap = false;
            _isClimping = false;
        }

        if (collision.CompareTag("MovingP"))
        {
            //transform.parent = null;
            MoveWithPlatform = false;
            _rb.gravityScale = 0f;
        }

        if (collision.CompareTag("Pullable"))
        {
            collision.transform.SetParent(null);
        }

        if (collision.gameObject.CompareTag("Rope"))
        {
            _canGrapRope = false;
        }

        if (collision.gameObject.CompareTag("JumpPadLeft"))
        {
            _isTouchingjumpPadLeft = false;
        }
        if (collision.gameObject.CompareTag("JumpPadRight"))
        {
            _isTouchingjumpPadRight = false;
        }

        else if (collision.gameObject.CompareTag("Obstacle") && isPush)
        {
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<BoxPull>().beingPushed = false;
            isPush = false;
            anim.SetBool("isIdleCatch", false);
        }
    }


    private void Awake()
    {
        _isFacingRight = true;
        anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _hj = GetComponent<HingeJoint2D>();
    }

    private void Start()
    {
        isPush = false;
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
    }


    private void Update()
    {
        if (InputManager.SnapWasPressed && !_canGrapRope)
        {
            _test = true;
        }
        else if(_test)
        {
            StartCoroutine(GrabDelay(0.01f));
        }
            CheckInputData();
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        if (_canSnap && InputManager.SnapWasPressed && !_isSnap)
        {
            _isLadder = true;
            _isSnap = true;
            _isClimping = true;
            _isTouchingWall = true;
        }
        else if (_isSnap && InputManager.JumpWasPressed)
        {
            _isSnap = false;
            _isLadder = false;
        }

        CountTimers();
        JumpCheck();
        LandCheck();
        WallSlideCheck();
        WallJumpCheck();
        OnGrabObject();

        //if (InputManager.JumpWasPressed && _isGrounded && !isPush)
        //{
        //    anim.SetBool("isJump", true);
        //}

        if (InputManager.JumpWasPressed && _isClimping && !_isGrounded && !_canSnap)
        {
            int dirMultiplier = 0;
            if (_isFacingRight)
            {
                dirMultiplier = 1;
            }
            else if (!_isFacingRight)
            {
                dirMultiplier = -1;
            }

            HorizontalVelucity = Mathf.Abs(MoveStats.WallJumpDirection.x) * dirMultiplier * 1.5f;
            VerticalVelocity = Mathf.Abs(MoveStats.WallJumpDirection.y) * 3;
        }      
    }

    private void FixedUpdate()
    {
        CollisionCheck();
        Jump();
        Fall();
        WallSlide();
        WallJump();
        _isDashing = false;

        JumpPadWhenTouchCheck();

        //JumpPadWhenTouchRight();
        //JumpPadWhenTouchLeft();

        if (_isGrounded)
        {
            Move(MoveStats.GroundAcceleration, MoveStats.GroundDeceleration, InputManager.Movement);
        }
        else
        {
            if (_useWallJumpMoveStats)
            {
                Move(MoveStats.WallJumpMoveAcceleration, MoveStats.WallJumpMoveDeceleration, InputManager.Movement);
            }
            else
            {
                Move(MoveStats.AirAcceleration, MoveStats.AirDeceleration, InputManager.Movement);
            }
        }

        ApplyVelocity();
    }

    public void CheckInputData()
    {
        if (InputManager._climpWIsHeld && attacth)
        {
            Slide(1);
        }

        if (InputManager._climpSIsHeld && attacth)
        {
            Slide(-1);
        }

        if (InputManager._climpDIsHeld && _RopeFoce)
        {
            if (attacth && _RopeFoce && !InputManager.JumpWasPressed)
            {
                GrabRopeTurnCheck(-1);
                _rb.AddRelativeForce(new Vector3(-1, 0, 0) * _pushForce);
            }
        }

        if (InputManager._climpAIsHeld && _RopeFoce)
        {
            if (attacth && _RopeFoce && !InputManager.JumpWasPressed)
            {
                GrabRopeTurnCheck(1);
                _rb.AddRelativeForce(new Vector3(1, 0, 0) * _pushForce);
            }
        }

        if (InputManager._climpAWasPressed || InputManager._climpDWasPressed)
        {
            _RopeFoce = true;
        }

        int dirMultiplier = 0;
        if (_isFacingRight)
        {
            dirMultiplier = 1;
        }
        else if (!_isFacingRight)
        {
            dirMultiplier = -1;
        }
        if (InputManager.JumpWasPressed && _isGrapRope)
        {
            //SoundManager.instance.PlaySound(_jumpSound);
            Detach();
            HorizontalVelucity = Mathf.Abs(MoveStats.WallJumpDirection.x) * dirMultiplier * 1.2f;
            VerticalVelocity = Mathf.Abs(MoveStats.WallJumpDirection.y) * 1.5f;
        }
    }


    public void Attach(Rigidbody2D RopeBone)
    {
        RopeBone.gameObject.GetComponent<RopeSegment>().isPlayerAttached = true;
        _hj.connectedBody = RopeBone;
        _hj.enabled = true;
        attacth = true;
        _isGrapRope = true;
        attachedTo = RopeBone.gameObject.transform.parent;
        _isTouchingWall = true;
        anim.SetBool("isClimp", true);
    }

    void Detach()
    {
        _hj.connectedBody.GetComponent<RopeSegment>().isPlayerAttached = false;
        _hj.enabled = false;
        attacth = false;
        _isGrapRope = false;
        _hj.connectedBody = null;
        _isTouchingWall = false;
        anim.SetBool("isClimp", false);
    }

    public void GrabRopeTurnCheck(int direction)
    {
        if (_isFacingRight && direction < 0)
        {
            Check(false);
        }
        else if (!_isFacingRight && direction > 0)
        {
            Check(true);
        }
    }

    public void Slide(int direction)
    {        

        RopeSegment myConnection = _hj.connectedBody.gameObject.GetComponent<RopeSegment>();
        GameObject newSeg = null;
        if (direction > 0)
        {            
            if (myConnection.connectedAbove != null)
            {
                if (myConnection.connectedAbove.gameObject.GetComponent<RopeSegment>() != null)
                {
                    newSeg = myConnection.connectedAbove;
                }
            }
        }

        else
        {
            if (myConnection.connectedBelow != null)
            {
                newSeg = myConnection.connectedBelow;
            }
        }

        if (newSeg != null)
        {
            transform.position = newSeg.transform.position;
            myConnection.isPlayerAttached = false;
            newSeg.GetComponent<RopeSegment>().isPlayerAttached = true;
            _hj.connectedBody = newSeg.GetComponent<Rigidbody2D>();
        }
    }


    private void GravutyTest(Rigidbody2D RopeBone)
    {
        attachedTo = RopeBone.gameObject.transform.parent;
    }

    private void ApplyVelocity()
    {
        if (!_isDashing)
        {
            VerticalVelocity = Mathf.Clamp(VerticalVelocity, -MoveStats.MaxFallSpeed, 50f);
        }
        if (_isGrapRope)
        {
            //VerticalVelocity = Mathf.Clamp(0, 0, 0);
            //HorizontalVelucity = Mathf.Clamp(0, 0, 0);
            GravutyTest(gameObject.GetComponent<Rigidbody2D>());
        }
        else
        {
            VerticalVelocity = Mathf.Clamp(VerticalVelocity, -50f, 50f);
        }
            _rb.velocity = new Vector2(HorizontalVelucity, VerticalVelocity);
    }

    private void OnGrabObject()
    {
        if (_isFacingRight)
        {
            distance = 1f;
            
        }
        else
        {
            distance = -1f;
        }

        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);
        if (hit.collider != null)
        {
            if (InputManager.GrabWasPressed && isPush == false)
            {
                box = hit.collider.gameObject;

                box.GetComponent<FixedJoint2D>().enabled = true;
                box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
                box.GetComponent<BoxPull>().beingPushed = true;

                anim.SetBool("isIdleCatch", true);
                isPush = true;
                _isClimping = false;
                _isTouchingWall = false;

            }
            else if (InputManager.GrabWasPressed && isPush == true)
            {
                anim.SetBool("isIdleCatch", false);
                anim.SetBool("isPush", false);
                anim.SetBool("isPull", false);
                box.GetComponent<FixedJoint2D>().enabled = false;
                box.GetComponent<BoxPull>().beingPushed = false;
                isPush = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_isFacingRight)
        {
            distance = 1f;
        }
        else
        {
            distance = -1f;
        }

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + distance * transform.localScale.x * Vector2.right);
    }

    private void Move(float acceleration, float deceleration, Vector2 moveInput)
    {
        if (/*moveInput != Vector2.zero*/ Mathf.Abs(moveInput.x) >= MoveStats.MoveThreshold && !isPush && !_isGrapRope /*&& !_isClimping*/ && !_isTouchingjumpPadRight && !_isTouchingjumpPadLeft)
        {
            TurnCheck(moveInput);
            anim.SetBool("isMove", true);
            anim.SetBool("isIdleCatch", false);
            float targetVelocity;
            targetVelocity = moveInput.x * MoveStats.MoveSpeed;

            HorizontalVelucity = Mathf.Lerp(HorizontalVelucity, targetVelocity, acceleration * Time.deltaTime);
        }

        else if (_isTouchingjumpPadLeft && Mathf.Abs(moveInput.x) >= MoveStats.MoveThreshold)
        {
            VerticalVelocity = verticalForce;
            HorizontalVelucity = (MoveStats.IntitialJumpVelocity * MoveStats.JumpPadHeightMultiplier * -verticalForce) / 5;
            //SoundManager.instance.PlaySound(_jumpSound);
        }
        else if (_isTouchingjumpPadRight && Mathf.Abs(moveInput.x) >= MoveStats.MoveThreshold)
        {
            VerticalVelocity = verticalForce;
            HorizontalVelucity = (MoveStats.IntitialJumpVelocity * MoveStats.JumpPadHeightMultiplier * verticalForce) / 5f;
            //SoundManager.instance.PlaySound(_jumpSound);
        }


        else if (_isTouchingjumpPadLeft && Mathf.Abs(moveInput.x) < MoveStats.MoveThreshold)
        {
            VerticalVelocity = verticalForce;
            HorizontalVelucity = (MoveStats.IntitialJumpVelocity * MoveStats.JumpPadHeightMultiplier * -verticalForce);
            //SoundManager.instance.PlaySound(_jumpSound);
        }
        else if (_isTouchingjumpPadRight && Mathf.Abs(moveInput.x) < MoveStats.MoveThreshold)
        {
            VerticalVelocity = verticalForce;
            HorizontalVelucity = (MoveStats.IntitialJumpVelocity * MoveStats.JumpPadHeightMultiplier * verticalForce);
            //SoundManager.instance.PlaySound(_jumpSound);
        }



        //if (InputManager._climpAIsHeld || InputManager._climpDIsHeld && !_isGrapRope && _isGrounded)
        //{
        //    SoundManager.instance.PlaySound(_moveSound);
        //}

        else if (Mathf.Abs(moveInput.x) >= MoveStats.MoveThreshold && isPush && _isGrounded && !_isGrapRope/*&& !_isClimping*/)
        {
            TurnCheck(moveInput);
            anim.SetBool("isMove", true);
            anim.SetBool("isIdleCatch", false);
            float targetVelocity;
            targetVelocity = moveInput.x * MoveStats.MoveSpeed/2;
            HorizontalVelucity = Mathf.Lerp(HorizontalVelucity, targetVelocity, acceleration/2 * Time.deltaTime);

            if (_isFacingRight)
            {
                if (InputManager._climpDIsHeld)
                {
                    anim.SetBool("isPush", false);
                    anim.SetBool("isPull", true);
                }
                
                else if (InputManager._climpAIsHeld)
                {
                    anim.SetBool("isPush", true);
                    anim.SetBool("isPull", false);
                }                               
            }
            else if (!_isFacingRight)
            {
                if (InputManager._climpDIsHeld)
                {
                    anim.SetBool("isPush", true);
                    anim.SetBool("isPull", false);
                }

                else if (InputManager._climpAIsHeld)
                {
                    anim.SetBool("isPush", false);
                    anim.SetBool("isPull", true);
                }
            }
            //else
            //{
            //    anim.SetBool("isPush", false);
            //    anim.SetBool("isPull", false);
            //    anim.SetBool("isIdleCatch", true);
            //}

        }

        else if (/*moveInput == Vector2.zero*/ Mathf.Abs(moveInput.x) < MoveStats.MoveThreshold && isPush && !_isClimping && !_isGrapRope)
        {
            HorizontalVelucity = Mathf.Lerp(HorizontalVelucity, 0f, deceleration * Time.deltaTime * 3);
            anim.SetBool("isMove", false);
            anim.SetBool("isIdleCatch", true);
        }

        else if (/*moveInput == Vector2.zero*/ Mathf.Abs(moveInput.x) < MoveStats.MoveThreshold && !_isClimping && !_isGrapRope)
        {
            HorizontalVelucity = Mathf.Lerp(HorizontalVelucity, 0f, deceleration * Time.deltaTime * 3);
            anim.SetBool("isMove", false);
        }

        if (_isTouchingWall && Mathf.Abs(moveInput.y) >= MoveStats.MoveThreshold && !_canSnap && !_isLadder && !isPush && !_isGrapRope)
        {
            anim.SetBool("isClimpMove", true);
            anim.SetBool("isOnGround", false);
            float targetVelocity;
            targetVelocity = moveInput.y * MoveStats.JumpHeight * MoveStats.ClimpMoveSpeedMultipler;
            VerticalVelocity = Mathf.Lerp(VerticalVelocity, targetVelocity, acceleration);
        }
        else
        {
            anim.SetBool("isClimpMove", false);
        }
        //if (_isClimping && !_isLadder)
        //{
        //    float targetVelocity = 0f;
        //    targetVelocity = moveInput.y * MoveStats.JumpHeight * MoveStats.ClimpMoveSpeedMultipler;
        //    VerticalVelocity = Mathf.Lerp(VerticalVelocity, targetVelocity, acceleration);
        //}

        if (_isClimping && _isLadder && !isPush && !_isGrapRope)
        {
            anim.SetBool("isClimp", true);
            float targetVelocity;
            targetVelocity = moveInput.y * MoveStats.JumpHeight * MoveStats.ClimpMoveSpeedMultipler;

            if (_isSnap)
            {
                VerticalVelocity = Mathf.Lerp(VerticalVelocity, targetVelocity, acceleration);

                HorizontalVelucity = Mathf.Lerp(/*HorizontalVelucity **/ 0f, 0, acceleration);
            }
        }
    }

    private void LandCheck()
    {
        if ((_isJumping || _isFalling || _isWallJumping || _isWallJumpFalling || _isWallSlideFalling || _isWallSliding) && _isGrounded && VerticalVelocity <= 0f)
        {
            ResetJumpValue();
            StopWallSlide();
            ResetWallJumpValue();

            _numberOfJumpUsed = 0;

            //_isJumping = false;
            //_isFalling = false;
            //_isFastFalling = false;
            //_fastFallingTime = 0f;
            //_isPastApexThreshold = false;
            //_numberOfJumpUsed = 0;
            VerticalVelocity = Physics2D.gravity.y;
        }
    }

    private void Fall()
    {
        if (!_isGrounded && !_isJumping && !_isWallSliding && !_isJumping &&!_isGrapRope)
        {
            if (!_isFalling)
            {
                _isFalling = true;
                anim.SetBool("isClimp", false);
            }
            VerticalVelocity += MoveStats.Gravity * Time.fixedDeltaTime;
        }
    }

    private void TurnCheck(Vector2 moveInput)
    {
        if (_isFacingRight && moveInput.x < 0 && !isPush)
        {
            Check(false);
        }

        else if (!_isFacingRight && moveInput.x > 0 && !isPush)
        {
            Check(true);
        }
    }

    private void IsJumpPad()
    {
        Vector2 boxCastOrigin = new(_feetColl.bounds.center.x, _feetColl.bounds.min.y);
        Vector2 boxCastSize = new(_feetColl.bounds.size.x *1.2f, MoveStats.GroundDetectionRayLength);

        _jumpPadHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, MoveStats.GroundDetectionRayLength, MoveStats.JumpPadLayerMask);
        if (_jumpPadHit.collider != null)
        {
            _isTouchingjumpPad = true;
        }
        else
        {
            _isTouchingjumpPad = false;
        }

        if (MoveStats.DebugShowIsGroundedBox)
        {
            Color rayColor;
            if (_isGrounded)
            {
                rayColor = Color.green;
            }

            else
            {
                rayColor = Color.red;
            }

            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * MoveStats.GroundDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x + boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * MoveStats.GroundDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y - MoveStats.GroundDetectionRayLength), Vector2.right * boxCastSize.x, rayColor);
        }
    }

    //private void JumpPadWhenTouchLeft()
    //{
    //    if (_isTouchingjumpPadLeft)
    //    {
    //        VerticalVelocity = verticalForce;
    //        HorizontalVelucity = MoveStats.IntitialJumpVelocity * MoveStats.JumpPadHeightMultiplier * -verticalForce;
    //        //SoundManager.instance.PlaySound(_jumpSound);
    //    }
    //}

    //private void JumpPadWhenTouchRight()
    //{
    //    if (_isTouchingjumpPadRight)
    //    {
    //        VerticalVelocity = verticalForce;
    //        HorizontalVelucity = MoveStats.IntitialJumpVelocity * MoveStats.JumpPadHeightMultiplier * verticalForce;
    //        //SoundManager.instance.PlaySound(_jumpSound);
    //    }
    //}

    private void JumpPadWhenTouchCheck()
    {
        if (_isTouchingjumpPad)
        {
            anim.SetBool("isJump", true);
            VerticalVelocity = MoveStats.IntitialJumpVelocity * MoveStats.JumpPadHeightMultiplier;
            //SoundManager.instance.PlaySound(_jumpSound);
        }
        else
        {
            anim.SetBool("isJump", false);
        }
    }

    private void IsGround()
    {
        Vector2 boxCastOrigin = new(_feetColl.bounds.center.x, _feetColl.bounds.min.y);
        Vector2 boxCastSize = new(_feetColl.bounds.size.x, MoveStats.GroundDetectionRayLength);

        _groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, MoveStats.GroundDetectionRayLength, MoveStats.GroundLayerMask);

        _boxHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, MoveStats.GroundDetectionRayLength, MoveStats.BoxLayerMask);

        if (_groundHit.collider != null)
        {
            _isGrounded = true;
            anim.SetBool("isOnGround", true);
            anim.SetBool("isJump", false);
        }

        else if (_boxHit.collider != null)
        {
            _isGrounded = true;
            anim.SetBool("isOnGround", true);
            anim.SetBool("isJump", false);
        }
        else
        {
            StartCoroutine(JumpDelay(0.02f));
            anim.SetBool("isOnGround", false);
            //_isGrounded = false;
        }

        if (MoveStats.DebugShowIsGroundedBox)
        {
            Color rayColor;
            if (_isGrounded)
            {
                rayColor = Color.green;
            }

            else
            {
                rayColor = Color.red;
            }

            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * MoveStats.GroundDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x + boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * MoveStats.GroundDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y - MoveStats.GroundDetectionRayLength), Vector2.right * boxCastSize.x, rayColor);
        }

    }

    private void JumpCheck()
    {
        //PressJump
        if (InputManager.JumpWasPressed && !isPush)
        {
            anim.SetBool("isJump", true);
            if (_isWallSlideFalling && _wallJumpPastBufferTime >= 0)
            {
                return;
            }
            else if (_isWallSliding || (_isTouchingWall && !_isGrounded))
            {
                return;
            }

            _jumpBufferTimer = MoveStats.JumpBufferTime;
            _jumpReleaseDuringBuffer = false;

            if (_isGrounded && InputManager.JumpWasPressed)
            {
                //SoundManager.instance.PlaySound(_jumpSound);
            }
        }

        //ReleaseJump
        if (InputManager.JumpWasReleased && _rb.velocity.y >= 8)
        {
            if (_jumpBufferTimer > 0)
            {
                _jumpReleaseDuringBuffer = true;
            }
            if (_isJumping && VerticalVelocity > 0f)
            {
                if (_isPastApexThreshold)
                {
                    _isPastApexThreshold = false;
                    _isFastFalling = true;
                    _fastFallingTime = MoveStats.TimeForUpwardCancel;
                    VerticalVelocity = 0f;
                }
                else
                {
                    _isFastFalling = true;
                    _fastFallReleaseSpeed = VerticalVelocity;
                }
            }
        }
        if (_jumpBufferTimer > 0 && !_isJumping && (_isGrounded || _coyoteTimer > 0f))
        {
            IntiateJump(1);
            if (_jumpReleaseDuringBuffer)
            {
                _isFastFalling = true;
                _fastFallReleaseSpeed = VerticalVelocity;
            }
        }
    }

    private void IntiateJump(int numberOfJumpUsed)
    {
        if (!_isJumping)
        {
            _isJumping = true;
        }
        ResetJumpValue();

        _jumpBufferTimer = 0f;
        _numberOfJumpUsed += numberOfJumpUsed;
        VerticalVelocity = MoveStats.IntitialJumpVelocity;
    }

    private void Jump()
    {
        if (_isJumping)
        {
            if (_rb.velocity.y >= 1)
            {
                _isJumping = false;
                _isFalling = true;
            }

            if (_bumpHead)
            {
                _isFastFalling = true;
            }

            if (VerticalVelocity >= 0f)
            {
                _apexPoint = Mathf.InverseLerp(MoveStats.IntitialJumpVelocity, 0f, VerticalVelocity);

                if (_apexPoint > MoveStats.ApexThreshold)
                {
                    if (!_isPastApexThreshold)
                    {
                        _isPastApexThreshold = true;
                        _timePastApexThreshold = 0f;
                    }

                    if (_isPastApexThreshold)
                    {
                        _timePastApexThreshold += Time.fixedDeltaTime;
                        if (_timePastApexThreshold < MoveStats.ApexHangTime)
                        {
                            VerticalVelocity = 0f;
                        }
                        else
                        {
                            VerticalVelocity = -0.01f;
                        }
                    }
                }

                else if (!_isFastFalling)
                {
                    _apexPoint += MoveStats.Gravity * Time.fixedDeltaTime;
                    if (_isPastApexThreshold)
                    {
                        _isPastApexThreshold = false;
                    }
                }
            }

            else if (!_isFastFalling)
            {
                VerticalVelocity += MoveStats.Gravity * MoveStats.GravityOnReleaseMultiplier * Time.fixedDeltaTime;
            }

            else if (VerticalVelocity < 0f)
            {
                if (!_isFalling)
                {
                    _isFalling = true;
                }
            }
        }

        if (_isFastFalling)
        {
            if (_fastFallingTime >= MoveStats.TimeForUpwardCancel)
            {
                VerticalVelocity += MoveStats.Gravity * MoveStats.GravityOnReleaseMultiplier * Time.fixedDeltaTime;
            }

            else if (_fastFallingTime < MoveStats.TimeForUpwardCancel)
            {
                VerticalVelocity = Mathf.Lerp(_fastFallReleaseSpeed, 0f, (_fastFallingTime / MoveStats.TimeForUpwardCancel));
            }
            _fastFallingTime += Time.fixedDeltaTime;
        }
    }

    private void ResetJumpValue()
    {
        _isJumping = false;
        _isFalling = false;
        _isFastFalling = false;
        _fastFallingTime = 0f;
        _isPastApexThreshold = false;
    }

    private void WallSlideCheck()
    {
        if (_isTouchingWall && !_isGrounded)
        {
            if (VerticalVelocity < 0f && !_isWallSliding)
            {
                ResetJumpValue();
                ResetWallJumpValue();

                _isWallSliding = false;
                _isWallSliding = true;

                if (MoveStats.ResetJumpOnWallSlide)
                {
                    _numberOfJumpUsed = 0;
                }
            }
        }

        else if (_isWallSliding && !_isTouchingWall && !_isGrounded && !_isWallSlideFalling)
        {
            _isWallSlideFalling = true;
            StopWallSlide();
        }
        else
        {
            StopWallSlide();
        }
    }

    private void StopWallSlide()
    {
        if (_isWallSliding)
        {
            _numberOfJumpUsed++;

            _isWallSliding = false;
        }
    }

    private void WallSlide()
    {
        if (_isWallSliding)
        {
            VerticalVelocity = Mathf.Lerp(VerticalVelocity, -MoveStats.wallSlideSpeed, MoveStats.wallSlideSpeedDecelerationSpeed * Time.fixedDeltaTime);
        }
    }

    private void ResetWallJumpValue()
    {
        _isWallSlideFalling = false;
        _useWallJumpMoveStats = false;
        _isWallJumping = false;
        _isWallJumpFastFalling = false;
        _isWallJumpFalling = false;
        _isPastApexThreshold = false;

        _wallJumpFastFallTime = 0f;
        _wallJumpTime = 0f;
    }

    private void WallJumpCheck()//12
    {
        if (ShouldApplyJumpPostBuffer())
        {
            _wallJumpPastBufferTime = MoveStats.WallJumpPostBufferTime;
        }

        if (InputManager.JumpWasReleased && !_isWallSliding && !_isTouchingWall && !_isWallJumping)
        {
            if (VerticalVelocity > 0f)
            {
                if (_isPastWallJumpApexThreshold)
                {
                    _isPastWallJumpApexThreshold = false;
                    _isWallJumpFastFalling = true;
                    _wallJumpFastFallTime = MoveStats.TimeForUpwardCancel;

                    VerticalVelocity = 0f;
                }
                else
                {
                    _isWallJumpFastFalling = true;
                    _wallJumpFastFallReleaseSpeed = VerticalVelocity;
                }
            }
        }
        if (InputManager.JumpWasPressed /*&& _isTouchingWall*/ && _canWallJump && !_isGrounded && !_isClimping /*&& _wallJumpPastBufferTime > 0f*/)
        {
            IntiateWallJump();
        }
    }

    private void IntiateWallJump()
    {
        if (!_isWallJumping)
        {
            _isWallJumping = true;
            _useWallJumpMoveStats = true;
        }

        StopWallSlide();
        ResetJumpValue();

        VerticalVelocity = MoveStats.IntitialWallJumpVelocity;

        int dirMultiplier;
        Vector2 hitpoint = _lastWallHit.collider.ClosestPoint(_bodyColl.bounds.center);

        if (hitpoint.x > transform.position.x)
        {
            dirMultiplier = -1;
        }
        else
        {
            dirMultiplier = 1;
        }

        HorizontalVelucity = Mathf.Abs(MoveStats.WallJumpDirection.x) * dirMultiplier * 2;
    }

    private void WallJump()
    {
        if (_isWallJumping)
        {
            _wallJumpTime += Time.fixedDeltaTime;
            if (_wallJumpTime > MoveStats.TimeTillJumpApex)
            {
                _useWallJumpMoveStats = false;
            }

            if (_bumpHead && !_isClimping)
            {
                _isWallJumpFastFalling = true;
                _useWallJumpMoveStats = false;
            }

            if (VerticalVelocity > 0)
            {
                _wallJumpApexPoint = Mathf.InverseLerp(MoveStats.WallJumpDirection.y, 0f, VerticalVelocity);

                if (_wallJumpApexPoint > MoveStats.ApexThreshold)
                {
                    if (!_isPastWallJumpApexThreshold)
                    {
                        _isPastWallJumpApexThreshold = true;
                        _timePastWallJumpApexThreshold = 0f;
                    }

                    if (_isPastWallJumpApexThreshold)
                    {
                        _timePastWallJumpApexThreshold += Time.fixedDeltaTime;

                        if (_timePastWallJumpApexThreshold < MoveStats.ApexHangTime)
                        {
                            VerticalVelocity = 0f;
                        }
                        else
                        {
                            VerticalVelocity = -0.01f;
                        }
                    }
                }

                else if (!_isWallJumpFastFalling)
                {
                    VerticalVelocity += MoveStats.WallJumpGravity * Time.fixedDeltaTime;

                    if (_isPastWallJumpApexThreshold)
                    {
                        _isPastWallJumpApexThreshold = false;
                    }
                }
            }

            else if (!_isWallJumpFastFalling)
            {
                VerticalVelocity += MoveStats.WallJumpGravity * Time.fixedDeltaTime;
            }
            else if (VerticalVelocity < 0f)
            {
                if (!_isWallJumpFalling)
                    _isWallJumpFalling = true;

            }
        }

        if (_isWallJumpFalling)
        {
            if (_wallJumpFastFallTime >= MoveStats.TimeForUpwardCancel)
            {
                VerticalVelocity += MoveStats.WallJumpGravity * MoveStats.WallJumpGravityOnReleaseMultipler * Time.fixedDeltaTime;
            }
            else if (_wallJumpFastFallTime < MoveStats.TimeForUpwardCancel)
            {
                VerticalVelocity = Mathf.Lerp(_wallJumpFastFallReleaseSpeed, 0f, (_wallJumpFastFallTime / MoveStats.TimeForUpwardCancel));
            }
            _wallJumpFastFallTime += Time.fixedDeltaTime;
        }
    }

    private bool ShouldApplyJumpPostBuffer()
    {
        if (_isGrounded && (_isTouchingWall || _isWallSliding))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Check(bool turnRight)
    {
        if (turnRight)
        {
            _isFacingRight = true;
            transform.Rotate(0f, 180f, 0f);
        }
        else
        {
            _isFacingRight = false;
            transform.Rotate(0f, -180f, 0f);
        }
    }

    private void BumpedHead()
    {
        Vector2 boxCastOrigin = new(_bodyColl.bounds.center.x, _bodyColl.bounds.max.y);
        Vector2 boxCastSize = new(_bodyColl.bounds.size.x, MoveStats.HeadDetectionRayLength);

        _headHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.up, MoveStats.HeadDetectionRayLength * 2, MoveStats.GroundLayerMask);
        if (_headHit.collider != null && !_isTouchingWall && !_isClimping)
        {;
            _bumpHead = true;
        }
        else
        {
            _bumpHead = false;
        }

        if (MoveStats.DebugShowHeadBumpBox)
        {
            float headWidth = MoveStats.HeadWidth;

            Color rayColor;
            if (_bumpHead)
            {
                rayColor = Color.red;
            }

            else
            {
                rayColor = Color.green;
            }

            Debug.DrawRay(new(boxCastOrigin.x - boxCastSize.x / 2 * headWidth, boxCastOrigin.y), 2 * MoveStats.HeadDetectionRayLength * Vector2.up, rayColor);
            Debug.DrawRay(new(boxCastOrigin.x + (boxCastSize.x / 2) * headWidth, boxCastOrigin.y), 2 * MoveStats.HeadDetectionRayLength * Vector2.up, rayColor);
            Debug.DrawRay(new(boxCastOrigin.x - boxCastSize.x / 2 * headWidth, boxCastOrigin.y + MoveStats.HeadDetectionRayLength * 2), boxCastSize.x * headWidth * Vector2.right, rayColor);
        }
    }

    private void IsTouchingWall()
    {
        float originEndPoint;
        if (_isFacingRight)
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

        if (_walldHit.collider != null && !_isLadder)
        {
            //anim.SetBool("isClimp", true);
            //anim.SetBool("isJump", false);
            _canWallJump = true;
           _lastWallHit = _walldHit;
            _isTouchingWall = true;
            if(!_isGrounded && _isTouchingWall)
            {
                anim.SetBool("isClimp", true);
                anim.SetBool("isJump", false);
            }
            if (_isTouchingWall && _isGrounded)
            {
                anim.SetBool("isClimp", false);
                anim.SetBool("isJump", false);
            }
        }

        else if (_isGrapRope)
        {
            anim.SetBool("isClimp", true);
        }

        else
        {
            anim.SetBool("isClimp", false);
            _isTouchingWall = false;
            StartCoroutine(WallJumpDelay(0.2f));
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

    private void CollisionCheck()
    {
        IsGround();
        BumpedHead();
        IsTouchingWall();
        IsJumpPad();
    }

    private void CountTimers()
    {
        _jumpBufferTimer -= Time.deltaTime;
        if (!_isGrounded)
        {
            _coyoteTimer -= Time.deltaTime;
        }
        else
        {
            _coyoteTimer = MoveStats.JumpCoyoteTime;
        }

        if (!ShouldApplyJumpPostBuffer())
        {
            _wallJumpPastBufferTime -= Time.deltaTime;
        }
    }

    IEnumerator JumpDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        _isGrounded = false;
    }

    IEnumerator GrabDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        _test = false;
    }

    IEnumerator WallJumpDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        _canWallJump = false;
    }
}
