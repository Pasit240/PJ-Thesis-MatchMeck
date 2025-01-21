using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public static UserInput instance;

    [HideInInspector] public PlayerControl playercontrol;
    [HideInInspector] public Vector2 moveInput;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            //Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

        playercontrol = new PlayerControl();

        playercontrol.Movement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        playercontrol.Enable();
    }

    private void OnDisable()
    {
        playercontrol.Disable();
    }
}
