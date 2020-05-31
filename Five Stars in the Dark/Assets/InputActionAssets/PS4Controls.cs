// GENERATED AUTOMATICALLY FROM 'Assets/InputActionAssets/PS4Controls.inputactions'

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
            ""id"": ""8f0d54da-9790-4361-a2b6-bde4bc8592db"",
            ""actions"": [
                {
                    ""name"": ""Accelerate"",
                    ""type"": ""Value"",
                    ""id"": ""d0a1ef65-8e5b-42ae-bb49-b7b4d1b96d4e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Brake"",
                    ""type"": ""Value"",
                    ""id"": ""a0fcc37c-2664-4ca8-8623-f544889e3585"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Strafe"",
                    ""type"": ""Value"",
                    ""id"": ""f8d72376-0901-40b7-b4ba-60071d616662"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b967d764-2cc2-408d-83f8-37ce236a3a80"",
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
                    ""id"": ""d2014a8e-b966-4876-84cb-57ffff6d0a86"",
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
                    ""id"": ""12768eed-9ec7-409a-9cf3-0200b4a41071"",
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
            ""id"": ""3d7c47fc-64d2-4cd9-915f-387ab554d90c"",
            ""actions"": [
                {
                    ""name"": ""Turn Left"",
                    ""type"": ""Value"",
                    ""id"": ""6adc9dbd-5f99-432d-8479-49e4602ad915"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=1,pressPoint=0.1)""
                },
                {
                    ""name"": ""Turn Right"",
                    ""type"": ""Value"",
                    ""id"": ""f02be2c8-d4ef-4e9c-a70d-d46dbf58936a"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=1,pressPoint=0.1)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e23dceb9-a008-411c-83d0-5786d2b7b38c"",
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
                    ""id"": ""29183ad6-b561-4aca-a1a0-45dd33b0d129"",
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
