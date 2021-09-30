// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/Inputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Inputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Editor"",
            ""id"": ""66554035-9166-49f4-8d95-28f06bdfa607"",
            ""actions"": [
                {
                    ""name"": ""Camera Rotation"",
                    ""type"": ""Value"",
                    ""id"": ""197537e8-bdf0-4dc3-9c9c-6121de9396fc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera Rotation Enabled"",
                    ""type"": ""Value"",
                    ""id"": ""79d564e1-1428-43f8-9133-fc558717c2b6"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera Radius"",
                    ""type"": ""Value"",
                    ""id"": ""e76da899-eb8f-4404-9cca-dd7899ec97e7"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Remove Block"",
                    ""type"": ""Button"",
                    ""id"": ""ed141a06-9921-4450-b7c8-8a28cfda1b41"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Add Block"",
                    ""type"": ""Button"",
                    ""id"": ""85f4ce18-d969-4473-ba6d-4b77649bdb66"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Add Remove Block Touchscreen"",
                    ""type"": ""Button"",
                    ""id"": ""88290f7f-09c2-4ec5-a951-660d4322666f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse Position"",
                    ""type"": ""Value"",
                    ""id"": ""87659fec-1f51-434b-bf96-ee26a2cb85e9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""68dd2268-65dd-413a-8c46-702f4576354e"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9317c04d-138b-43b3-a396-ed3a1c4c7057"",
                    ""path"": ""<Touchscreen>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3443ab35-309a-4558-a6d4-579bc3f2aa1e"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Rotation Enabled"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ab2cbe1-2c0f-417b-bb1e-75015c567fd3"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Rotation Enabled"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90030396-6d1d-402c-a936-11a3912344ff"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Radius"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea0b6680-fe07-4911-835a-04be0ef3eeb9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Remove Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c03ab1c9-0a7a-44e7-b644-d295026aa671"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71b06562-fa90-4e68-a2ca-1a5c37b56244"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0810f77a-517d-4cc3-b3ea-094566785def"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Add Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b69e346-1dec-46e6-bbd8-4ee623f519d6"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Add Remove Block Touchscreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Editor
        m_Editor = asset.FindActionMap("Editor", throwIfNotFound: true);
        m_Editor_CameraRotation = m_Editor.FindAction("Camera Rotation", throwIfNotFound: true);
        m_Editor_CameraRotationEnabled = m_Editor.FindAction("Camera Rotation Enabled", throwIfNotFound: true);
        m_Editor_CameraRadius = m_Editor.FindAction("Camera Radius", throwIfNotFound: true);
        m_Editor_RemoveBlock = m_Editor.FindAction("Remove Block", throwIfNotFound: true);
        m_Editor_AddBlock = m_Editor.FindAction("Add Block", throwIfNotFound: true);
        m_Editor_AddRemoveBlockTouchscreen = m_Editor.FindAction("Add Remove Block Touchscreen", throwIfNotFound: true);
        m_Editor_MousePosition = m_Editor.FindAction("Mouse Position", throwIfNotFound: true);
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

    // Editor
    private readonly InputActionMap m_Editor;
    private IEditorActions m_EditorActionsCallbackInterface;
    private readonly InputAction m_Editor_CameraRotation;
    private readonly InputAction m_Editor_CameraRotationEnabled;
    private readonly InputAction m_Editor_CameraRadius;
    private readonly InputAction m_Editor_RemoveBlock;
    private readonly InputAction m_Editor_AddBlock;
    private readonly InputAction m_Editor_AddRemoveBlockTouchscreen;
    private readonly InputAction m_Editor_MousePosition;
    public struct EditorActions
    {
        private @Inputs m_Wrapper;
        public EditorActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @CameraRotation => m_Wrapper.m_Editor_CameraRotation;
        public InputAction @CameraRotationEnabled => m_Wrapper.m_Editor_CameraRotationEnabled;
        public InputAction @CameraRadius => m_Wrapper.m_Editor_CameraRadius;
        public InputAction @RemoveBlock => m_Wrapper.m_Editor_RemoveBlock;
        public InputAction @AddBlock => m_Wrapper.m_Editor_AddBlock;
        public InputAction @AddRemoveBlockTouchscreen => m_Wrapper.m_Editor_AddRemoveBlockTouchscreen;
        public InputAction @MousePosition => m_Wrapper.m_Editor_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_Editor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(EditorActions set) { return set.Get(); }
        public void SetCallbacks(IEditorActions instance)
        {
            if (m_Wrapper.m_EditorActionsCallbackInterface != null)
            {
                @CameraRotation.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraRotation;
                @CameraRotation.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraRotation;
                @CameraRotation.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraRotation;
                @CameraRotationEnabled.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraRotationEnabled;
                @CameraRotationEnabled.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraRotationEnabled;
                @CameraRotationEnabled.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraRotationEnabled;
                @CameraRadius.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraRadius;
                @CameraRadius.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraRadius;
                @CameraRadius.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraRadius;
                @RemoveBlock.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnRemoveBlock;
                @RemoveBlock.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnRemoveBlock;
                @RemoveBlock.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnRemoveBlock;
                @AddBlock.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnAddBlock;
                @AddBlock.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnAddBlock;
                @AddBlock.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnAddBlock;
                @AddRemoveBlockTouchscreen.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnAddRemoveBlockTouchscreen;
                @AddRemoveBlockTouchscreen.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnAddRemoveBlockTouchscreen;
                @AddRemoveBlockTouchscreen.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnAddRemoveBlockTouchscreen;
                @MousePosition.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_EditorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CameraRotation.started += instance.OnCameraRotation;
                @CameraRotation.performed += instance.OnCameraRotation;
                @CameraRotation.canceled += instance.OnCameraRotation;
                @CameraRotationEnabled.started += instance.OnCameraRotationEnabled;
                @CameraRotationEnabled.performed += instance.OnCameraRotationEnabled;
                @CameraRotationEnabled.canceled += instance.OnCameraRotationEnabled;
                @CameraRadius.started += instance.OnCameraRadius;
                @CameraRadius.performed += instance.OnCameraRadius;
                @CameraRadius.canceled += instance.OnCameraRadius;
                @RemoveBlock.started += instance.OnRemoveBlock;
                @RemoveBlock.performed += instance.OnRemoveBlock;
                @RemoveBlock.canceled += instance.OnRemoveBlock;
                @AddBlock.started += instance.OnAddBlock;
                @AddBlock.performed += instance.OnAddBlock;
                @AddBlock.canceled += instance.OnAddBlock;
                @AddRemoveBlockTouchscreen.started += instance.OnAddRemoveBlockTouchscreen;
                @AddRemoveBlockTouchscreen.performed += instance.OnAddRemoveBlockTouchscreen;
                @AddRemoveBlockTouchscreen.canceled += instance.OnAddRemoveBlockTouchscreen;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public EditorActions @Editor => new EditorActions(this);
    public interface IEditorActions
    {
        void OnCameraRotation(InputAction.CallbackContext context);
        void OnCameraRotationEnabled(InputAction.CallbackContext context);
        void OnCameraRadius(InputAction.CallbackContext context);
        void OnRemoveBlock(InputAction.CallbackContext context);
        void OnAddBlock(InputAction.CallbackContext context);
        void OnAddRemoveBlockTouchscreen(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
}
