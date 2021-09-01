// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputManager.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using Object = UnityEngine.Object;

public class InputManager : IInputActionCollection, IDisposable
{
    // Cursor
    private readonly InputActionMap m_Cursor;
    private readonly InputAction m_Cursor_Cancel;
    private readonly InputAction m_Cursor_Interaction;
    private readonly InputAction m_Cursor_Movement;
    private ICursorActions m_CursorActionsCallbackInterface;
    private int m_GamepadSchemeIndex = -1;
    private int m_KeyboardSchemeIndex = -1;
    private int m_MouseSchemeIndex = -1;

    public InputManager()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputManager"",
    ""maps"": [
        {
            ""name"": ""Cursor"",
            ""id"": ""69937725-6c8b-4c23-aa2a-6ac067b59d96"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""d7428ee0-ff05-4a70-8319-9eeb8dfdb0e5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""2e5411d9-a5ef-4422-ba4b-835000dd08b2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""287e60a0-5844-49c5-9da5-78940435ce3c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c452f24f-c7bb-407b-a424-7a78296db057"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3dea32ac-17c6-4ace-a488-2d3db3730110"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f71b2980-f373-4cd3-9090-556f61e985d0"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Cancel"",
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
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Cursor
        m_Cursor = asset.FindActionMap("Cursor", true);
        m_Cursor_Movement = m_Cursor.FindAction("Movement", true);
        m_Cursor_Interaction = m_Cursor.FindAction("Interaction", true);
        m_Cursor_Cancel = m_Cursor.FindAction("Cancel", true);
    }

    public InputActionAsset asset { get; }
    public CursorActions Cursor => new CursorActions(this);

    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }

    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }

    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }

    public void Dispose()
    {
        Object.Destroy(asset);
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

    public struct CursorActions
    {
        private readonly InputManager m_Wrapper;

        public CursorActions(InputManager wrapper)
        {
            m_Wrapper = wrapper;
        }

        public InputAction Movement => m_Wrapper.m_Cursor_Movement;
        public InputAction Interaction => m_Wrapper.m_Cursor_Interaction;
        public InputAction Cancel => m_Wrapper.m_Cursor_Cancel;

        public InputActionMap Get()
        {
            return m_Wrapper.m_Cursor;
        }

        public void Enable()
        {
            Get().Enable();
        }

        public void Disable()
        {
            Get().Disable();
        }

        public bool enabled => Get().enabled;

        public static implicit operator InputActionMap(CursorActions set)
        {
            return set.Get();
        }

        public void SetCallbacks(ICursorActions instance)
        {
            if (m_Wrapper.m_CursorActionsCallbackInterface != null)
            {
                Movement.started -= m_Wrapper.m_CursorActionsCallbackInterface.OnMovement;
                Movement.performed -= m_Wrapper.m_CursorActionsCallbackInterface.OnMovement;
                Movement.canceled -= m_Wrapper.m_CursorActionsCallbackInterface.OnMovement;
                Interaction.started -= m_Wrapper.m_CursorActionsCallbackInterface.OnInteraction;
                Interaction.performed -= m_Wrapper.m_CursorActionsCallbackInterface.OnInteraction;
                Interaction.canceled -= m_Wrapper.m_CursorActionsCallbackInterface.OnInteraction;
                Cancel.started -= m_Wrapper.m_CursorActionsCallbackInterface.OnCancel;
                Cancel.performed -= m_Wrapper.m_CursorActionsCallbackInterface.OnCancel;
                Cancel.canceled -= m_Wrapper.m_CursorActionsCallbackInterface.OnCancel;
            }

            m_Wrapper.m_CursorActionsCallbackInterface = instance;
            if (instance != null)
            {
                Movement.started += instance.OnMovement;
                Movement.performed += instance.OnMovement;
                Movement.canceled += instance.OnMovement;
                Interaction.started += instance.OnInteraction;
                Interaction.performed += instance.OnInteraction;
                Interaction.canceled += instance.OnInteraction;
                Cancel.started += instance.OnCancel;
                Cancel.performed += instance.OnCancel;
                Cancel.canceled += instance.OnCancel;
            }
        }
    }

    public interface ICursorActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
}