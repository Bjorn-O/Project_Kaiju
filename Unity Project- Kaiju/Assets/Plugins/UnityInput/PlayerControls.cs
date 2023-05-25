//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Plugins/UnityInput/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""OnRail"",
            ""id"": ""0427f45c-ce4b-489b-b4ec-18d46ed99449"",
            ""actions"": [
                {
                    ""name"": ""MouseLook"",
                    ""type"": ""Value"",
                    ""id"": ""92924909-f401-4ada-b627-d80c8cf83a3b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Value"",
                    ""id"": ""e329acf7-0a6b-4089-8ddd-052cff6de12a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""ca6da8b5-891b-4f56-b34b-da2082a6de56"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""780388a1-62e7-4df3-ae93-c2e3018e9bc6"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f296ac0a-b950-4249-86fe-df8aecf43211"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""A/D"",
                    ""id"": ""7fc56a16-4395-46fa-ad1e-a468df63677a"",
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
                    ""id"": ""9f46c58a-8c33-49d5-949e-f3a60cb5c126"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8f2647a2-64b5-436f-a111-f1519bd7521c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // OnRail
        m_OnRail = asset.FindActionMap("OnRail", throwIfNotFound: true);
        m_OnRail_MouseLook = m_OnRail.FindAction("MouseLook", throwIfNotFound: true);
        m_OnRail_Fire = m_OnRail.FindAction("Fire", throwIfNotFound: true);
        m_OnRail_Rotate = m_OnRail.FindAction("Rotate", throwIfNotFound: true);
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

    // OnRail
    private readonly InputActionMap m_OnRail;
    private IOnRailActions m_OnRailActionsCallbackInterface;
    private readonly InputAction m_OnRail_MouseLook;
    private readonly InputAction m_OnRail_Fire;
    private readonly InputAction m_OnRail_Rotate;
    public struct OnRailActions
    {
        private @PlayerControls m_Wrapper;
        public OnRailActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseLook => m_Wrapper.m_OnRail_MouseLook;
        public InputAction @Fire => m_Wrapper.m_OnRail_Fire;
        public InputAction @Rotate => m_Wrapper.m_OnRail_Rotate;
        public InputActionMap Get() { return m_Wrapper.m_OnRail; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OnRailActions set) { return set.Get(); }
        public void SetCallbacks(IOnRailActions instance)
        {
            if (m_Wrapper.m_OnRailActionsCallbackInterface != null)
            {
                @MouseLook.started -= m_Wrapper.m_OnRailActionsCallbackInterface.OnMouseLook;
                @MouseLook.performed -= m_Wrapper.m_OnRailActionsCallbackInterface.OnMouseLook;
                @MouseLook.canceled -= m_Wrapper.m_OnRailActionsCallbackInterface.OnMouseLook;
                @Fire.started -= m_Wrapper.m_OnRailActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_OnRailActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_OnRailActionsCallbackInterface.OnFire;
                @Rotate.started -= m_Wrapper.m_OnRailActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_OnRailActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_OnRailActionsCallbackInterface.OnRotate;
            }
            m_Wrapper.m_OnRailActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseLook.started += instance.OnMouseLook;
                @MouseLook.performed += instance.OnMouseLook;
                @MouseLook.canceled += instance.OnMouseLook;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
            }
        }
    }
    public OnRailActions @OnRail => new OnRailActions(this);
    public interface IOnRailActions
    {
        void OnMouseLook(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
    }
}
