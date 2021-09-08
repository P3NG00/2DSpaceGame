// GENERATED AUTOMATICALLY FROM 'Assets/Input/Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Input : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""Keyboard + Mouse"",
            ""id"": ""33063dee-766e-4c81-9c8f-1ed363d508f0"",
            ""actions"": [
                {
                    ""name"": ""Add Force"",
                    ""type"": ""Button"",
                    ""id"": ""164377d6-e88c-4ffc-b50c-62619ab31fca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""265737f6-0605-42e0-afbd-7994e55cfdab"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""bf94d510-7c98-43c0-a0a4-21f7c345335e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ba536da7-2c2e-480f-8eac-cba17878704f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Add Force"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""e8090e10-37fe-47a9-8474-4887e9052079"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""73763565-2c3c-4974-a3aa-ce5521e84c4a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""1207add5-029f-4371-8cac-186b7af070a6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""503de463-2ced-4d34-b879-51adb98e4825"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard + Mouse"",
            ""bindingGroup"": ""Keyboard + Mouse"",
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
        // Keyboard + Mouse
        m_KeyboardMouse = asset.FindActionMap("Keyboard + Mouse", throwIfNotFound: true);
        m_KeyboardMouse_AddForce = m_KeyboardMouse.FindAction("Add Force", throwIfNotFound: true);
        m_KeyboardMouse_Rotate = m_KeyboardMouse.FindAction("Rotate", throwIfNotFound: true);
        m_KeyboardMouse_Fire = m_KeyboardMouse.FindAction("Fire", throwIfNotFound: true);
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

    // Keyboard + Mouse
    private readonly InputActionMap m_KeyboardMouse;
    private IKeyboardMouseActions m_KeyboardMouseActionsCallbackInterface;
    private readonly InputAction m_KeyboardMouse_AddForce;
    private readonly InputAction m_KeyboardMouse_Rotate;
    private readonly InputAction m_KeyboardMouse_Fire;
    public struct KeyboardMouseActions
    {
        private @Input m_Wrapper;
        public KeyboardMouseActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @AddForce => m_Wrapper.m_KeyboardMouse_AddForce;
        public InputAction @Rotate => m_Wrapper.m_KeyboardMouse_Rotate;
        public InputAction @Fire => m_Wrapper.m_KeyboardMouse_Fire;
        public InputActionMap Get() { return m_Wrapper.m_KeyboardMouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardMouseActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardMouseActions instance)
        {
            if (m_Wrapper.m_KeyboardMouseActionsCallbackInterface != null)
            {
                @AddForce.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnAddForce;
                @AddForce.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnAddForce;
                @AddForce.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnAddForce;
                @Rotate.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnRotate;
                @Fire.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnFire;
            }
            m_Wrapper.m_KeyboardMouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AddForce.started += instance.OnAddForce;
                @AddForce.performed += instance.OnAddForce;
                @AddForce.canceled += instance.OnAddForce;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
            }
        }
    }
    public KeyboardMouseActions @KeyboardMouse => new KeyboardMouseActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard + Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IKeyboardMouseActions
    {
        void OnAddForce(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
    }
}
