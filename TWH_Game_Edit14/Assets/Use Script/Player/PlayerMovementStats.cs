using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Movement")]
public class PlayerMovementStats : ScriptableObject
{
    [Header("Movement")]
    [Range(0f, 1f)] public float MoveThreshold = 0.25f;
    [Range(1f, 50f)] public float MoveSpeed = 10f;
    [Range(0.25f, 50f)] public float GroundAcceleration = 5f;
    [Range(0.25f, 50f)] public float GroundDeceleration = 5f;
    [Range(0.25f, 50f)] public float AirAcceleration = 5f;
    [Range(0.25f, 50f)] public float AirDeceleration = 5f;
    [Range(0.25f, 50f)] public float WallJumpMoveAcceleration = 5f;
    [Range(0.25f, 50f)] public float WallJumpMoveDeceleration = 5f;

    //[Header("Run")]
    //[Range(1f, 50f)] public float MaxRunSpeed = 20;

    [Header("LayerMask")]
    public LayerMask ClimpLayerMask;
    public LayerMask BoxLayerMask;
    public LayerMask JumpPadLayerMask;
    public LayerMask GroundLayerMask;
    public LayerMask CanClimpLayerMask;

    [Header("Grounded/Collision Checks")]    
    public float GroundDetectionRayLength = 0.05f;
    public float HeadDetectionRayLength = 0.02f;
    [Range(0f, 1f)] public float HeadWidth = 0.75f;
    public float WallDetectionRayLength = 0.75f;
    [Range(0.25f, 50f)] public float WallDetectionRayHeightMultiplier = 0.9f;
    
    [Range(1f, 10f)] public float JumpPadHeightMultiplier = 2f;
    

    [Header("Climping")]
    [Range(1f, 10f)] public float ClimpMoveSpeedMultipler = 2f;

    [Header("Jump")]
    public float JumpHeight = 10f;
    [Range(1f, 1.1f)] public float JumpHeightComposationFactor = 1.054f;
    public float TimeTillJumpApex = 0.2f;
    [Range(0.01f, 5f)] public float GravityOnReleaseMultiplier = 2f;
    public float MaxFallSpeed = 26;
    [Range(1f, 5f)] public int NumberJumpAllow = 1;

    [Header("Jump Cut")]
    [Range(0.25f, 50f)] public float TimeForUpwardCancel = 0.027f;

    [Header("Jump Apex")]
    [Range(0.25f, 50f)] public float ApexThreshold = 0.97f;
    [Range(0.25f, 50f)] public float ApexHangTime = 0.35f;

    [Header("Jump Buffer")]
    [Range(0.25f, 50f)] public float JumpBufferTime = 0.125f;

    [Header("Jump Coyote Time")]
    [Range(0.25f, 50f)] public float JumpCoyoteTime = 0.1f;

    [Header("Debug")]
    public bool DebugShowIsGroundedBox;
    public bool DebugShowHeadBumpBox;
    public bool DebugShowWallHitBox;

    [Header("Jump Visualization Tool")]
    public bool ShowWalkJumpArc = false;
    //public bool ShowRunJumpArc = false;
    public bool StopOnCollision = true;
    public bool DrawRight = true;
    [Range(5f, 100f)] public int ArcResolution = 20;
    [Range(0f, 500f)] public int VisualizationStep = 90;

    [Header("ResetJumpOption")]
    public bool ResetJumpOnWallSlide = true;

    [Header("WallSlide")]
    [Min(0f)] public float wallSlideSpeed = 5f;
    [Range(0.25f, 50f)] public float wallSlideSpeedDecelerationSpeed = 50f;
    [Range(0.25f, 50f)] public float wallJumpGravityOnReleaseMultiplier = 1f;

    [Header("WallJump")]
    public Vector2 WallJumpDirection = new Vector2(-20f, 6.5f);
    [Range(0f, 1f)] public float WallJumpPostBufferTime = 0.125f;
    [Range(0.01f, 5f)] public float WallJumpGravityOnReleaseMultipler = 1f;

    public float Gravity { get; private set; }
    public float IntitialJumpVelocity { get; private set; }
    public float AdjustJumpHeight { get; private set; }

    public float WallJumpGravity { get; private set; }
    public float IntitialWallJumpVelocity { get; private set; }
    public float AdjustedWallJumpHeight { get; private set; }

    private void OnValidate()
    {
        CalculateValues();
    }

    private void OnEnable()
    {
        CalculateValues();
    }

    private void CalculateValues()
    {
        AdjustJumpHeight = JumpHeight * JumpHeightComposationFactor;
        Gravity = (2f - AdjustJumpHeight) / Mathf.Pow(TimeTillJumpApex, 2f);
        IntitialJumpVelocity = Mathf.Abs(Gravity) * TimeTillJumpApex;



        AdjustedWallJumpHeight = WallJumpDirection.y * JumpHeightComposationFactor;
        WallJumpGravity = -(2f * AdjustedWallJumpHeight) / Mathf.Pow(TimeTillJumpApex, 2f);
        IntitialWallJumpVelocity = Mathf.Abs(WallJumpGravity) * TimeTillJumpApex;
    }

}
