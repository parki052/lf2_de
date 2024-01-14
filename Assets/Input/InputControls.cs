//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Input/InputControls.inputactions
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

public partial class @InputControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""c72c0cf0-3049-469e-a87c-b9ec505bfff7"",
            ""actions"": [
                {
                    ""name"": ""attack"",
                    ""type"": ""Button"",
                    ""id"": ""3b5a4617-8c45-45ab-bd25-10afc299fb46"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""jump"",
                    ""type"": ""Button"",
                    ""id"": ""dc07b1e5-c2cd-4268-9860-b172417ffeb8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""defend"",
                    ""type"": ""Button"",
                    ""id"": ""4392577e-09e3-4579-8dac-d237d177fed4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""movement"",
                    ""type"": ""Value"",
                    ""id"": ""f0de7f38-f01e-45bb-aa84-65aea825c2c2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""direction_up"",
                    ""type"": ""Button"",
                    ""id"": ""20565852-4c29-48eb-9f1b-1b95efb5221d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""direction_left"",
                    ""type"": ""Button"",
                    ""id"": ""a5bb048f-d290-4080-870f-b994bd50d791"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""direction_down"",
                    ""type"": ""Button"",
                    ""id"": ""12fc2fb0-591d-4422-99b9-fa7139de9533"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""direction_right"",
                    ""type"": ""Button"",
                    ""id"": ""2c88d512-224d-4152-baad-ebec5a5e4db9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7ef62e3d-a99b-4e90-9592-cb4d53e6f813"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""614c8fb6-ffc1-4953-9297-5f75c0f13276"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5e7b3fa-5bad-4cec-9e65-3f105d07c7b8"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""defend"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a6167592-62ae-4d95-a6d8-d68af41602b9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f8c3077b-5583-48b0-9b36-62d67fccffb5"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c3f00c65-a87f-4066-b986-e0d05ce7143f"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bd50c77c-57a5-4a18-a303-089fd347cf38"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3db98e72-bbfd-45f5-b038-33d42bfee03b"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""21d7a5fd-41f3-42c6-b160-34d6e84bd40d"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""direction_up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""91e74c8d-23f4-4be8-ba1a-67ffd251d881"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""direction_left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5d9572c-303e-4e8a-a6c6-e93bcad228aa"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""direction_down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2483ce7-bd89-4f93-868c-824710ff5406"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""direction_right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_attack = m_Player.FindAction("attack", throwIfNotFound: true);
        m_Player_jump = m_Player.FindAction("jump", throwIfNotFound: true);
        m_Player_defend = m_Player.FindAction("defend", throwIfNotFound: true);
        m_Player_movement = m_Player.FindAction("movement", throwIfNotFound: true);
        m_Player_direction_up = m_Player.FindAction("direction_up", throwIfNotFound: true);
        m_Player_direction_left = m_Player.FindAction("direction_left", throwIfNotFound: true);
        m_Player_direction_down = m_Player.FindAction("direction_down", throwIfNotFound: true);
        m_Player_direction_right = m_Player.FindAction("direction_right", throwIfNotFound: true);
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
    private readonly InputAction m_Player_attack;
    private readonly InputAction m_Player_jump;
    private readonly InputAction m_Player_defend;
    private readonly InputAction m_Player_movement;
    private readonly InputAction m_Player_direction_up;
    private readonly InputAction m_Player_direction_left;
    private readonly InputAction m_Player_direction_down;
    private readonly InputAction m_Player_direction_right;
    public struct PlayerActions
    {
        private @InputControls m_Wrapper;
        public PlayerActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @attack => m_Wrapper.m_Player_attack;
        public InputAction @jump => m_Wrapper.m_Player_jump;
        public InputAction @defend => m_Wrapper.m_Player_defend;
        public InputAction @movement => m_Wrapper.m_Player_movement;
        public InputAction @direction_up => m_Wrapper.m_Player_direction_up;
        public InputAction @direction_left => m_Wrapper.m_Player_direction_left;
        public InputAction @direction_down => m_Wrapper.m_Player_direction_down;
        public InputAction @direction_right => m_Wrapper.m_Player_direction_right;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @attack.started += instance.OnAttack;
            @attack.performed += instance.OnAttack;
            @attack.canceled += instance.OnAttack;
            @jump.started += instance.OnJump;
            @jump.performed += instance.OnJump;
            @jump.canceled += instance.OnJump;
            @defend.started += instance.OnDefend;
            @defend.performed += instance.OnDefend;
            @defend.canceled += instance.OnDefend;
            @movement.started += instance.OnMovement;
            @movement.performed += instance.OnMovement;
            @movement.canceled += instance.OnMovement;
            @direction_up.started += instance.OnDirection_up;
            @direction_up.performed += instance.OnDirection_up;
            @direction_up.canceled += instance.OnDirection_up;
            @direction_left.started += instance.OnDirection_left;
            @direction_left.performed += instance.OnDirection_left;
            @direction_left.canceled += instance.OnDirection_left;
            @direction_down.started += instance.OnDirection_down;
            @direction_down.performed += instance.OnDirection_down;
            @direction_down.canceled += instance.OnDirection_down;
            @direction_right.started += instance.OnDirection_right;
            @direction_right.performed += instance.OnDirection_right;
            @direction_right.canceled += instance.OnDirection_right;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @attack.started -= instance.OnAttack;
            @attack.performed -= instance.OnAttack;
            @attack.canceled -= instance.OnAttack;
            @jump.started -= instance.OnJump;
            @jump.performed -= instance.OnJump;
            @jump.canceled -= instance.OnJump;
            @defend.started -= instance.OnDefend;
            @defend.performed -= instance.OnDefend;
            @defend.canceled -= instance.OnDefend;
            @movement.started -= instance.OnMovement;
            @movement.performed -= instance.OnMovement;
            @movement.canceled -= instance.OnMovement;
            @direction_up.started -= instance.OnDirection_up;
            @direction_up.performed -= instance.OnDirection_up;
            @direction_up.canceled -= instance.OnDirection_up;
            @direction_left.started -= instance.OnDirection_left;
            @direction_left.performed -= instance.OnDirection_left;
            @direction_left.canceled -= instance.OnDirection_left;
            @direction_down.started -= instance.OnDirection_down;
            @direction_down.performed -= instance.OnDirection_down;
            @direction_down.canceled -= instance.OnDirection_down;
            @direction_right.started -= instance.OnDirection_right;
            @direction_right.performed -= instance.OnDirection_right;
            @direction_right.canceled -= instance.OnDirection_right;
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
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnAttack(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDefend(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnDirection_up(InputAction.CallbackContext context);
        void OnDirection_left(InputAction.CallbackContext context);
        void OnDirection_down(InputAction.CallbackContext context);
        void OnDirection_right(InputAction.CallbackContext context);
    }
}
