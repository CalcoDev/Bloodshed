// GENERATED AUTOMATICALLY FROM 'Assets/Data/Player/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Player
{
    public class @PlayerInput : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""53b155a6-5f3f-47f0-a5d2-37d25a0cfc87"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""ded56722-1793-4ea7-b342-a1d6ca99db34"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""e5c42596-4abf-4672-b86b-541994ccfa08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""17597aac-95c3-4b9c-8aef-c7e4002306f7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PassiveAbility"",
                    ""type"": ""Button"",
                    ""id"": ""381cc390-38a5-4efb-94cd-4ef5ec831c10"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryAbility"",
                    ""type"": ""Button"",
                    ""id"": ""7d22697e-c2a2-4dd9-a399-07ea73446dc7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryAbility"",
                    ""type"": ""Button"",
                    ""id"": ""8ab98ecf-07a9-4d00-ba51-96e9fae07a8b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UltimateAbility"",
                    ""type"": ""Button"",
                    ""id"": ""c3acc679-df5c-436b-8588-ec21f74c90f8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryAttack"",
                    ""type"": ""Button"",
                    ""id"": ""4afc2d36-c970-46d1-b0d1-a67bcc3ec816"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryAttack"",
                    ""type"": ""Button"",
                    ""id"": ""f058023f-2d07-440d-aeda-39db11f19b49"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Horizontal"",
                    ""id"": ""dcc56f62-b921-4bcd-b431-5ed16ea89a13"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""6ef6e254-31d9-4007-8b3b-b88ba71ab039"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""97c6329a-4cad-44bc-b556-7efbdbf8f2f1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f56e1307-cd0a-47cb-bada-b7179400fcaa"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b74b4ab-1bd0-4b68-bb0b-e5ec70c5eb3a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default Keyboard"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bed79d81-f6af-490e-9c08-366cf62c2eb5"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default Keyboard"",
                    ""action"": ""PassiveAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""54ae335f-5103-41bd-a5ea-98b6059f3192"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default Keyboard"",
                    ""action"": ""PrimaryAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0accb63-2cde-4464-85d0-7a8c7b0ac205"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default Keyboard"",
                    ""action"": ""SecondaryAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""45db3cd9-0220-4397-b96f-3db9da1aa199"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default Keyboard"",
                    ""action"": ""UltimateAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f2d6472-4cb5-40df-9c1e-5f2c5e1e6b76"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default Keyboard"",
                    ""action"": ""PrimaryAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffadb819-fe72-4cb1-af56-b9e48a8b37b1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default Keyboard"",
                    ""action"": ""SecondaryAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Default Keyboard"",
            ""bindingGroup"": ""Default Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
            m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
            m_Player_Crouch = m_Player.FindAction("Crouch", throwIfNotFound: true);
            m_Player_PassiveAbility = m_Player.FindAction("PassiveAbility", throwIfNotFound: true);
            m_Player_PrimaryAbility = m_Player.FindAction("PrimaryAbility", throwIfNotFound: true);
            m_Player_SecondaryAbility = m_Player.FindAction("SecondaryAbility", throwIfNotFound: true);
            m_Player_UltimateAbility = m_Player.FindAction("UltimateAbility", throwIfNotFound: true);
            m_Player_PrimaryAttack = m_Player.FindAction("PrimaryAttack", throwIfNotFound: true);
            m_Player_SecondaryAttack = m_Player.FindAction("SecondaryAttack", throwIfNotFound: true);
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

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_Movement;
        private readonly InputAction m_Player_Jump;
        private readonly InputAction m_Player_Crouch;
        private readonly InputAction m_Player_PassiveAbility;
        private readonly InputAction m_Player_PrimaryAbility;
        private readonly InputAction m_Player_SecondaryAbility;
        private readonly InputAction m_Player_UltimateAbility;
        private readonly InputAction m_Player_PrimaryAttack;
        private readonly InputAction m_Player_SecondaryAttack;
        public struct PlayerActions
        {
            private @PlayerInput m_Wrapper;
            public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_Player_Movement;
            public InputAction @Jump => m_Wrapper.m_Player_Jump;
            public InputAction @Crouch => m_Wrapper.m_Player_Crouch;
            public InputAction @PassiveAbility => m_Wrapper.m_Player_PassiveAbility;
            public InputAction @PrimaryAbility => m_Wrapper.m_Player_PrimaryAbility;
            public InputAction @SecondaryAbility => m_Wrapper.m_Player_SecondaryAbility;
            public InputAction @UltimateAbility => m_Wrapper.m_Player_UltimateAbility;
            public InputAction @PrimaryAttack => m_Wrapper.m_Player_PrimaryAttack;
            public InputAction @SecondaryAttack => m_Wrapper.m_Player_SecondaryAttack;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                    @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                    @Crouch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                    @Crouch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                    @Crouch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                    @PassiveAbility.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPassiveAbility;
                    @PassiveAbility.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPassiveAbility;
                    @PassiveAbility.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPassiveAbility;
                    @PrimaryAbility.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryAbility;
                    @PrimaryAbility.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryAbility;
                    @PrimaryAbility.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryAbility;
                    @SecondaryAbility.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryAbility;
                    @SecondaryAbility.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryAbility;
                    @SecondaryAbility.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryAbility;
                    @UltimateAbility.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUltimateAbility;
                    @UltimateAbility.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUltimateAbility;
                    @UltimateAbility.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUltimateAbility;
                    @PrimaryAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryAttack;
                    @PrimaryAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryAttack;
                    @PrimaryAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryAttack;
                    @SecondaryAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryAttack;
                    @SecondaryAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryAttack;
                    @SecondaryAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryAttack;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Crouch.started += instance.OnCrouch;
                    @Crouch.performed += instance.OnCrouch;
                    @Crouch.canceled += instance.OnCrouch;
                    @PassiveAbility.started += instance.OnPassiveAbility;
                    @PassiveAbility.performed += instance.OnPassiveAbility;
                    @PassiveAbility.canceled += instance.OnPassiveAbility;
                    @PrimaryAbility.started += instance.OnPrimaryAbility;
                    @PrimaryAbility.performed += instance.OnPrimaryAbility;
                    @PrimaryAbility.canceled += instance.OnPrimaryAbility;
                    @SecondaryAbility.started += instance.OnSecondaryAbility;
                    @SecondaryAbility.performed += instance.OnSecondaryAbility;
                    @SecondaryAbility.canceled += instance.OnSecondaryAbility;
                    @UltimateAbility.started += instance.OnUltimateAbility;
                    @UltimateAbility.performed += instance.OnUltimateAbility;
                    @UltimateAbility.canceled += instance.OnUltimateAbility;
                    @PrimaryAttack.started += instance.OnPrimaryAttack;
                    @PrimaryAttack.performed += instance.OnPrimaryAttack;
                    @PrimaryAttack.canceled += instance.OnPrimaryAttack;
                    @SecondaryAttack.started += instance.OnSecondaryAttack;
                    @SecondaryAttack.performed += instance.OnSecondaryAttack;
                    @SecondaryAttack.canceled += instance.OnSecondaryAttack;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);
        private int m_DefaultKeyboardSchemeIndex = -1;
        public InputControlScheme DefaultKeyboardScheme
        {
            get
            {
                if (m_DefaultKeyboardSchemeIndex == -1) m_DefaultKeyboardSchemeIndex = asset.FindControlSchemeIndex("Default Keyboard");
                return asset.controlSchemes[m_DefaultKeyboardSchemeIndex];
            }
        }
        public interface IPlayerActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnCrouch(InputAction.CallbackContext context);
            void OnPassiveAbility(InputAction.CallbackContext context);
            void OnPrimaryAbility(InputAction.CallbackContext context);
            void OnSecondaryAbility(InputAction.CallbackContext context);
            void OnUltimateAbility(InputAction.CallbackContext context);
            void OnPrimaryAttack(InputAction.CallbackContext context);
            void OnSecondaryAttack(InputAction.CallbackContext context);
        }
    }
}
