using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public delegate void SnapEnable(Transform transform);
    //public SnapEnable snapEnable;

    public bool test;

    public bool _isLadder;
    public bool _isSnap;
    public bool _canSnap;

    private Animator anim;

    [Header("Reference")]
    public PlayerMovementStats MoveStats;
    [SerializeField] private Collider2D _feetColl;
    [SerializeField] private Collider2D _bodyColl;

    Rigidbody2D _rb;

    public float HorizontalVelucity { get; private set; }
    private bool _isFacingRight;

    private RaycastHit2D _headHit;
    private RaycastHit2D _groundHit;
    private RaycastHit2D _walldHit;
    private RaycastHit2D _lastWallHit;
    private RaycastHit2D _jumpPadHit;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _canSnap = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _isLadder = false;
            _canSnap = false;
        }
    }


    private void Awake()
    {
        _isFacingRight = true;

        anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_canSnap && InputManager.SnapWasPressed && !_isSnap)
        {
            _isLadder = true;
            _isSnap = true;
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            test = true;
        }
        else
        {
            test = false;
        }

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    snapEnable(this.transform);
        //}

        if (InputManager.JumpWasPressed && _isGrounded)
        {
            anim.SetBool("isJump", true);
        }

        if (InputManager.JumpWasPressed && _isClimping && !_isGrounded)
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

    private void ApplyVelocity()
    {
        if (!_isDashing)
        {
            VerticalVelocity = Mathf.Clamp(VerticalVelocity, -MoveStats.MaxFallSpeed, 50f);
        }
        else
        {
            VerticalVelocity = Mathf.Clamp(VerticalVelocity, -50f, 50f);
        }
        _rb.velocity = new Vector2(HorizontalVelucity, VerticalVelocity);
    }

    private void OnDrawGizmos()
    {
        if (MoveStats.ShowWalkJumpArc)
        {
            DrawJumpArc(MoveStats.MoveSpeed, Color.white);
        }
    }

    private void Move(float acceleration, float deceleration, Vector2 moveInput)
    {
        if (/*moveInput != Vector2.zero*/ Mathf.Abs(moveInput.x) >= MoveStats.MoveThreshold)
        {
            TurnCheck(moveInput);

            anim.SetBool("isMove", true);

            float targetVelocity = 0f;
            //if (PlayerControl.RunIsHeld)
            //{
            //    targetVelocity = moveInput.x * MoveStats.MaxRunSpeed;
            //}
            //else
            //{
            targetVelocity = moveInput.x * MoveStats.MoveSpeed;
            //}

            HorizontalVelucity = Mathf.Lerp(HorizontalVelucity, targetVelocity, acceleration * Time.deltaTime);
        }

        else if (/*moveInput == Vector2.zero*/ Mathf.Abs(moveInput.x) < MoveStats.MoveThreshold)
        {
            HorizontalVelucity = Mathf.Lerp(HorizontalVelucity, 0f, deceleration * Time.deltaTime);
            anim.SetBool("isMove", false);
        }

        if (_isTouchingWall && Mathf.Abs(moveInput.y) >= MoveStats.MoveThreshold)
        {
            float targetVelocity = 0f;
            targetVelocity = moveInput.y * MoveStats.JumpHeight * MoveStats.ClimpMoveSpeedMultipler;
            VerticalVelocity = Mathf.Lerp(VerticalVelocity, targetVelocity, acceleration * Time.deltaTime);
        }
        if (_isClimping && !_isLadder)
        {
            float targetVelocity = 0f;
            targetVelocity = moveInput.y * MoveStats.JumpHeight * MoveStats.ClimpMoveSpeedMultipler;
            VerticalVelocity = Mathf.Lerp(VerticalVelocity, targetVelocity, acceleration * Time.deltaTime);
        }

        if (_isClimping && _isLadder)
        {            
                float targetVelocity = 0f;
                targetVelocity = moveInput.y * MoveStats.JumpHeight * MoveStats.ClimpMoveSpeedMultipler;
                VerticalVelocity = Mathf.Lerp(VerticalVelocity, targetVelocity, acceleration * Time.deltaTime);

                HorizontalVelucity = Mathf.Lerp(/*HorizontalVelucity **/ 0f, 0, acceleration * Time.deltaTime);            
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            _isClimping = true;
            _isTouchingWall = true;
            anim.SetBool("isClimp", true);
        }
        else
        {
            _isClimping = false;
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
        if (!_isGrounded && !_isJumping && !_isWallSliding && !_isJumping)
        {
            if (!_isFalling)
            {
                _isFalling = true;
            }
            VerticalVelocity += MoveStats.Gravity * Time.fixedDeltaTime;
        }
    }

    private void TurnCheck(Vector2 moveInput)
    {
        if (_isFacingRight && moveInput.x < 0)
        {
            Check(false);
        }

        else if (!_isFacingRight && moveInput.x > 0)
        {
            Check(true);
        }
    }

    private void IsJumpPad()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _feetColl.bounds.min.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x, MoveStats.GroundDetectionRayLength);

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

    private void JumpPadWhenTouchCheck()
    {
        if (_isTouchingjumpPad)
        {
            anim.SetBool("isJump", true);
            VerticalVelocity = MoveStats.IntitialJumpVelocity * MoveStats.JumpPadHeightMultiplier;
        }
        else
        {
            anim.SetBool("isJump", false);
        }
    }

    private void IsGround()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _feetColl.bounds.min.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x, MoveStats.GroundDetectionRayLength);

        _groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, MoveStats.GroundDetectionRayLength, MoveStats.GroundLayerMask);
        if (_groundHit.collider != null)
        {
            anim.SetBool("isOnGround", true);
            anim.SetBool("isJump", false);
            _isGrounded = true;
        }
        else
        {
            anim.SetBool("isOnGround", false);
            _isGrounded = false;
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
        if (InputManager.JumpWasPressed)
        {
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

        ////DoubleJump
        //else if (_jumpBufferTimer > 0f && _isJumping && _numberOfJumpUsed < MoveStats.NumberJumpAllow)
        //{
        //    _isFalling = false;
        //    IntiateJump(1);
        //}

        ////AirJump
        //else if (_jumpBufferTimer > 0f && _isJumping && _numberOfJumpUsed < MoveStats.NumberJumpAllow - 1)
        //{
        //    IntiateJump(2);
        //    _isFalling = false;
        //}
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

    private void WallJumpCheck()
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
        if (InputManager.JumpWasPressed && _isTouchingWall && !_isGrounded && !_isClimping /*&& _wallJumpPastBufferTime > 0f*/)
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

        int dirMultiplier = 0;
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

            if (_bumpHead)
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
        Vector2 boxCastOrigin = new Vector2(_bodyColl.bounds.center.x, _bodyColl.bounds.max.y);
        Vector2 boxCastSize = new Vector2(_bodyColl.bounds.size.x, MoveStats.HeadDetectionRayLength);

        _headHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.up, MoveStats.HeadDetectionRayLength * 2, MoveStats.GroundLayerMask);
        if (_headHit.collider != null && !_isTouchingWall)
        {
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

            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2 * headWidth, boxCastOrigin.y), Vector2.up * MoveStats.HeadDetectionRayLength * 2, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x + (boxCastSize.x / 2) * headWidth, boxCastOrigin.y), Vector2.up * MoveStats.HeadDetectionRayLength * 2, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2 * headWidth, boxCastOrigin.y + MoveStats.HeadDetectionRayLength * 2), Vector2.right * boxCastSize.x * headWidth, rayColor);
        }
    }

    private void IsTouchingWall()
    {
        float originEndPoint = 0f;
        if (_isFacingRight)
        {
            originEndPoint = _bodyColl.bounds.center.x;
        }
        else
        {
            originEndPoint = _bodyColl.bounds.center.x;
        }

        float adjustedHeight = _bodyColl.bounds.size.y * MoveStats.WallDetectionRayHeightMultiplier;

        Vector2 boxCastOrigin = new Vector2(originEndPoint, _bodyColl.bounds.center.y);
        Vector2 boxCastSize = new Vector2(MoveStats.WallDetectionRayLength, adjustedHeight);

        _walldHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, transform.right, MoveStats.WallDetectionRayLength, MoveStats.GroundLayerMask);

        if (_walldHit.collider != null && !_isLadder)
        {
            anim.SetBool("isClimp", true);
            anim.SetBool("isJump", false);
            _lastWallHit = _walldHit;
            _isTouchingWall = true;
        }

        else
        {
            anim.SetBool("isClimp", false);
            _isTouchingWall = false;
        }

        if (MoveStats.DebugShowWallHitBox)
        {
            Color rayColor;
            if (_isTouchingWall)
            {
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
            }

            Vector2 boxBottomLeft = new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y - boxCastSize.y / 2);
            Vector2 boxBottomRight = new Vector2(boxCastOrigin.x + boxCastSize.x / 2, boxCastOrigin.y - boxCastSize.y / 2);
            Vector2 boxTopLeft = new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y + boxCastSize.y / 2);
            Vector2 boxTopRight = new Vector2(boxCastOrigin.x + boxCastSize.x / 2, boxCastOrigin.y + boxCastSize.y / 2);

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

    private void DrawJumpArc(float moveSpeed, Color gizmoColor)
    {
        Vector2 startPosition = new Vector2(_feetColl.bounds.center.x, _feetColl.bounds.min.y);
        Vector2 previousPosition = startPosition;
        float speed = 0f;
        if (MoveStats.DrawRight)
        {
            speed = moveSpeed;
        }
        else speed = -moveSpeed;
        Vector2 velocity = new Vector2(speed, MoveStats.IntitialJumpVelocity);

        Gizmos.color = gizmoColor;

        float timeStep = 2 * MoveStats.TimeTillJumpApex / MoveStats.ArcResolution;
        //float totalTime = (MoveStats.TimeTillJumpApex) + MoveStats.ApexHangTime;

        for (int i = 0; i < MoveStats.VisualizationStep; i++)
        {
            float simulationTime = i * timeStep;
            Vector2 displacement;
            Vector2 drawPoint;

            if (simulationTime < MoveStats.TimeTillJumpApex)
            {
                displacement = velocity * simulationTime + 0.5f * new Vector2(0, MoveStats.Gravity) * simulationTime * simulationTime;
            }
            else if (simulationTime < MoveStats.TimeTillJumpApex + MoveStats.ApexHangTime)
            {
                float apexTime = simulationTime - MoveStats.TimeTillJumpApex;
                displacement = velocity * MoveStats.TimeTillJumpApex + 0.5f * new Vector2(0, MoveStats.Gravity) * MoveStats.TimeTillJumpApex * MoveStats.TimeTillJumpApex;
                displacement += new Vector2(speed, 0) * apexTime;
            }
            else
            {
                float desendTime = simulationTime - (MoveStats.TimeTillJumpApex + MoveStats.ApexHangTime);
                displacement = velocity * MoveStats.TimeTillJumpApex + 0.5f * new Vector2(0, MoveStats.Gravity) * MoveStats.TimeTillJumpApex * MoveStats.TimeTillJumpApex;
                displacement += new Vector2(speed, 0) * MoveStats.ApexHangTime;
                displacement += new Vector2(speed, 0) * desendTime + 0.5f * new Vector2(0, MoveStats.Gravity) * desendTime * desendTime;
            }
            drawPoint = startPosition + displacement;

            if (MoveStats.StopOnCollision)
            {
                RaycastHit2D hit = Physics2D.Raycast(previousPosition, drawPoint - previousPosition, Vector2.Distance(previousPosition, drawPoint), MoveStats.GroundLayerMask);
                if (hit.collider != null)
                {
                    Gizmos.DrawLine(previousPosition, hit.point);
                    break;
                }
            }

            Gizmos.DrawLine(previousPosition, drawPoint);
            previousPosition = drawPoint;
        }


    }









}