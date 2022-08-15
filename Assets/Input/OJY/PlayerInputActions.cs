//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Input/OJY/PlayerInputActions.inputactions
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

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""f9e01c9d-9cab-4a7d-ac2a-625c702b540a"",
            ""actions"": [
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""d805ca69-8a5c-44b2-adcc-7b6d1c007d02"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MoveModeChange"",
                    ""type"": ""Button"",
                    ""id"": ""b48de213-945c-4b77-a8ce-c411dd725c1a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""880f2687-a9e0-4bd5-8c81-ade440bdf966"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill1"",
                    ""type"": ""Button"",
                    ""id"": ""563f8cc0-b4a2-4502-a274-f7c7341d3dbd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill2"",
                    ""type"": ""Button"",
                    ""id"": ""df9c9823-a998-4fec-a831-b57e7797c774"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill3"",
                    ""type"": ""Button"",
                    ""id"": ""32a36b3e-436c-406d-a3b6-230fbe18054c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill4"",
                    ""type"": ""Button"",
                    ""id"": ""8933b172-8a6a-43ff-baea-d90ff39f3f8e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PickUp"",
                    ""type"": ""Button"",
                    ""id"": ""e41d2ee1-7982-4d7e-b8ed-8655194fb8c7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""NormalAttack"",
                    ""type"": ""Button"",
                    ""id"": ""551e83aa-39e6-4ccb-8140-3341f66cea01"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ada6928d-298c-441f-bd4e-e4517a107610"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""296b6574-f641-49dc-b391-5547a1e70825"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""MoveModeChange"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66ce95e7-3e6e-4735-9d48-a929e5fdb444"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Skill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""861a382e-b80d-46c2-9242-2ec368d2556b"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5388c26-04e5-4211-af77-95765fe4f251"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Skill3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""188401c1-818f-4ebb-8310-b8e1e8cfd6e4"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Skill4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a0e915b-7739-47eb-ae41-02513e7261a3"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2cc1912-4f60-446d-a3a3-20a2083ea222"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48ddacde-373a-4655-9c29-2c9893b844a1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""NormalAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ShortCut"",
            ""id"": ""82491fa7-53cb-4cb3-b946-9a4eead7d430"",
            ""actions"": [
                {
                    ""name"": ""InventoryOnOff"",
                    ""type"": ""Button"",
                    ""id"": ""d45fa55c-8578-4806-b575-7189c306c0a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""EquipmentOnOff"",
                    ""type"": ""Button"",
                    ""id"": ""a867801d-c2ac-4c2f-9e74-c791a77ed418"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4a1ab879-17f1-4200-825b-97ec63a1fc28"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""InventoryOnOff"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9823a8c-a05c-4861-a5cc-487935ebc6fd"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""EquipmentOnOff"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerMove"",
            ""id"": ""f00bb91e-ee13-443c-8114-2dd1ed20f1e0"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""899546a4-41b7-4737-9040-d0d0425a6fb7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""8ca67824-ef9e-4b86-a303-f77e037aa148"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""8312fe65-2b8c-4326-b792-9ee1186469ca"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7767d3a9-de48-46d2-97e8-57c6f09351a1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7ee50faf-43d8-41ef-9d42-dda57a2031b9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ecc663d5-7af0-447c-889f-3d659a4876df"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fe74ad4d-91ff-4261-a2d6-d7036c5fc45a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2d2e8591-2e26-4f6e-9bef-039b8676d5d6"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false),ScaleVector2(x=0.05,y=0.05)"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""22f749b4-87de-4948-8ccb-d272452b9af6"",
            ""actions"": [
                {
                    ""name"": ""ItemDrop"",
                    ""type"": ""Button"",
                    ""id"": ""cdab6e0c-352e-415c-8d59-43eef0c6427c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0340de14-372d-4d57-933d-d6d9e69e9e37"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ItemDrop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Player"",
            ""bindingGroup"": ""Player"",
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
        m_Player_Use = m_Player.FindAction("Use", throwIfNotFound: true);
        m_Player_MoveModeChange = m_Player.FindAction("MoveModeChange", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Skill1 = m_Player.FindAction("Skill1", throwIfNotFound: true);
        m_Player_Skill2 = m_Player.FindAction("Skill2", throwIfNotFound: true);
        m_Player_Skill3 = m_Player.FindAction("Skill3", throwIfNotFound: true);
        m_Player_Skill4 = m_Player.FindAction("Skill4", throwIfNotFound: true);
        m_Player_PickUp = m_Player.FindAction("PickUp", throwIfNotFound: true);
        m_Player_NormalAttack = m_Player.FindAction("NormalAttack", throwIfNotFound: true);
        // ShortCut
        m_ShortCut = asset.FindActionMap("ShortCut", throwIfNotFound: true);
        m_ShortCut_InventoryOnOff = m_ShortCut.FindAction("InventoryOnOff", throwIfNotFound: true);
        m_ShortCut_EquipmentOnOff = m_ShortCut.FindAction("EquipmentOnOff", throwIfNotFound: true);
        // PlayerMove
        m_PlayerMove = asset.FindActionMap("PlayerMove", throwIfNotFound: true);
        m_PlayerMove_Move = m_PlayerMove.FindAction("Move", throwIfNotFound: true);
        m_PlayerMove_Look = m_PlayerMove.FindAction("Look", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_ItemDrop = m_UI.FindAction("ItemDrop", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Use;
    private readonly InputAction m_Player_MoveModeChange;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Skill1;
    private readonly InputAction m_Player_Skill2;
    private readonly InputAction m_Player_Skill3;
    private readonly InputAction m_Player_Skill4;
    private readonly InputAction m_Player_PickUp;
    private readonly InputAction m_Player_NormalAttack;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Use => m_Wrapper.m_Player_Use;
        public InputAction @MoveModeChange => m_Wrapper.m_Player_MoveModeChange;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Skill1 => m_Wrapper.m_Player_Skill1;
        public InputAction @Skill2 => m_Wrapper.m_Player_Skill2;
        public InputAction @Skill3 => m_Wrapper.m_Player_Skill3;
        public InputAction @Skill4 => m_Wrapper.m_Player_Skill4;
        public InputAction @PickUp => m_Wrapper.m_Player_PickUp;
        public InputAction @NormalAttack => m_Wrapper.m_Player_NormalAttack;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Use.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUse;
                @Use.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUse;
                @Use.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUse;
                @MoveModeChange.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveModeChange;
                @MoveModeChange.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveModeChange;
                @MoveModeChange.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveModeChange;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Skill1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill1;
                @Skill1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill1;
                @Skill1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill1;
                @Skill2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill2;
                @Skill2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill2;
                @Skill2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill2;
                @Skill3.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill3;
                @Skill3.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill3;
                @Skill3.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill3;
                @Skill4.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill4;
                @Skill4.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill4;
                @Skill4.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill4;
                @PickUp.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickUp;
                @PickUp.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickUp;
                @PickUp.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickUp;
                @NormalAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNormalAttack;
                @NormalAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNormalAttack;
                @NormalAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNormalAttack;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Use.started += instance.OnUse;
                @Use.performed += instance.OnUse;
                @Use.canceled += instance.OnUse;
                @MoveModeChange.started += instance.OnMoveModeChange;
                @MoveModeChange.performed += instance.OnMoveModeChange;
                @MoveModeChange.canceled += instance.OnMoveModeChange;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Skill1.started += instance.OnSkill1;
                @Skill1.performed += instance.OnSkill1;
                @Skill1.canceled += instance.OnSkill1;
                @Skill2.started += instance.OnSkill2;
                @Skill2.performed += instance.OnSkill2;
                @Skill2.canceled += instance.OnSkill2;
                @Skill3.started += instance.OnSkill3;
                @Skill3.performed += instance.OnSkill3;
                @Skill3.canceled += instance.OnSkill3;
                @Skill4.started += instance.OnSkill4;
                @Skill4.performed += instance.OnSkill4;
                @Skill4.canceled += instance.OnSkill4;
                @PickUp.started += instance.OnPickUp;
                @PickUp.performed += instance.OnPickUp;
                @PickUp.canceled += instance.OnPickUp;
                @NormalAttack.started += instance.OnNormalAttack;
                @NormalAttack.performed += instance.OnNormalAttack;
                @NormalAttack.canceled += instance.OnNormalAttack;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // ShortCut
    private readonly InputActionMap m_ShortCut;
    private IShortCutActions m_ShortCutActionsCallbackInterface;
    private readonly InputAction m_ShortCut_InventoryOnOff;
    private readonly InputAction m_ShortCut_EquipmentOnOff;
    public struct ShortCutActions
    {
        private @PlayerInputActions m_Wrapper;
        public ShortCutActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @InventoryOnOff => m_Wrapper.m_ShortCut_InventoryOnOff;
        public InputAction @EquipmentOnOff => m_Wrapper.m_ShortCut_EquipmentOnOff;
        public InputActionMap Get() { return m_Wrapper.m_ShortCut; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShortCutActions set) { return set.Get(); }
        public void SetCallbacks(IShortCutActions instance)
        {
            if (m_Wrapper.m_ShortCutActionsCallbackInterface != null)
            {
                @InventoryOnOff.started -= m_Wrapper.m_ShortCutActionsCallbackInterface.OnInventoryOnOff;
                @InventoryOnOff.performed -= m_Wrapper.m_ShortCutActionsCallbackInterface.OnInventoryOnOff;
                @InventoryOnOff.canceled -= m_Wrapper.m_ShortCutActionsCallbackInterface.OnInventoryOnOff;
                @EquipmentOnOff.started -= m_Wrapper.m_ShortCutActionsCallbackInterface.OnEquipmentOnOff;
                @EquipmentOnOff.performed -= m_Wrapper.m_ShortCutActionsCallbackInterface.OnEquipmentOnOff;
                @EquipmentOnOff.canceled -= m_Wrapper.m_ShortCutActionsCallbackInterface.OnEquipmentOnOff;
            }
            m_Wrapper.m_ShortCutActionsCallbackInterface = instance;
            if (instance != null)
            {
                @InventoryOnOff.started += instance.OnInventoryOnOff;
                @InventoryOnOff.performed += instance.OnInventoryOnOff;
                @InventoryOnOff.canceled += instance.OnInventoryOnOff;
                @EquipmentOnOff.started += instance.OnEquipmentOnOff;
                @EquipmentOnOff.performed += instance.OnEquipmentOnOff;
                @EquipmentOnOff.canceled += instance.OnEquipmentOnOff;
            }
        }
    }
    public ShortCutActions @ShortCut => new ShortCutActions(this);

    // PlayerMove
    private readonly InputActionMap m_PlayerMove;
    private IPlayerMoveActions m_PlayerMoveActionsCallbackInterface;
    private readonly InputAction m_PlayerMove_Move;
    private readonly InputAction m_PlayerMove_Look;
    public struct PlayerMoveActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerMoveActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerMove_Move;
        public InputAction @Look => m_Wrapper.m_PlayerMove_Look;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMove; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMoveActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMoveActions instance)
        {
            if (m_Wrapper.m_PlayerMoveActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnLook;
            }
            m_Wrapper.m_PlayerMoveActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
            }
        }
    }
    public PlayerMoveActions @PlayerMove => new PlayerMoveActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_ItemDrop;
    public struct UIActions
    {
        private @PlayerInputActions m_Wrapper;
        public UIActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @ItemDrop => m_Wrapper.m_UI_ItemDrop;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @ItemDrop.started -= m_Wrapper.m_UIActionsCallbackInterface.OnItemDrop;
                @ItemDrop.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnItemDrop;
                @ItemDrop.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnItemDrop;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ItemDrop.started += instance.OnItemDrop;
                @ItemDrop.performed += instance.OnItemDrop;
                @ItemDrop.canceled += instance.OnItemDrop;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_PlayerSchemeIndex = -1;
    public InputControlScheme PlayerScheme
    {
        get
        {
            if (m_PlayerSchemeIndex == -1) m_PlayerSchemeIndex = asset.FindControlSchemeIndex("Player");
            return asset.controlSchemes[m_PlayerSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnUse(InputAction.CallbackContext context);
        void OnMoveModeChange(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSkill1(InputAction.CallbackContext context);
        void OnSkill2(InputAction.CallbackContext context);
        void OnSkill3(InputAction.CallbackContext context);
        void OnSkill4(InputAction.CallbackContext context);
        void OnPickUp(InputAction.CallbackContext context);
        void OnNormalAttack(InputAction.CallbackContext context);
    }
    public interface IShortCutActions
    {
        void OnInventoryOnOff(InputAction.CallbackContext context);
        void OnEquipmentOnOff(InputAction.CallbackContext context);
    }
    public interface IPlayerMoveActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnItemDrop(InputAction.CallbackContext context);
    }
}
