// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputSystem.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputSystem : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputSystem"",
    ""maps"": [
        {
            ""name"": ""Input"",
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
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""bf94d510-7c98-43c0-a0a4-21f7c345335e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory Toggle"",
                    ""type"": ""Button"",
                    ""id"": ""00c7eef2-64bf-4137-a39d-cf3fd0b40bec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""f20c52fe-a657-4c9d-9981-09a9a161724a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Direction"",
                    ""type"": ""Value"",
                    ""id"": ""3f9f38c2-fa50-4ece-bd98-6c87238ffef6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""b54c0df7-699a-42aa-bb06-7b434d99daa1"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slow Down"",
                    ""type"": ""Button"",
                    ""id"": ""b44f9430-4cb6-4553-9224-0dc4c95188db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hotbar 1"",
                    ""type"": ""Button"",
                    ""id"": ""e539be18-f555-4101-b6b5-42dd5b1c6d30"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hotbar 2"",
                    ""type"": ""Button"",
                    ""id"": ""6d8119a5-b884-4794-bdf3-bc20058b7b1e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hotbar 3"",
                    ""type"": ""Button"",
                    ""id"": ""27ec11c0-7fce-4cbc-99c9-1b17b36527d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hotbar 4"",
                    ""type"": ""Button"",
                    ""id"": ""e34a4a2c-e14a-4035-adec-91d9f07b6cce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hotbar 5"",
                    ""type"": ""Button"",
                    ""id"": ""e4f00b57-052a-4326-b836-a019814b5854"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Exit"",
                    ""type"": ""Button"",
                    ""id"": ""962cd95c-12ca-4046-be24-364f014dec43"",
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
                    ""name"": """",
                    ""id"": ""077fd388-4d2c-4f67-9003-8b45316911ed"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Add Force"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
                },
                {
                    ""name"": """",
                    ""id"": ""ab60fc60-17a6-4fa3-82ca-30ffc93a29f5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24b0065e-4b6c-4eeb-95da-57362e4a5db9"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""398dad8c-3a97-4588-af62-2df56d4bfdc8"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Inventory Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""723f9c93-a974-4e8a-be70-6a2e5e07c1b4"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Inventory Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3a80431-a895-4efd-8ee0-a836cbfd8b5c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c64fe491-e101-45cc-9f10-98d0c1015f97"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4563df86-f8d4-4bb6-999f-c98e98328fdc"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06d8e7bc-fc22-4de5-bcb3-dd6cd42a26b9"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7221955b-19cd-4ba2-8414-5b972b9898e5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Slow Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e9f732d-e458-4007-bcc2-97811912b201"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Slow Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a3243263-f80b-4616-a4cf-a5b6422b798c"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Hotbar 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06ed8e1c-cddc-4a61-aba1-51c52bb47981"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Hotbar 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfa37fbc-1de0-4f9f-b42a-8a8123c06b52"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Hotbar 3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27a43fc4-ec1d-4f40-987c-aa5ef1edc232"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Hotbar 4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""336683f7-0cfe-43bc-8187-c6d342dd3876"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Hotbar 5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""3210c6d1-23a1-440a-a2d4-07498d2a30d2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""39a89dd7-7e08-409f-80e6-ee2058f860d3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d95c8829-0fd8-4535-8635-04a63ec3ef44"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Controller"",
                    ""id"": ""aeb65b9c-7258-436a-b853-5cc87add7103"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""78a0230b-20a2-4ae7-82bb-6052027a972a"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""2a845a8d-845f-4673-b1d7-1c5b13577173"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""302db301-833e-43ce-ac4b-14c393937e4b"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Exit"",
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
        },
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Input
        m_Input = asset.FindActionMap("Input", throwIfNotFound: true);
        m_Input_AddForce = m_Input.FindAction("Add Force", throwIfNotFound: true);
        m_Input_Fire = m_Input.FindAction("Fire", throwIfNotFound: true);
        m_Input_InventoryToggle = m_Input.FindAction("Inventory Toggle", throwIfNotFound: true);
        m_Input_Menu = m_Input.FindAction("Menu", throwIfNotFound: true);
        m_Input_Direction = m_Input.FindAction("Direction", throwIfNotFound: true);
        m_Input_Rotate = m_Input.FindAction("Rotate", throwIfNotFound: true);
        m_Input_SlowDown = m_Input.FindAction("Slow Down", throwIfNotFound: true);
        m_Input_Hotbar1 = m_Input.FindAction("Hotbar 1", throwIfNotFound: true);
        m_Input_Hotbar2 = m_Input.FindAction("Hotbar 2", throwIfNotFound: true);
        m_Input_Hotbar3 = m_Input.FindAction("Hotbar 3", throwIfNotFound: true);
        m_Input_Hotbar4 = m_Input.FindAction("Hotbar 4", throwIfNotFound: true);
        m_Input_Hotbar5 = m_Input.FindAction("Hotbar 5", throwIfNotFound: true);
        m_Input_Exit = m_Input.FindAction("Exit", throwIfNotFound: true);
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

    // Input
    private readonly InputActionMap m_Input;
    private IInputActions m_InputActionsCallbackInterface;
    private readonly InputAction m_Input_AddForce;
    private readonly InputAction m_Input_Fire;
    private readonly InputAction m_Input_InventoryToggle;
    private readonly InputAction m_Input_Menu;
    private readonly InputAction m_Input_Direction;
    private readonly InputAction m_Input_Rotate;
    private readonly InputAction m_Input_SlowDown;
    private readonly InputAction m_Input_Hotbar1;
    private readonly InputAction m_Input_Hotbar2;
    private readonly InputAction m_Input_Hotbar3;
    private readonly InputAction m_Input_Hotbar4;
    private readonly InputAction m_Input_Hotbar5;
    private readonly InputAction m_Input_Exit;
    public struct InputActions
    {
        private @InputSystem m_Wrapper;
        public InputActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @AddForce => m_Wrapper.m_Input_AddForce;
        public InputAction @Fire => m_Wrapper.m_Input_Fire;
        public InputAction @InventoryToggle => m_Wrapper.m_Input_InventoryToggle;
        public InputAction @Menu => m_Wrapper.m_Input_Menu;
        public InputAction @Direction => m_Wrapper.m_Input_Direction;
        public InputAction @Rotate => m_Wrapper.m_Input_Rotate;
        public InputAction @SlowDown => m_Wrapper.m_Input_SlowDown;
        public InputAction @Hotbar1 => m_Wrapper.m_Input_Hotbar1;
        public InputAction @Hotbar2 => m_Wrapper.m_Input_Hotbar2;
        public InputAction @Hotbar3 => m_Wrapper.m_Input_Hotbar3;
        public InputAction @Hotbar4 => m_Wrapper.m_Input_Hotbar4;
        public InputAction @Hotbar5 => m_Wrapper.m_Input_Hotbar5;
        public InputAction @Exit => m_Wrapper.m_Input_Exit;
        public InputActionMap Get() { return m_Wrapper.m_Input; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InputActions set) { return set.Get(); }
        public void SetCallbacks(IInputActions instance)
        {
            if (m_Wrapper.m_InputActionsCallbackInterface != null)
            {
                @AddForce.started -= m_Wrapper.m_InputActionsCallbackInterface.OnAddForce;
                @AddForce.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnAddForce;
                @AddForce.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnAddForce;
                @Fire.started -= m_Wrapper.m_InputActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnFire;
                @InventoryToggle.started -= m_Wrapper.m_InputActionsCallbackInterface.OnInventoryToggle;
                @InventoryToggle.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnInventoryToggle;
                @InventoryToggle.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnInventoryToggle;
                @Menu.started -= m_Wrapper.m_InputActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnMenu;
                @Direction.started -= m_Wrapper.m_InputActionsCallbackInterface.OnDirection;
                @Direction.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnDirection;
                @Direction.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnDirection;
                @Rotate.started -= m_Wrapper.m_InputActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnRotate;
                @SlowDown.started -= m_Wrapper.m_InputActionsCallbackInterface.OnSlowDown;
                @SlowDown.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnSlowDown;
                @SlowDown.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnSlowDown;
                @Hotbar1.started -= m_Wrapper.m_InputActionsCallbackInterface.OnHotbar1;
                @Hotbar1.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnHotbar1;
                @Hotbar1.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnHotbar1;
                @Hotbar2.started -= m_Wrapper.m_InputActionsCallbackInterface.OnHotbar2;
                @Hotbar2.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnHotbar2;
                @Hotbar2.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnHotbar2;
                @Hotbar3.started -= m_Wrapper.m_InputActionsCallbackInterface.OnHotbar3;
                @Hotbar3.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnHotbar3;
                @Hotbar3.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnHotbar3;
                @Hotbar4.started -= m_Wrapper.m_InputActionsCallbackInterface.OnHotbar4;
                @Hotbar4.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnHotbar4;
                @Hotbar4.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnHotbar4;
                @Hotbar5.started -= m_Wrapper.m_InputActionsCallbackInterface.OnHotbar5;
                @Hotbar5.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnHotbar5;
                @Hotbar5.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnHotbar5;
                @Exit.started -= m_Wrapper.m_InputActionsCallbackInterface.OnExit;
                @Exit.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnExit;
                @Exit.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnExit;
            }
            m_Wrapper.m_InputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AddForce.started += instance.OnAddForce;
                @AddForce.performed += instance.OnAddForce;
                @AddForce.canceled += instance.OnAddForce;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @InventoryToggle.started += instance.OnInventoryToggle;
                @InventoryToggle.performed += instance.OnInventoryToggle;
                @InventoryToggle.canceled += instance.OnInventoryToggle;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
                @Direction.started += instance.OnDirection;
                @Direction.performed += instance.OnDirection;
                @Direction.canceled += instance.OnDirection;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @SlowDown.started += instance.OnSlowDown;
                @SlowDown.performed += instance.OnSlowDown;
                @SlowDown.canceled += instance.OnSlowDown;
                @Hotbar1.started += instance.OnHotbar1;
                @Hotbar1.performed += instance.OnHotbar1;
                @Hotbar1.canceled += instance.OnHotbar1;
                @Hotbar2.started += instance.OnHotbar2;
                @Hotbar2.performed += instance.OnHotbar2;
                @Hotbar2.canceled += instance.OnHotbar2;
                @Hotbar3.started += instance.OnHotbar3;
                @Hotbar3.performed += instance.OnHotbar3;
                @Hotbar3.canceled += instance.OnHotbar3;
                @Hotbar4.started += instance.OnHotbar4;
                @Hotbar4.performed += instance.OnHotbar4;
                @Hotbar4.canceled += instance.OnHotbar4;
                @Hotbar5.started += instance.OnHotbar5;
                @Hotbar5.performed += instance.OnHotbar5;
                @Hotbar5.canceled += instance.OnHotbar5;
                @Exit.started += instance.OnExit;
                @Exit.performed += instance.OnExit;
                @Exit.canceled += instance.OnExit;
            }
        }
    }
    public InputActions @Input => new InputActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard + Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IInputActions
    {
        void OnAddForce(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnInventoryToggle(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
        void OnDirection(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnSlowDown(InputAction.CallbackContext context);
        void OnHotbar1(InputAction.CallbackContext context);
        void OnHotbar2(InputAction.CallbackContext context);
        void OnHotbar3(InputAction.CallbackContext context);
        void OnHotbar4(InputAction.CallbackContext context);
        void OnHotbar5(InputAction.CallbackContext context);
        void OnExit(InputAction.CallbackContext context);
    }
}
