// GENERATED AUTOMATICALLY FROM 'Assets/PS4Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PS4Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PS4Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PS4Controls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""214d1fe7-3d1b-41b1-ad35-3fc9f0166f5d"",
            ""actions"": [
                {
                    ""name"": ""Accelerate"",
                    ""type"": ""Value"",
                    ""id"": ""3a347a41-ebb3-4825-abae-67285979a7ba"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Brake"",
                    ""type"": ""Value"",
                    ""id"": ""1ad7417e-abbb-4462-b949-70e0509d4f2c"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Strafe"",
                    ""type"": ""Value"",
                    ""id"": ""f5591991-fa34-48aa-9de7-266a27b5aca2"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8a33d485-713a-4ee0-b012-93fb363640a3"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c99c37c3-9f12-43a6-a849-1f1e245296a5"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e437901-7051-4cb6-972d-5a85a684f134"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""QuickTurns"",
            ""id"": ""5e95b0b4-128e-4083-8d44-97c2a7501c08"",
            ""actions"": [
                {
                    ""name"": ""Turn Left"",
                    ""type"": ""Value"",
                    ""id"": ""2c08916c-36ce-4c5d-b08c-1654729794d0"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=1,pressPoint=0.1)""
                },
                {
                    ""name"": ""Turn Right"",
                    ""type"": ""Value"",
                    ""id"": ""4af16860-3899-43a6-9061-0320d22d2282"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=1,pressPoint=0.1)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bde5239f-e763-4031-bae0-b868f6d6fa2f"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4eb56b82-8268-4b5f-b41a-4dd41cfe5b4d"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Accelerate = m_Gameplay.FindAction("Accelerate", throwIfNotFound: true);
        m_Gameplay_Brake = m_Gameplay.FindAction("Brake", throwIfNotFound: true);
        m_Gameplay_Strafe = m_Gameplay.FindAction("Strafe", throwIfNotFound: true);
        // QuickTurns
        m_QuickTurns = asset.FindActionMap("QuickTurns", throwIfNotFound: true);
        m_QuickTurns_TurnLeft = m_QuickTurns.FindAction("Turn Left", throwIfNotFound: true);
        m_QuickTurns_TurnRight = m_QuickTurns.FindAction("Turn Right", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Accelerate;
    private readonly InputAction m_Gameplay_Brake;
    private readonly InputAction m_Gameplay_Strafe;
    public struct GameplayActions
    {
        private @PS4Controls m_Wrapper;
        public GameplayActions(@PS4Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Accelerate => m_Wrapper.m_Gameplay_Accelerate;
        public InputAction @Brake => m_Wrapper.m_Gameplay_Brake;
        public InputAction @Strafe => m_Wrapper.m_Gameplay_Strafe;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Accelerate.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAccelerate;
                @Accelerate.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAccelerate;
                @Accelerate.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAccelerate;
                @Brake.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBrake;
                @Brake.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBrake;
                @Brake.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBrake;
                @Strafe.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStrafe;
                @Strafe.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStrafe;
                @Strafe.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStrafe;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Accelerate.started += instance.OnAccelerate;
                @Accelerate.performed += instance.OnAccelerate;
                @Accelerate.canceled += instance.OnAccelerate;
                @Brake.started += instance.OnBrake;
                @Brake.performed += instance.OnBrake;
                @Brake.canceled += instance.OnBrake;
                @Strafe.started += instance.OnStrafe;
                @Strafe.performed += instance.OnStrafe;
                @Strafe.canceled += instance.OnStrafe;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // QuickTurns
    private readonly InputActionMap m_QuickTurns;
    private IQuickTurnsActions m_QuickTurnsActionsCallbackInterface;
    private readonly InputAction m_QuickTurns_TurnLeft;
    private readonly InputAction m_QuickTurns_TurnRight;
    public struct QuickTurnsActions
    {
        private @PS4Controls m_Wrapper;
        public QuickTurnsActions(@PS4Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TurnLeft => m_Wrapper.m_QuickTurns_TurnLeft;
        public InputAction @TurnRight => m_Wrapper.m_QuickTurns_TurnRight;
        public InputActionMap Get() { return m_Wrapper.m_QuickTurns; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(QuickTurnsActions set) { return set.Get(); }
        public void SetCallbacks(IQuickTurnsActions instance)
        {
            if (m_Wrapper.m_QuickTurnsActionsCallbackInterface != null)
            {
                @TurnLeft.started -= m_Wrapper.m_QuickTurnsActionsCallbackInterface.OnTurnLeft;
                @TurnLeft.performed -= m_Wrapper.m_QuickTurnsActionsCallbackInterface.OnTurnLeft;
                @TurnLeft.canceled -= m_Wrapper.m_QuickTurnsActionsCallbackInterface.OnTurnLeft;
                @TurnRight.started -= m_Wrapper.m_QuickTurnsActionsCallbackInterface.OnTurnRight;
                @TurnRight.performed -= m_Wrapper.m_QuickTurnsActionsCallbackInterface.OnTurnRight;
                @TurnRight.canceled -= m_Wrapper.m_QuickTurnsActionsCallbackInterface.OnTurnRight;
            }
            m_Wrapper.m_QuickTurnsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TurnLeft.started += instance.OnTurnLeft;
                @TurnLeft.performed += instance.OnTurnLeft;
                @TurnLeft.canceled += instance.OnTurnLeft;
                @TurnRight.started += instance.OnTurnRight;
                @TurnRight.performed += instance.OnTurnRight;
                @TurnRight.canceled += instance.OnTurnRight;
            }
        }
    }
    public QuickTurnsActions @QuickTurns => new QuickTurnsActions(this);
    public interface IGameplayActions
    {
        void OnAccelerate(InputAction.CallbackContext context);
        void OnBrake(InputAction.CallbackContext context);
        void OnStrafe(InputAction.CallbackContext context);
    }
    public interface IQuickTurnsActions
    {
        void OnTurnLeft(InputAction.CallbackContext context);
        void OnTurnRight(InputAction.CallbackContext context);
    }
}
