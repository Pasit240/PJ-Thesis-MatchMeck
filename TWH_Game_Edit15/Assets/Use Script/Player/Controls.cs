//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Use Script/Player/Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""4f3101ea-affd-4f63-a046-e30a95d045f6"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e620c816-baaf-41e8-805d-8435c2d69c0d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""PassThrough"",
                    ""id"": ""212910c8-26cb-4b87-9d51-7c8a66170c98"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Snap"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c7e36d10-2dfc-491f-b99b-e72679c2336d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Grap"",
                    ""type"": ""Button"",
                    ""id"": ""f31ff5c3-4eaa-4424-b33b-0a5fae43dd17"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ClimpMoveUp"",
                    ""type"": ""PassThrough"",
                    ""id"": ""543a749e-e4ea-423a-87c7-8c3d1ada2f9a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ClimpMoveDown"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1fb03426-9c08-4007-b6fd-d0526f7af169"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ClimpMoveRight"",
                    ""type"": ""PassThrough"",
                    ""id"": ""15943837-bc05-49fd-9948-4114b98ef614"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ClimpMoveLeft"",
                    ""type"": ""PassThrough"",
                    ""id"": ""da165a9c-ba52-47b8-a64f-5a96eab6dd39"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenDebug"",
                    ""type"": ""Button"",
                    ""id"": ""70c59e08-a464-4d2c-9bfd-fc59421aafc6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Arrow"",
                    ""id"": ""eaaa378d-99f2-4aa3-98f0-9f176b6540e0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""bca00dd1-e114-442b-a510-b12ce7580c07"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1a63f086-c5f4-4b6e-ab56-ad2dd9113e17"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1aea5389-b894-4949-a2f9-e5ab46fe2b53"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""27bd09a8-34b6-4c04-a31f-3f9349153d22"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""350ea752-73e1-4942-a9e5-08df55954c80"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""53d6b051-03b2-4f03-abdc-3273c8ba96bb"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""004187ce-28de-41e0-9fb5-18c92ece6620"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""de763b7b-fb49-4269-8107-5ea7a35fa4f9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8e3d04a5-bda1-42bd-8990-0e7a22563d2a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e1b94845-6065-4780-958e-50075904cda5"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ceb454b4-12ac-4926-b15f-aed40e01d589"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""908ea499-e2fd-4a4a-a9f7-a3aa51c772c5"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5f177f8-0288-4ed7-b438-ac2fa4900508"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eac3d2b3-8ffb-49c6-a32a-09a734582e99"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClimpMoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""848dc86f-0ef4-4e47-8037-3a2285e7343f"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClimpMoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""442e084b-56d9-428b-ac07-9eb1d2c72eec"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClimpMoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16e6c170-f681-4abf-a91d-a190c53d2f60"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClimpMoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3f6a804-aee4-4e2e-9bbd-fba768bd4536"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClimpMoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ec142f7-6256-4753-9128-e8819baf7265"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClimpMoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""10593ab8-de0d-4d41-b28d-a8a1b0bc2ee6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClimpMoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e24b1c6-072a-485b-8601-1c0257c23f8c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClimpMoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ede63aeb-1526-4722-9428-f71ea734d72c"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenDebug"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Snap = m_Player.FindAction("Snap", throwIfNotFound: true);
        m_Player_Grap = m_Player.FindAction("Grap", throwIfNotFound: true);
        m_Player_ClimpMoveUp = m_Player.FindAction("ClimpMoveUp", throwIfNotFound: true);
        m_Player_ClimpMoveDown = m_Player.FindAction("ClimpMoveDown", throwIfNotFound: true);
        m_Player_ClimpMoveRight = m_Player.FindAction("ClimpMoveRight", throwIfNotFound: true);
        m_Player_ClimpMoveLeft = m_Player.FindAction("ClimpMoveLeft", throwIfNotFound: true);
        m_Player_OpenDebug = m_Player.FindAction("OpenDebug", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Snap;
    private readonly InputAction m_Player_Grap;
    private readonly InputAction m_Player_ClimpMoveUp;
    private readonly InputAction m_Player_ClimpMoveDown;
    private readonly InputAction m_Player_ClimpMoveRight;
    private readonly InputAction m_Player_ClimpMoveLeft;
    private readonly InputAction m_Player_OpenDebug;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Snap => m_Wrapper.m_Player_Snap;
        public InputAction @Grap => m_Wrapper.m_Player_Grap;
        public InputAction @ClimpMoveUp => m_Wrapper.m_Player_ClimpMoveUp;
        public InputAction @ClimpMoveDown => m_Wrapper.m_Player_ClimpMoveDown;
        public InputAction @ClimpMoveRight => m_Wrapper.m_Player_ClimpMoveRight;
        public InputAction @ClimpMoveLeft => m_Wrapper.m_Player_ClimpMoveLeft;
        public InputAction @OpenDebug => m_Wrapper.m_Player_OpenDebug;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Snap.started += instance.OnSnap;
            @Snap.performed += instance.OnSnap;
            @Snap.canceled += instance.OnSnap;
            @Grap.started += instance.OnGrap;
            @Grap.performed += instance.OnGrap;
            @Grap.canceled += instance.OnGrap;
            @ClimpMoveUp.started += instance.OnClimpMoveUp;
            @ClimpMoveUp.performed += instance.OnClimpMoveUp;
            @ClimpMoveUp.canceled += instance.OnClimpMoveUp;
            @ClimpMoveDown.started += instance.OnClimpMoveDown;
            @ClimpMoveDown.performed += instance.OnClimpMoveDown;
            @ClimpMoveDown.canceled += instance.OnClimpMoveDown;
            @ClimpMoveRight.started += instance.OnClimpMoveRight;
            @ClimpMoveRight.performed += instance.OnClimpMoveRight;
            @ClimpMoveRight.canceled += instance.OnClimpMoveRight;
            @ClimpMoveLeft.started += instance.OnClimpMoveLeft;
            @ClimpMoveLeft.performed += instance.OnClimpMoveLeft;
            @ClimpMoveLeft.canceled += instance.OnClimpMoveLeft;
            @OpenDebug.started += instance.OnOpenDebug;
            @OpenDebug.performed += instance.OnOpenDebug;
            @OpenDebug.canceled += instance.OnOpenDebug;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Snap.started -= instance.OnSnap;
            @Snap.performed -= instance.OnSnap;
            @Snap.canceled -= instance.OnSnap;
            @Grap.started -= instance.OnGrap;
            @Grap.performed -= instance.OnGrap;
            @Grap.canceled -= instance.OnGrap;
            @ClimpMoveUp.started -= instance.OnClimpMoveUp;
            @ClimpMoveUp.performed -= instance.OnClimpMoveUp;
            @ClimpMoveUp.canceled -= instance.OnClimpMoveUp;
            @ClimpMoveDown.started -= instance.OnClimpMoveDown;
            @ClimpMoveDown.performed -= instance.OnClimpMoveDown;
            @ClimpMoveDown.canceled -= instance.OnClimpMoveDown;
            @ClimpMoveRight.started -= instance.OnClimpMoveRight;
            @ClimpMoveRight.performed -= instance.OnClimpMoveRight;
            @ClimpMoveRight.canceled -= instance.OnClimpMoveRight;
            @ClimpMoveLeft.started -= instance.OnClimpMoveLeft;
            @ClimpMoveLeft.performed -= instance.OnClimpMoveLeft;
            @ClimpMoveLeft.canceled -= instance.OnClimpMoveLeft;
            @OpenDebug.started -= instance.OnOpenDebug;
            @OpenDebug.performed -= instance.OnOpenDebug;
            @OpenDebug.canceled -= instance.OnOpenDebug;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSnap(InputAction.CallbackContext context);
        void OnGrap(InputAction.CallbackContext context);
        void OnClimpMoveUp(InputAction.CallbackContext context);
        void OnClimpMoveDown(InputAction.CallbackContext context);
        void OnClimpMoveRight(InputAction.CallbackContext context);
        void OnClimpMoveLeft(InputAction.CallbackContext context);
        void OnOpenDebug(InputAction.CallbackContext context);
    }
}
