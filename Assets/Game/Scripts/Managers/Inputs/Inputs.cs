//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Game/Scripts/Managers/Inputs/Inputs.inputactions
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

public partial class @Inputs : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Character"",
            ""id"": ""e2823836-b2a1-47ae-ad0d-726d3d2156cd"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""ba2e151c-ab4e-4642-a8f0-34827dc57b28"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""c289c7e8-adc3-4f53-b9fa-9933b20924e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MovementJoystick"",
                    ""type"": ""Value"",
                    ""id"": ""23c7c855-6968-4ae8-a623-e1d4b40d95d2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LookAt"",
                    ""type"": ""Value"",
                    ""id"": ""c148d4d0-92b1-455b-b436-7da2adcb31d7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PickUpGun"",
                    ""type"": ""Button"",
                    ""id"": ""2cd8f447-64ce-43c1-ab92-bc5d1b1f2583"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""cd7b501a-cc60-45e9-9545-0bfe575cdebe"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""be44912d-2d05-47be-b4b9-ec024245e88a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""776064b4-856f-47dc-9676-fd7f9e01053c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""50635d12-e2ca-4483-97e3-f6bb2d805a80"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""975babeb-1bd7-414d-a61b-81cfd602218a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fbef6505-3305-44cc-857f-f939c1a5e199"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17c2aaf9-9aa3-4b13-9f4d-b1eaa9cd3161"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementJoystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1c8f7dd-4ba1-4ff0-a132-e48af67ab1fa"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookAt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""362526ed-201c-42ca-87b2-057771b48ce4"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickUpGun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Mouse"",
            ""id"": ""2be9411f-6468-488a-b9ff-d7378da3ad09"",
            ""actions"": [
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""f99d3237-6bd6-48f7-a834-0211c2c4ba7c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Automatic"",
                    ""type"": ""Button"",
                    ""id"": ""00bdc208-974c-439d-afa9-6d40e83e3679"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Semi"",
                    ""type"": ""Button"",
                    ""id"": ""68064f85-91e2-4365-8bd2-9686deeba185"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseDelta"",
                    ""type"": ""Value"",
                    ""id"": ""b3000089-7dab-4a64-80df-07ce8d78a9cf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bee8e9ad-dfb9-4e51-8ed1-0f62ef885265"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a3d598ea-bcc4-465f-9181-d5fbdfda1ba1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""AutoFire"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Automatic"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d6d09143-3e58-4d35-970d-e60f69a7af6c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Semi"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62a41d92-1d0e-4dcd-9324-0f1d85adcdbe"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Screen"",
            ""id"": ""8088aec9-42f0-4827-b402-dc4ad1fdbbf5"",
            ""actions"": [],
            ""bindings"": []
        }
    ],
    ""controlSchemes"": []
}");
        // Character
        m_Character = asset.FindActionMap("Character", throwIfNotFound: true);
        m_Character_Movement = m_Character.FindAction("Movement", throwIfNotFound: true);
        m_Character_Sprint = m_Character.FindAction("Sprint", throwIfNotFound: true);
        m_Character_MovementJoystick = m_Character.FindAction("MovementJoystick", throwIfNotFound: true);
        m_Character_LookAt = m_Character.FindAction("LookAt", throwIfNotFound: true);
        m_Character_PickUpGun = m_Character.FindAction("PickUpGun", throwIfNotFound: true);
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_MousePosition = m_Mouse.FindAction("MousePosition", throwIfNotFound: true);
        m_Mouse_Automatic = m_Mouse.FindAction("Automatic", throwIfNotFound: true);
        m_Mouse_Semi = m_Mouse.FindAction("Semi", throwIfNotFound: true);
        m_Mouse_MouseDelta = m_Mouse.FindAction("MouseDelta", throwIfNotFound: true);
        // Screen
        m_Screen = asset.FindActionMap("Screen", throwIfNotFound: true);
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

    // Character
    private readonly InputActionMap m_Character;
    private ICharacterActions m_CharacterActionsCallbackInterface;
    private readonly InputAction m_Character_Movement;
    private readonly InputAction m_Character_Sprint;
    private readonly InputAction m_Character_MovementJoystick;
    private readonly InputAction m_Character_LookAt;
    private readonly InputAction m_Character_PickUpGun;
    public struct CharacterActions
    {
        private @Inputs m_Wrapper;
        public CharacterActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Character_Movement;
        public InputAction @Sprint => m_Wrapper.m_Character_Sprint;
        public InputAction @MovementJoystick => m_Wrapper.m_Character_MovementJoystick;
        public InputAction @LookAt => m_Wrapper.m_Character_LookAt;
        public InputAction @PickUpGun => m_Wrapper.m_Character_PickUpGun;
        public InputActionMap Get() { return m_Wrapper.m_Character; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterActions instance)
        {
            if (m_Wrapper.m_CharacterActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMovement;
                @Sprint.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSprint;
                @MovementJoystick.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMovementJoystick;
                @MovementJoystick.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMovementJoystick;
                @MovementJoystick.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMovementJoystick;
                @LookAt.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnLookAt;
                @LookAt.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnLookAt;
                @LookAt.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnLookAt;
                @PickUpGun.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnPickUpGun;
                @PickUpGun.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnPickUpGun;
                @PickUpGun.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnPickUpGun;
            }
            m_Wrapper.m_CharacterActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @MovementJoystick.started += instance.OnMovementJoystick;
                @MovementJoystick.performed += instance.OnMovementJoystick;
                @MovementJoystick.canceled += instance.OnMovementJoystick;
                @LookAt.started += instance.OnLookAt;
                @LookAt.performed += instance.OnLookAt;
                @LookAt.canceled += instance.OnLookAt;
                @PickUpGun.started += instance.OnPickUpGun;
                @PickUpGun.performed += instance.OnPickUpGun;
                @PickUpGun.canceled += instance.OnPickUpGun;
            }
        }
    }
    public CharacterActions @Character => new CharacterActions(this);

    // Mouse
    private readonly InputActionMap m_Mouse;
    private IMouseActions m_MouseActionsCallbackInterface;
    private readonly InputAction m_Mouse_MousePosition;
    private readonly InputAction m_Mouse_Automatic;
    private readonly InputAction m_Mouse_Semi;
    private readonly InputAction m_Mouse_MouseDelta;
    public struct MouseActions
    {
        private @Inputs m_Wrapper;
        public MouseActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePosition => m_Wrapper.m_Mouse_MousePosition;
        public InputAction @Automatic => m_Wrapper.m_Mouse_Automatic;
        public InputAction @Semi => m_Wrapper.m_Mouse_Semi;
        public InputAction @MouseDelta => m_Wrapper.m_Mouse_MouseDelta;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                @MousePosition.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnMousePosition;
                @Automatic.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnAutomatic;
                @Automatic.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnAutomatic;
                @Automatic.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnAutomatic;
                @Semi.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnSemi;
                @Semi.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnSemi;
                @Semi.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnSemi;
                @MouseDelta.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnMouseDelta;
                @MouseDelta.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnMouseDelta;
                @MouseDelta.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnMouseDelta;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Automatic.started += instance.OnAutomatic;
                @Automatic.performed += instance.OnAutomatic;
                @Automatic.canceled += instance.OnAutomatic;
                @Semi.started += instance.OnSemi;
                @Semi.performed += instance.OnSemi;
                @Semi.canceled += instance.OnSemi;
                @MouseDelta.started += instance.OnMouseDelta;
                @MouseDelta.performed += instance.OnMouseDelta;
                @MouseDelta.canceled += instance.OnMouseDelta;
            }
        }
    }
    public MouseActions @Mouse => new MouseActions(this);

    // Screen
    private readonly InputActionMap m_Screen;
    private IScreenActions m_ScreenActionsCallbackInterface;
    public struct ScreenActions
    {
        private @Inputs m_Wrapper;
        public ScreenActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_Screen; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ScreenActions set) { return set.Get(); }
        public void SetCallbacks(IScreenActions instance)
        {
            if (m_Wrapper.m_ScreenActionsCallbackInterface != null)
            {
            }
            m_Wrapper.m_ScreenActionsCallbackInterface = instance;
            if (instance != null)
            {
            }
        }
    }
    public ScreenActions @Screen => new ScreenActions(this);
    public interface ICharacterActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnMovementJoystick(InputAction.CallbackContext context);
        void OnLookAt(InputAction.CallbackContext context);
        void OnPickUpGun(InputAction.CallbackContext context);
    }
    public interface IMouseActions
    {
        void OnMousePosition(InputAction.CallbackContext context);
        void OnAutomatic(InputAction.CallbackContext context);
        void OnSemi(InputAction.CallbackContext context);
        void OnMouseDelta(InputAction.CallbackContext context);
    }
    public interface IScreenActions
    {
    }
}
