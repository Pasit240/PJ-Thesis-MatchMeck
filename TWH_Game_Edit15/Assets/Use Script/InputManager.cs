using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerInput PlayerInput;

    public static Vector2 Movement;
    public static bool JumpWasPressed;
    public static bool JumpIsHeld;
    public static bool JumpWasReleased;
    public static bool SnapWasPressed;
    //public static bool RunIsHeld;
    public static bool GrabWasPressed;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _snapAction;
    //private InputAction _runAction;
    private InputAction _grapAction;

    private InputAction _climpW;
    private InputAction _climpS;
    private InputAction _climpA;
    private InputAction _climpD;

    public static bool _climpWIsHeld;
    public static bool _climpSIsHeld;
    public static bool _climpAIsHeld;
    public static bool _climpDIsHeld;

    public static bool _climpAWasPressed;
    public static bool _climpDWasPressed;

    private InputAction _openDebug;
    public static bool _isDebugOpen;

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();

        _snapAction = PlayerInput.actions["Snap"];
        _moveAction = PlayerInput.actions["Move"];
        _jumpAction = PlayerInput.actions["Jump"];
        //_runAction = PlayerInput.actions["Run"];
        _grapAction = PlayerInput.actions["Grap"];

        _climpW = PlayerInput.actions["ClimpMoveUp"];
        _climpS = PlayerInput.actions["ClimpMoveDown"];
        _climpA = PlayerInput.actions["ClimpMoveLeft"];
        _climpD = PlayerInput.actions["ClimpMoveRight"];


        _openDebug = PlayerInput.actions["OpenDebug"];
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();

        JumpWasPressed = _jumpAction.WasPressedThisFrame();
        JumpIsHeld = _jumpAction.IsPressed();
        JumpWasReleased = _jumpAction.WasReleasedThisFrame();

        SnapWasPressed = _snapAction.WasPressedThisFrame();

        GrabWasPressed = _grapAction.WasPressedThisFrame();

        _climpWIsHeld = _climpW.WasPressedThisFrame();
        _climpSIsHeld = _climpS.WasPressedThisFrame();
        _climpAIsHeld = _climpA.IsPressed();
        _climpDIsHeld = _climpD.IsPressed();

        _climpAWasPressed = _climpA.WasPressedThisFrame();
        _climpDWasPressed = _climpD.WasPressedThisFrame();


        _isDebugOpen = _openDebug.WasPressedThisFrame();


        //RunIsHeld = _runAction.IsPressed();
    }
}
