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

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _snapAction;
    //private InputAction _runAction;

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();

        _snapAction = PlayerInput.actions["Snap"];
        _moveAction = PlayerInput.actions["Move"];
        _jumpAction = PlayerInput.actions["Jump"];
        //_runAction = PlayerInput.actions["Run"];
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();

        JumpWasPressed = _jumpAction.WasPressedThisFrame();
        JumpIsHeld = _jumpAction.IsPressed();
        JumpWasReleased =_jumpAction.WasReleasedThisFrame();

        SnapWasPressed = _snapAction.WasPressedThisFrame();

        //RunIsHeld = _runAction.IsPressed();
    }

}
