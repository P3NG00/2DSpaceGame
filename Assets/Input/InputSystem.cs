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
                    ""name"": ""Aim At Mouse"",
                    ""type"": ""Value"",
                    ""id"": ""03dce504-6579-42a0-b1cf-2024f60e6df8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim In Direction"",
                    ""type"": ""Value"",
                    ""id"": ""3f9f38c2-fa50-4ece-bd98-6c87238ffef6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cheat Menu"",
                    ""type"": ""Button"",
                    ""id"": ""15a5c2a1-4090-4872-8ce0-09a8117e52cb"",
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
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""00c7eef2-64bf-4137-a39d-cf3fd0b40bec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu Down"",
                    ""type"": ""Button"",
                    ""id"": ""2765cd99-289d-4af8-a9f5-d5772ad2923f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu Left"",
                    ""type"": ""Button"",
                    ""id"": ""03bc9f2e-cc15-4a05-898d-7c219335ca1b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu Right"",
                    ""type"": ""Button"",
                    ""id"": ""4f2b0702-32ac-4c25-9459-ee64a8a0b271"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu Up"",
                    ""type"": ""Button"",
                    ""id"": ""5e4c67e1-b665-4b4a-af0a-0dc948340ba7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""b54c0df7-699a-42aa-bb06-7b434d99daa1"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""Scale(factor=1.2)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slow Down"",
                    ""type"": ""Button"",
                    ""id"": ""b44f9430-4cb6-4553-9224-0dc4c95188db"",
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
                    ""name"": """",
                    ""id"": ""c0bdd35c-0dca-4afe-9463-3c44cd643863"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Aim At Mouse"",
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
                    ""action"": ""Aim In Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0cff634-79a1-4a28-b430-55982808b3f6"",
                    ""path"": ""<Keyboard>/f8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Cheat Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
                },
                {
                    ""name"": """",
                    ""id"": ""398dad8c-3a97-4588-af62-2df56d4bfdc8"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Inventory"",
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
                    ""action"": ""Inventory"",
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
                    ""name"": ""Controller"",
                    ""id"": ""aeb65b9c-7258-436a-b853-5cc87add7103"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
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
                    ""name"": ""1D Axis"",
                    ""id"": ""38a2f8ef-9f7d-4207-84e0-a77926269297"",
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
                    ""id"": ""36b9ee6b-2760-4db7-9aa5-c12662e098e7"",
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
                    ""id"": ""92b36960-8a9f-4386-bd9d-82fc8b94dcc6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""86868716-b6c6-4048-86c4-8b317f25f2fc"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Menu Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47cde2cf-5425-4048-b546-42dfbce51e8f"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Menu Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5d92fa6-2d28-41ce-a543-af2c43c4c5ed"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Menu Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8c4c5289-b7bf-4a1b-8d73-32371256e5d1"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Menu Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c85b26fc-3806-4c2f-895c-ed7daa7df08c"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Menu Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cff24113-fcb8-466d-ad4a-e8a82e259553"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Menu Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""15963fef-dec7-4acf-81b1-4c68d03b8a9a"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Menu Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""876f0877-3aab-4493-b327-9f828f5d0a7e"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Menu Right"",
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
        m_Input_AimAtMouse = m_Input.FindAction("Aim At Mouse", throwIfNotFound: true);
        m_Input_AimInDirection = m_Input.FindAction("Aim In Direction", throwIfNotFound: true);
        m_Input_CheatMenu = m_Input.FindAction("Cheat Menu", throwIfNotFound: true);
        m_Input_Exit = m_Input.FindAction("Exit", throwIfNotFound: true);
        m_Input_Fire = m_Input.FindAction("Fire", throwIfNotFound: true);
        m_Input_Hotbar1 = m_Input.FindAction("Hotbar 1", throwIfNotFound: true);
        m_Input_Hotbar2 = m_Input.FindAction("Hotbar 2", throwIfNotFound: true);
        m_Input_Hotbar3 = m_Input.FindAction("Hotbar 3", throwIfNotFound: true);
        m_Input_Hotbar4 = m_Input.FindAction("Hotbar 4", throwIfNotFound: true);
        m_Input_Hotbar5 = m_Input.FindAction("Hotbar 5", throwIfNotFound: true);
        m_Input_Inventory = m_Input.FindAction("Inventory", throwIfNotFound: true);
        m_Input_MenuDown = m_Input.FindAction("Menu Down", throwIfNotFound: true);
        m_Input_MenuLeft = m_Input.FindAction("Menu Left", throwIfNotFound: true);
        m_Input_MenuRight = m_Input.FindAction("Menu Right", throwIfNotFound: true);
        m_Input_MenuUp = m_Input.FindAction("Menu Up", throwIfNotFound: true);
        m_Input_Rotate = m_Input.FindAction("Rotate", throwIfNotFound: true);
        m_Input_SlowDown = m_Input.FindAction("Slow Down", throwIfNotFound: true);
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
    private readonly InputAction m_Input_AimAtMouse;
    private readonly InputAction m_Input_AimInDirection;
    private readonly InputAction m_Input_CheatMenu;
    private readonly InputAction m_Input_Exit;
    private readonly InputAction m_Input_Fire;
    private readonly InputAction m_Input_Hotbar1;
    private readonly InputAction m_Input_Hotbar2;
    private readonly InputAction m_Input_Hotbar3;
    private readonly InputAction m_Input_Hotbar4;
    private readonly InputAction m_Input_Hotbar5;
    private readonly InputAction m_Input_Inventory;
    private readonly InputAction m_Input_MenuDown;
    private readonly InputAction m_Input_MenuLeft;
    private readonly InputAction m_Input_MenuRight;
    private readonly InputAction m_Input_MenuUp;
    private readonly InputAction m_Input_Rotate;
    private readonly InputAction m_Input_SlowDown;
    public struct InputActions
    {
        private @InputSystem m_Wrapper;
        public InputActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @AddForce => m_Wrapper.m_Input_AddForce;
        public InputAction @AimAtMouse => m_Wrapper.m_Input_AimAtMouse;
        public InputAction @AimInDirection => m_Wrapper.m_Input_AimInDirection;
        public InputAction @CheatMenu => m_Wrapper.m_Input_CheatMenu;
        public InputAction @Exit => m_Wrapper.m_Input_Exit;
        public InputAction @Fire => m_Wrapper.m_Input_Fire;
        public InputAction @Hotbar1 => m_Wrapper.m_Input_Hotbar1;
        public InputAction @Hotbar2 => m_Wrapper.m_Input_Hotbar2;
        public InputAction @Hotbar3 => m_Wrapper.m_Input_Hotbar3;
        public InputAction @Hotbar4 => m_Wrapper.m_Input_Hotbar4;
        public InputAction @Hotbar5 => m_Wrapper.m_Input_Hotbar5;
        public InputAction @Inventory => m_Wrapper.m_Input_Inventory;
        public InputAction @MenuDown => m_Wrapper.m_Input_MenuDown;
        public InputAction @MenuLeft => m_Wrapper.m_Input_MenuLeft;
        public InputAction @MenuRight => m_Wrapper.m_Input_MenuRight;
        public InputAction @MenuUp => m_Wrapper.m_Input_MenuUp;
        public InputAction @Rotate => m_Wrapper.m_Input_Rotate;
        public InputAction @SlowDown => m_Wrapper.m_Input_SlowDown;
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
                @AimAtMouse.started -= m_Wrapper.m_InputActionsCallbackInterface.OnAimAtMouse;
                @AimAtMouse.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnAimAtMouse;
                @AimAtMouse.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnAimAtMouse;
                @AimInDirection.started -= m_Wrapper.m_InputActionsCallbackInterface.OnAimInDirection;
                @AimInDirection.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnAimInDirection;
                @AimInDirection.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnAimInDirection;
                @CheatMenu.started -= m_Wrapper.m_InputActionsCallbackInterface.OnCheatMenu;
                @CheatMenu.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnCheatMenu;
                @CheatMenu.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnCheatMenu;
                @Exit.started -= m_Wrapper.m_InputActionsCallbackInterface.OnExit;
                @Exit.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnExit;
                @Exit.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnExit;
                @Fire.started -= m_Wrapper.m_InputActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnFire;
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
                @Inventory.started -= m_Wrapper.m_InputActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnInventory;
                @MenuDown.started -= m_Wrapper.m_InputActionsCallbackInterface.OnMenuDown;
                @MenuDown.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnMenuDown;
                @MenuDown.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnMenuDown;
                @MenuLeft.started -= m_Wrapper.m_InputActionsCallbackInterface.OnMenuLeft;
                @MenuLeft.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnMenuLeft;
                @MenuLeft.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnMenuLeft;
                @MenuRight.started -= m_Wrapper.m_InputActionsCallbackInterface.OnMenuRight;
                @MenuRight.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnMenuRight;
                @MenuRight.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnMenuRight;
                @MenuUp.started -= m_Wrapper.m_InputActionsCallbackInterface.OnMenuUp;
                @MenuUp.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnMenuUp;
                @MenuUp.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnMenuUp;
                @Rotate.started -= m_Wrapper.m_InputActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnRotate;
                @SlowDown.started -= m_Wrapper.m_InputActionsCallbackInterface.OnSlowDown;
                @SlowDown.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnSlowDown;
                @SlowDown.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnSlowDown;
            }
            m_Wrapper.m_InputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AddForce.started += instance.OnAddForce;
                @AddForce.performed += instance.OnAddForce;
                @AddForce.canceled += instance.OnAddForce;
                @AimAtMouse.started += instance.OnAimAtMouse;
                @AimAtMouse.performed += instance.OnAimAtMouse;
                @AimAtMouse.canceled += instance.OnAimAtMouse;
                @AimInDirection.started += instance.OnAimInDirection;
                @AimInDirection.performed += instance.OnAimInDirection;
                @AimInDirection.canceled += instance.OnAimInDirection;
                @CheatMenu.started += instance.OnCheatMenu;
                @CheatMenu.performed += instance.OnCheatMenu;
                @CheatMenu.canceled += instance.OnCheatMenu;
                @Exit.started += instance.OnExit;
                @Exit.performed += instance.OnExit;
                @Exit.canceled += instance.OnExit;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
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
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @MenuDown.started += instance.OnMenuDown;
                @MenuDown.performed += instance.OnMenuDown;
                @MenuDown.canceled += instance.OnMenuDown;
                @MenuLeft.started += instance.OnMenuLeft;
                @MenuLeft.performed += instance.OnMenuLeft;
                @MenuLeft.canceled += instance.OnMenuLeft;
                @MenuRight.started += instance.OnMenuRight;
                @MenuRight.performed += instance.OnMenuRight;
                @MenuRight.canceled += instance.OnMenuRight;
                @MenuUp.started += instance.OnMenuUp;
                @MenuUp.performed += instance.OnMenuUp;
                @MenuUp.canceled += instance.OnMenuUp;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @SlowDown.started += instance.OnSlowDown;
                @SlowDown.performed += instance.OnSlowDown;
                @SlowDown.canceled += instance.OnSlowDown;
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
        void OnAimAtMouse(InputAction.CallbackContext context);
        void OnAimInDirection(InputAction.CallbackContext context);
        void OnCheatMenu(InputAction.CallbackContext context);
        void OnExit(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnHotbar1(InputAction.CallbackContext context);
        void OnHotbar2(InputAction.CallbackContext context);
        void OnHotbar3(InputAction.CallbackContext context);
        void OnHotbar4(InputAction.CallbackContext context);
        void OnHotbar5(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnMenuDown(InputAction.CallbackContext context);
        void OnMenuLeft(InputAction.CallbackContext context);
        void OnMenuRight(InputAction.CallbackContext context);
        void OnMenuUp(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnSlowDown(InputAction.CallbackContext context);
    }
}
