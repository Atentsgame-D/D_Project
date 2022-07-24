using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class Player : MonoBehaviour
{
    // Money 관련 -------------------------------------------------------------------------------
    private int money = 0;                      // 플레이어의 소지 금액
    public int Money
    {
        get => money;
        private set
        {
            if (money != value)
            {
                money = value;
                OnMoneyChange?.Invoke(money);
            }
        }
    }

    public System.Action<int> OnMoneyChange;
    // ------------------------------------------------------------------------------------------

    // Inventory 관련 ---------------------------------------------------------------------------
    private InventoryUI invenUI;

    public InventoryUI InvenUI { get => invenUI; }

    private Inventory inven = new Inventory();
    // ------------------------------------------------------------------------------------------

    PlayerInputActions actions = null;
    Animator anim;
    GameObject useText;
    CharacterController controller;
    private GameManager manager;
    public GameObject scanObj;

    Vector3 inputDir = Vector3.zero;
    Quaternion targetRotation = Quaternion.identity;

    // 캐릭터 HP----------------------------
    float maxHP = 500.0f; 
    float hp = 100.0f;
    public float MaxHP { get => maxHP; }
    public float Hp 
    { 
        get => hp; 
        set
        {
            if (hp != value)
            {
                hp = Mathf.Clamp(value, 0.0f, maxHP);
                OnHpChange?.Invoke(hp);
            }
        }
    }
    public System.Action<float> OnHpChange;
    //캐릭터 MP------------------------------
    float maxMP = 200.0f;
    float mp = 50.0f;
    public float MaxMP { get => maxMP; }
    public float Mp 
    { 
        get => mp;
        set
        {
            if (mp != value)
            {
                mp = Mathf.Clamp(value, 0.0f, maxMP);
                OnMpChange?.Invoke(mp);
            }
        }
    }
    public System.Action<float> OnMpChange;
    //---------------------------------------
    enum MoveMode
    {
        Walk = 0,
        Run
    }

    MoveMode moveMode = MoveMode.Walk;
    //캐릭터 이동 관련---------------------------------------
    float speed = 5.0f;
    public float walkSpeed = 5.0f;
    public float runSpeed = 10.0f;
    public float turnSpeed = 15.0f;

    public float jumpPower = 5.0f;
    public float gravity = -9.81f;
    //---------------------------------------
    public bool tryUse = false;
    public bool isTrigger = false;
    //---------------------------------------
    private void Awake()
    {
        actions = new PlayerInputActions();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        useText = GameObject.Find("UseText_GameObject");
        invenUI = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();
    }
    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        useText.gameObject.SetActive(false);
        invenUI.InitializeInventory(inven);
        //manager.TalkPanel.SetActive(false);
    }
    private void Update()
    {

        inputDir.y -= 9.8f * Time.deltaTime;
        if (inputDir.sqrMagnitude > 0.0f)
        {
            if (moveMode == MoveMode.Run)
            {
                speed = runSpeed;
                anim.SetBool("OnRun",true);
            }
            else if (moveMode == MoveMode.Walk)
            {   
                speed = walkSpeed;
                anim.SetBool("OnRun", false);
            }
            controller.Move(speed * Time.deltaTime * inputDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        //캐릭터가 땅에 붙어있지 않으면 중력 작용
        if (!controller.isGrounded) 
        {
            inputDir.y += gravity * Time.deltaTime;
        }

        ScanObject();
        if (scanObj == null)
        {
            if (manager.TalkPanel != null)
            {
                manager.TalkPanel.SetActive(false);
            }
        }
    }


    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += OnMoveInput;                //방향키
        actions.Player.Move.canceled += OnMoveInput;
        actions.Player.Use.performed += OnUseInput;                  //f키
        actions.Player.Jump.performed += OnJump;
        actions.Player.MoveModeChange.performed += OnMoveModeChange; // 왼쪽 쉬프트 
        actions.Player.Skill1.performed += OnSkill_Q;
        actions.Player.Skill2.performed += OnSkill_W;
        actions.Player.Skill3.performed += OnSkill_E;
        actions.Player.Skill4.performed += OnSkill_R;
    }

    private void OnDisable()
    {
        actions.Player.Skill4.performed -= OnSkill_R;
        actions.Player.Skill3.performed -= OnSkill_E;
        actions.Player.Skill2.performed -= OnSkill_W;
        actions.Player.Skill1.performed -= OnSkill_Q;
        actions.Player.MoveModeChange.performed -= OnMoveModeChange;
        actions.Player.Jump.performed -= OnJump;
        actions.Player.Use.performed -= OnUseInput;
        actions.Player.Move.canceled -= OnMoveInput;
        actions.Player.Move.performed -= OnMoveInput;
        actions.Player.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext context) // 방향키 입력시 이동
    {
        Vector2 input = context.ReadValue<Vector2>();

        inputDir.x = input.x;
        inputDir.y = 0.0f;
        inputDir.z = input.y;

        if (inputDir.sqrMagnitude > 0.0f)
        {
            inputDir = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * inputDir;
            targetRotation = Quaternion.LookRotation(inputDir);
            anim.SetBool("OnMove", true);
        }
        else
        {
            anim.SetBool("OnMove", false);
            moveMode = MoveMode.Walk;
        }
    }

    private void OnUseInput(InputAction.CallbackContext context)
    {
        Use();
    }

    public void Use()
    {
        if (isTrigger)
        {
            if (tryUse)
            {
                tryUse = false;
            }
            else
            {
                tryUse = true;
            }
            useText.gameObject.SetActive(!tryUse);
        }
        if(scanObj != null)
        {
            manager.Action(scanObj);
        }
    }
    //------------------
    private void OnJump(InputAction.CallbackContext _)
    {
        if (controller.isGrounded)
        {
            inputDir.y = jumpPower; // y축 이동방향을 점프높이로 설정하여 올라가다가 다시 내려오게 만듦 = 점프
            anim.SetTrigger("OnJump");
        }
    }
    //------------------
    private void OnTriggerEnter(Collider other)
    {
        isTrigger = true;
        useText.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        isTrigger = false;
        tryUse = false;
        useText.SetActive(false);
    }
    private void OnMoveModeChange(InputAction.CallbackContext context) // 쉬프트 키로 달리기 토글 설정. 기본 걷기상태
    {
        if (moveMode == MoveMode.Walk)
        {
            moveMode = MoveMode.Run;
        }
        else
        {
            moveMode = MoveMode.Walk;
        }
    }

    // 스킬 세팅
    private void OnSkill_Q(InputAction.CallbackContext context) // 돌진
    {
    }
    private void OnSkill_W(InputAction.CallbackContext context) // 강공격
    {
    }
    private void OnSkill_E(InputAction.CallbackContext context) // 흡혈 버프
    {
    }
    private void OnSkill_R(InputAction.CallbackContext context) // 도약
    {
    }

    void ScanObject()
    {
        Ray ray = new(transform.position, transform.forward);
        ray.origin += Vector3.up * 0.5f;
        if (Physics.Raycast(ray, out RaycastHit hit, 1.0f, LayerMask.GetMask("Object")))
        {
            if (hit.collider != null)
            {
                scanObj = hit.collider.gameObject;
            }
            else
                scanObj = null;
        }
        else
        {
            scanObj = null;
        }
    }

}
