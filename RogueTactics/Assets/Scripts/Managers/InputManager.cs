// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Managers/InputManager.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputManager : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputManager()
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
                },
                {
                    ""name"": ""Selection"",
                    ""type"": ""Value"",
                    ""id"": ""dfedc9bc-bfab-47db-8aa3-8d4f6c2e6d1e"",
                    ""expectedControlType"": ""Vector2"",
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
                    ""id"": ""f71b2980-f373-4cd3-9090-556f61e985d0"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0cca49a6-94fd-4f4a-805c-eeb727b77e91"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Selection"",
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
        m_Cursor = asset.FindActionMap("Cursor", throwIfNotFound: true);
        m_Cursor_Movement = m_Cursor.FindAction("Movement", throwIfNotFound: true);
        m_Cursor_Interaction = m_Cursor.FindAction("Interaction", throwIfNotFound: true);
        m_Cursor_Cancel = m_Cursor.FindAction("Cancel", throwIfNotFound: true);
        m_Cursor_Selection = m_Cursor.FindAction("Selection", throwIfNotFound: true);
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

    // Cursor
    private readonly InputActionMap m_Cursor;
    private ICursorActions m_CursorActionsCallbackInterface;
    private readonly InputAction m_Cursor_Movement;
    private readonly InputAction m_Cursor_Interaction;
    private readonly InputAction m_Cursor_Cancel;
    private readonly InputAction m_Cursor_Selection;
    public struct CursorActions
    {
        private @InputManager m_Wrapper;
        public CursorActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Cursor_Movement;
        public InputAction @Interaction => m_Wrapper.m_Cursor_Interaction;
        public InputAction @Cancel => m_Wrapper.m_Cursor_Cancel;
        public InputAction @Selection => m_Wrapper.m_Cursor_Selection;
        public InputActionMap Get() { return m_Wrapper.m_Cursor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CursorActions set) { return set.Get(); }
        public void SetCallbacks(ICursorActions instance)
        {
            if (m_Wrapper.m_CursorActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CursorActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CursorActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CursorActionsCallbackInterface.OnMovement;
                @Interaction.started -= m_Wrapper.m_CursorActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_CursorActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_CursorActionsCallbackInterface.OnInteraction;
                @Cancel.started -= m_Wrapper.m_CursorActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_CursorActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_CursorActionsCallbackInterface.OnCancel;
                @Selection.started -= m_Wrapper.m_CursorActionsCallbackInterface.OnSelection;
                @Selection.performed -= m_Wrapper.m_CursorActionsCallbackInterface.OnSelection;
                @Selection.canceled -= m_Wrapper.m_CursorActionsCallbackInterface.OnSelection;
            }
            m_Wrapper.m_CursorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Selection.started += instance.OnSelection;
                @Selection.performed += instance.OnSelection;
                @Selection.canceled += instance.OnSelection;
            }
        }
    }
    public CursorActions @Cursor => new CursorActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }
    public interface ICursorActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnSelection(InputAction.CallbackContext context);
    }
}
