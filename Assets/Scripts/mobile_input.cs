//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Scripts/mobile_input.inputactions
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

public partial class @Mobile_input: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Mobile_input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""mobile_input"",
    ""maps"": [
        {
            ""name"": ""MainAction"",
            ""id"": ""9a8a25f6-c357-466f-9474-78caf7aabe96"",
            ""actions"": [
                {
                    ""name"": ""Touch"",
                    ""type"": ""Button"",
                    ""id"": ""3c1d7db4-325c-46d6-bd17-9b3aa8a6b656"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TouchPos"",
                    ""type"": ""Value"",
                    ""id"": ""0fa62ced-aff7-4668-8719-3c35e04bf1d5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ae2e92ea-779f-435f-8f32-2e8765ccbc19"",
                    ""path"": ""<Touchscreen>/primaryTouch/indirectTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""defaultMobile"",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d23442a2-df0b-4e9e-b1be-efa62ef2a7d1"",
                    ""path"": ""<Touchscreen>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""defaultMobile"",
                    ""action"": ""TouchPos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""defaultMobile"",
            ""bindingGroup"": ""defaultMobile"",
            ""devices"": []
        }
    ]
}");
        // MainAction
        m_MainAction = asset.FindActionMap("MainAction", throwIfNotFound: true);
        m_MainAction_Touch = m_MainAction.FindAction("Touch", throwIfNotFound: true);
        m_MainAction_TouchPos = m_MainAction.FindAction("TouchPos", throwIfNotFound: true);
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

    // MainAction
    private readonly InputActionMap m_MainAction;
    private List<IMainActionActions> m_MainActionActionsCallbackInterfaces = new List<IMainActionActions>();
    private readonly InputAction m_MainAction_Touch;
    private readonly InputAction m_MainAction_TouchPos;
    public struct MainActionActions
    {
        private @Mobile_input m_Wrapper;
        public MainActionActions(@Mobile_input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Touch => m_Wrapper.m_MainAction_Touch;
        public InputAction @TouchPos => m_Wrapper.m_MainAction_TouchPos;
        public InputActionMap Get() { return m_Wrapper.m_MainAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActionActions set) { return set.Get(); }
        public void AddCallbacks(IMainActionActions instance)
        {
            if (instance == null || m_Wrapper.m_MainActionActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MainActionActionsCallbackInterfaces.Add(instance);
            @Touch.started += instance.OnTouch;
            @Touch.performed += instance.OnTouch;
            @Touch.canceled += instance.OnTouch;
            @TouchPos.started += instance.OnTouchPos;
            @TouchPos.performed += instance.OnTouchPos;
            @TouchPos.canceled += instance.OnTouchPos;
        }

        private void UnregisterCallbacks(IMainActionActions instance)
        {
            @Touch.started -= instance.OnTouch;
            @Touch.performed -= instance.OnTouch;
            @Touch.canceled -= instance.OnTouch;
            @TouchPos.started -= instance.OnTouchPos;
            @TouchPos.performed -= instance.OnTouchPos;
            @TouchPos.canceled -= instance.OnTouchPos;
        }

        public void RemoveCallbacks(IMainActionActions instance)
        {
            if (m_Wrapper.m_MainActionActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMainActionActions instance)
        {
            foreach (var item in m_Wrapper.m_MainActionActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MainActionActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MainActionActions @MainAction => new MainActionActions(this);
    private int m_defaultMobileSchemeIndex = -1;
    public InputControlScheme defaultMobileScheme
    {
        get
        {
            if (m_defaultMobileSchemeIndex == -1) m_defaultMobileSchemeIndex = asset.FindControlSchemeIndex("defaultMobile");
            return asset.controlSchemes[m_defaultMobileSchemeIndex];
        }
    }
    public interface IMainActionActions
    {
        void OnTouch(InputAction.CallbackContext context);
        void OnTouchPos(InputAction.CallbackContext context);
    }
}
