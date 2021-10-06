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
                    ""name"": ""Pick Block"",
                    ""type"": ""Button"",
                    ""id"": ""10bd8519-5cb8-4ac4-a8c9-9d0950cdf595"",
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
                },
                {
                    ""name"": ""Camera Move"",
                    ""type"": ""Value"",
                    ""id"": ""7dbd7aaa-294d-4a93-abe7-81d6341469ea"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera Up Down"",
                    ""type"": ""Value"",
                    ""id"": ""488c711a-3b05-49e1-b6d0-cd8ea3ff9974"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera Rotation"",
                    ""type"": ""Value"",
                    ""id"": ""044b37f3-db7f-47eb-bd24-c9c4a85dc0a0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera Rotation Enabled"",
                    ""type"": ""Value"",
                    ""id"": ""a064cac4-1c75-4508-9aea-69d0ba87212a"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera Speed"",
                    ""type"": ""Value"",
                    ""id"": ""b5df4359-060b-4993-aa13-d924eb8d1a5e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Change Tool"",
                    ""type"": ""Button"",
                    ""id"": ""f93641ae-583c-4bce-adfe-0e08e76e4cda"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
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
                    ""name"": ""2D Vector"",
                    ""id"": ""12e9c16f-472e-4390-8111-aab50ce6c4f4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5d5e5a44-288e-425c-aa3d-42bbb3ad951e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f48ea019-95b0-45f2-a2a1-8a50bde67ee5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""48a45c57-22c2-4744-97d9-9622bea71b70"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""30cda488-7024-4c9c-ba19-8a4dde34ad83"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2148204c-7a4d-498d-8481-fff03bf39f4c"",
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
                    ""id"": ""3a89f024-979f-495c-9f13-15d0115a66ae"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Rotation Enabled"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""9a071e33-036a-4019-8c7a-5e6fb27554f9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Up Down"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f15885ea-de2f-4621-b0ed-21bb59a4651d"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Up Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f6d4a181-4ded-4184-915c-2d3a68aee361"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Up Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""dbcff75e-0420-4b9c-9ad2-e3a4dbe17fb5"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Speed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f87896e-6bc6-4db9-9b6e-136ab4670e2e"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Change Tool"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e4afeea-dc85-421d-ac24-e3bbbcd311b2"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": ""MultiTap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pick Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player"",
            ""id"": ""975d3f32-889f-40f0-a20f-8010428084c4"",
            ""actions"": [
                {
                    ""name"": ""Touchscreen Press"",
                    ""type"": ""Button"",
                    ""id"": ""051a346f-fd57-4c76-88b1-5d5dd938db5c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Touchscreen Position"",
                    ""type"": ""Value"",
                    ""id"": ""b147966e-f28d-47b2-b903-2d877bd45363"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Keyboard Up"",
                    ""type"": ""Button"",
                    ""id"": ""50967b4a-995f-4cb7-916f-6b4ecb71d6c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Keyboard Down"",
                    ""type"": ""Button"",
                    ""id"": ""eeecf23e-1ab1-4925-b59b-4efdc6a8860e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Keyboard Left"",
                    ""type"": ""Button"",
                    ""id"": ""565c1867-a50a-4f50-8ab4-9ac98e1f27b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Keyboard Right"",
                    ""type"": ""Button"",
                    ""id"": ""5796f54a-f634-4413-adf7-49998c256fa0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2a07fa73-2adc-46f0-9bba-1398b77ffb29"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touchscreen Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9fe0451-fd53-4904-830b-2a215ac9b706"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touchscreen Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9488db1-7806-41ff-b741-ea69787b18fb"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cab6566f-a006-43a0-89ea-b08cc3b6966c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""221b0ec2-e6b1-44c3-8f85-6fbb3d07540c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aade54fa-91cf-4841-95e7-962a21000040"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2207befc-8092-44e8-9185-818bd927b08f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6738f65-d09d-461a-b4e7-d5e86a9f4c32"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a33addb5-c5ec-4441-a838-8f0cd98e1ab7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3312eae-caca-4760-8272-a0ab903fc96d"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard Right"",
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
        m_Editor_RemoveBlock = m_Editor.FindAction("Remove Block", throwIfNotFound: true);
        m_Editor_AddBlock = m_Editor.FindAction("Add Block", throwIfNotFound: true);
        m_Editor_PickBlock = m_Editor.FindAction("Pick Block", throwIfNotFound: true);
        m_Editor_MousePosition = m_Editor.FindAction("Mouse Position", throwIfNotFound: true);
        m_Editor_CameraMove = m_Editor.FindAction("Camera Move", throwIfNotFound: true);
        m_Editor_CameraUpDown = m_Editor.FindAction("Camera Up Down", throwIfNotFound: true);
        m_Editor_CameraRotation = m_Editor.FindAction("Camera Rotation", throwIfNotFound: true);
        m_Editor_CameraRotationEnabled = m_Editor.FindAction("Camera Rotation Enabled", throwIfNotFound: true);
        m_Editor_CameraSpeed = m_Editor.FindAction("Camera Speed", throwIfNotFound: true);
        m_Editor_ChangeTool = m_Editor.FindAction("Change Tool", throwIfNotFound: true);
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_TouchscreenPress = m_Player.FindAction("Touchscreen Press", throwIfNotFound: true);
        m_Player_TouchscreenPosition = m_Player.FindAction("Touchscreen Position", throwIfNotFound: true);
        m_Player_KeyboardUp = m_Player.FindAction("Keyboard Up", throwIfNotFound: true);
        m_Player_KeyboardDown = m_Player.FindAction("Keyboard Down", throwIfNotFound: true);
        m_Player_KeyboardLeft = m_Player.FindAction("Keyboard Left", throwIfNotFound: true);
        m_Player_KeyboardRight = m_Player.FindAction("Keyboard Right", throwIfNotFound: true);
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
    private readonly InputAction m_Editor_RemoveBlock;
    private readonly InputAction m_Editor_AddBlock;
    private readonly InputAction m_Editor_PickBlock;
    private readonly InputAction m_Editor_MousePosition;
    private readonly InputAction m_Editor_CameraMove;
    private readonly InputAction m_Editor_CameraUpDown;
    private readonly InputAction m_Editor_CameraRotation;
    private readonly InputAction m_Editor_CameraRotationEnabled;
    private readonly InputAction m_Editor_CameraSpeed;
    private readonly InputAction m_Editor_ChangeTool;
    public struct EditorActions
    {
        private @Inputs m_Wrapper;
        public EditorActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @RemoveBlock => m_Wrapper.m_Editor_RemoveBlock;
        public InputAction @AddBlock => m_Wrapper.m_Editor_AddBlock;
        public InputAction @PickBlock => m_Wrapper.m_Editor_PickBlock;
        public InputAction @MousePosition => m_Wrapper.m_Editor_MousePosition;
        public InputAction @CameraMove => m_Wrapper.m_Editor_CameraMove;
        public InputAction @CameraUpDown => m_Wrapper.m_Editor_CameraUpDown;
        public InputAction @CameraRotation => m_Wrapper.m_Editor_CameraRotation;
        public InputAction @CameraRotationEnabled => m_Wrapper.m_Editor_CameraRotationEnabled;
        public InputAction @CameraSpeed => m_Wrapper.m_Editor_CameraSpeed;
        public InputAction @ChangeTool => m_Wrapper.m_Editor_ChangeTool;
        public InputActionMap Get() { return m_Wrapper.m_Editor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(EditorActions set) { return set.Get(); }
        public void SetCallbacks(IEditorActions instance)
        {
            if (m_Wrapper.m_EditorActionsCallbackInterface != null)
            {
                @RemoveBlock.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnRemoveBlock;
                @RemoveBlock.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnRemoveBlock;
                @RemoveBlock.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnRemoveBlock;
                @AddBlock.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnAddBlock;
                @AddBlock.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnAddBlock;
                @AddBlock.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnAddBlock;
                @PickBlock.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnPickBlock;
                @PickBlock.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnPickBlock;
                @PickBlock.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnPickBlock;
                @MousePosition.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnMousePosition;
                @CameraMove.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraMove;
                @CameraMove.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraMove;
                @CameraMove.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraMove;
                @CameraUpDown.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraUpDown;
                @CameraUpDown.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraUpDown;
                @CameraUpDown.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraUpDown;
                @CameraRotation.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraRotation;
                @CameraRotation.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraRotation;
                @CameraRotation.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraRotation;
                @CameraRotationEnabled.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraRotationEnabled;
                @CameraRotationEnabled.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraRotationEnabled;
                @CameraRotationEnabled.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraRotationEnabled;
                @CameraSpeed.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraSpeed;
                @CameraSpeed.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraSpeed;
                @CameraSpeed.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnCameraSpeed;
                @ChangeTool.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnChangeTool;
                @ChangeTool.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnChangeTool;
                @ChangeTool.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnChangeTool;
            }
            m_Wrapper.m_EditorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RemoveBlock.started += instance.OnRemoveBlock;
                @RemoveBlock.performed += instance.OnRemoveBlock;
                @RemoveBlock.canceled += instance.OnRemoveBlock;
                @AddBlock.started += instance.OnAddBlock;
                @AddBlock.performed += instance.OnAddBlock;
                @AddBlock.canceled += instance.OnAddBlock;
                @PickBlock.started += instance.OnPickBlock;
                @PickBlock.performed += instance.OnPickBlock;
                @PickBlock.canceled += instance.OnPickBlock;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @CameraMove.started += instance.OnCameraMove;
                @CameraMove.performed += instance.OnCameraMove;
                @CameraMove.canceled += instance.OnCameraMove;
                @CameraUpDown.started += instance.OnCameraUpDown;
                @CameraUpDown.performed += instance.OnCameraUpDown;
                @CameraUpDown.canceled += instance.OnCameraUpDown;
                @CameraRotation.started += instance.OnCameraRotation;
                @CameraRotation.performed += instance.OnCameraRotation;
                @CameraRotation.canceled += instance.OnCameraRotation;
                @CameraRotationEnabled.started += instance.OnCameraRotationEnabled;
                @CameraRotationEnabled.performed += instance.OnCameraRotationEnabled;
                @CameraRotationEnabled.canceled += instance.OnCameraRotationEnabled;
                @CameraSpeed.started += instance.OnCameraSpeed;
                @CameraSpeed.performed += instance.OnCameraSpeed;
                @CameraSpeed.canceled += instance.OnCameraSpeed;
                @ChangeTool.started += instance.OnChangeTool;
                @ChangeTool.performed += instance.OnChangeTool;
                @ChangeTool.canceled += instance.OnChangeTool;
            }
        }
    }
    public EditorActions @Editor => new EditorActions(this);

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_TouchscreenPress;
    private readonly InputAction m_Player_TouchscreenPosition;
    private readonly InputAction m_Player_KeyboardUp;
    private readonly InputAction m_Player_KeyboardDown;
    private readonly InputAction m_Player_KeyboardLeft;
    private readonly InputAction m_Player_KeyboardRight;
    public struct PlayerActions
    {
        private @Inputs m_Wrapper;
        public PlayerActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchscreenPress => m_Wrapper.m_Player_TouchscreenPress;
        public InputAction @TouchscreenPosition => m_Wrapper.m_Player_TouchscreenPosition;
        public InputAction @KeyboardUp => m_Wrapper.m_Player_KeyboardUp;
        public InputAction @KeyboardDown => m_Wrapper.m_Player_KeyboardDown;
        public InputAction @KeyboardLeft => m_Wrapper.m_Player_KeyboardLeft;
        public InputAction @KeyboardRight => m_Wrapper.m_Player_KeyboardRight;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @TouchscreenPress.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchscreenPress;
                @TouchscreenPress.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchscreenPress;
                @TouchscreenPress.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchscreenPress;
                @TouchscreenPosition.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchscreenPosition;
                @TouchscreenPosition.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchscreenPosition;
                @TouchscreenPosition.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchscreenPosition;
                @KeyboardUp.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKeyboardUp;
                @KeyboardUp.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKeyboardUp;
                @KeyboardUp.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKeyboardUp;
                @KeyboardDown.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKeyboardDown;
                @KeyboardDown.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKeyboardDown;
                @KeyboardDown.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKeyboardDown;
                @KeyboardLeft.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKeyboardLeft;
                @KeyboardLeft.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKeyboardLeft;
                @KeyboardLeft.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKeyboardLeft;
                @KeyboardRight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKeyboardRight;
                @KeyboardRight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKeyboardRight;
                @KeyboardRight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKeyboardRight;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TouchscreenPress.started += instance.OnTouchscreenPress;
                @TouchscreenPress.performed += instance.OnTouchscreenPress;
                @TouchscreenPress.canceled += instance.OnTouchscreenPress;
                @TouchscreenPosition.started += instance.OnTouchscreenPosition;
                @TouchscreenPosition.performed += instance.OnTouchscreenPosition;
                @TouchscreenPosition.canceled += instance.OnTouchscreenPosition;
                @KeyboardUp.started += instance.OnKeyboardUp;
                @KeyboardUp.performed += instance.OnKeyboardUp;
                @KeyboardUp.canceled += instance.OnKeyboardUp;
                @KeyboardDown.started += instance.OnKeyboardDown;
                @KeyboardDown.performed += instance.OnKeyboardDown;
                @KeyboardDown.canceled += instance.OnKeyboardDown;
                @KeyboardLeft.started += instance.OnKeyboardLeft;
                @KeyboardLeft.performed += instance.OnKeyboardLeft;
                @KeyboardLeft.canceled += instance.OnKeyboardLeft;
                @KeyboardRight.started += instance.OnKeyboardRight;
                @KeyboardRight.performed += instance.OnKeyboardRight;
                @KeyboardRight.canceled += instance.OnKeyboardRight;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IEditorActions
    {
        void OnRemoveBlock(InputAction.CallbackContext context);
        void OnAddBlock(InputAction.CallbackContext context);
        void OnPickBlock(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnCameraMove(InputAction.CallbackContext context);
        void OnCameraUpDown(InputAction.CallbackContext context);
        void OnCameraRotation(InputAction.CallbackContext context);
        void OnCameraRotationEnabled(InputAction.CallbackContext context);
        void OnCameraSpeed(InputAction.CallbackContext context);
        void OnChangeTool(InputAction.CallbackContext context);
    }
    public interface IPlayerActions
    {
        void OnTouchscreenPress(InputAction.CallbackContext context);
        void OnTouchscreenPosition(InputAction.CallbackContext context);
        void OnKeyboardUp(InputAction.CallbackContext context);
        void OnKeyboardDown(InputAction.CallbackContext context);
        void OnKeyboardLeft(InputAction.CallbackContext context);
        void OnKeyboardRight(InputAction.CallbackContext context);
    }
}
