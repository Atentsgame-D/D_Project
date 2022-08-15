using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class Player : MonoBehaviour, IEquipTarget
{
    // Item 관련 -------------------------------------------------------------------------------
    private float itemPickupRange = 3.0f;       // 아이템 줍는 범위
    private int money = 10000;                      // 플레이어의 소지 금액
    public int Money
    {
        get => money;
        set
        {
            if (money != value)
            {
                money = value;
                OnMoneyChange?.Invoke(money);
            }
        }
    }
    public System.Action<int> OnMoneyChange;

    private float dropRange = 2.0f;
    // ------------------------------------------------------------------------------------------

    // Inventory 관련 ---------------------------------------------------------------------------
    private InventoryUI invenUI;

    public InventoryUI InvenUI { get => invenUI; }

    private Inventory inven;

    private StoreUI store;
    // ------------------------------------------------------------------------------------------

    // Equipment 관련 ---------------------------------------------------------------------------
    private EquipmentUI equipUI;

    public EquipmentUI EquipUI { get => equipUI; }

    private Equipment equip;
    // ------------------------------------------------------------------------------------------

    PlayerInputActions actions = null;
    Animator anim;
    GameObject useText;
    CharacterController controller;
    SkillCoolTimeManager coolTime;
    GameManager manager;
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
    float maxMP = 300.0f;
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
    Vector2 input = Vector2.zero;
    public float speed = 5.0f;
    public float walkSpeed = 5.0f;
    public float runSpeed = 10.0f;
    public float turnSpeed = 15.0f;

    public float jumpPower = 2.5f;
    public float gravity = -9.81f;

    // 상점확인용 변수--------------------------
    private bool isStore = false;
    //-----------------------------------------
    public bool tryUse = false;
    public bool isTrigger = false;
    // 스킬용 변수------------------------------
    public bool gianHP = false;
    bool Onskill01 = false;
    public float skill01Distance = 10.0f;
    // 전투스탯 ---------------------
   
    //공
    public float attackPower = 20.0f;
    public float AttackPower => attackPower;

    //방 
    public float defencePower = 10.0f;
    public float DefencePower { get => defencePower; }

    public ItemSlot EquipItemSlot => throw new NotImplementedException();

    // 카메라 -----------------------------
    Transform cameraTarget; //카메라 타겟

    Vector2 look;

    private float TargetX;
    private float TargetY;
    [Header("카메라 최대/최소 각도")]
    public float TopClamp = 70.0f;
    public float BottomClamp = -30.0f;

    private float inputY;
    private float inputX;

    //---------------------------------------
    private void Awake()
    {
        cameraTarget = transform.Find("PlayerCameraRoot").GetComponent<Transform>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        coolTime = GameObject.Find("SkillInfo").GetComponent<SkillCoolTimeManager>();
        actions = new PlayerInputActions();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        useText = GameObject.Find("UseText_GameObject");
        invenUI = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();
<<<<<<< HEAD
        store = GameObject.Find("Store").GetComponent<StoreUI>();
=======
        equipUI = GameObject.Find("EquipmentUI").GetComponent<EquipmentUI>();
>>>>>>> f42b2ef082b8f2ca076675490e4ae83c09345574
    }
    private void Start()
    {
        TargetY = cameraTarget.rotation.eulerAngles.y;

        useText.gameObject.SetActive(false);
        inven = new Inventory();
        invenUI.InitializeInventory(inven);

        equip = new Equipment();
        equipUI.InitializeEquipment(equip);

        inven.AddItem(ItemIDCode.Weapon_Wooden_Sword);
        inven.AddItem(ItemIDCode.Equipment_Leather_Helmet);
        inven.AddItem(ItemIDCode.Equipment_Leather_Boot);
    }
    private void Update()
    {
        if (inputDir.sqrMagnitude > 0.0f)
        {
            if (moveMode == MoveMode.Run)
            {
                speed = runSpeed;
                anim.SetBool("OnRun", true);
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

        //스킬1을 사용했을떄 전방으로 돌진
        if (Onskill01)
        {
            transform.Translate(skill01Distance * Vector3.forward * Time.deltaTime, Space.Self);
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
    private void LateUpdate()
    {
        CameraRotate();
    }
    //OnEnable / OnDisable-----------------------------------------------------------------------------
    private void OnEnable()
    {
        actions.PlayerMove.Enable();
        actions.PlayerMove.Move.performed += OnMoveInput;                //wasd
        actions.PlayerMove.Move.canceled += OnMoveInput;
        actions.PlayerMove.Look.performed += OnLook;

        actions.Player.Enable();
        actions.Player.Use.performed += OnUseInput;                  //f키
        actions.Player.Jump.performed += OnJump;
        actions.Player.MoveModeChange.performed += OnMoveModeChange; // 왼쪽 쉬프트 
        actions.Player.Skill1.performed += OnSkill_1;
        actions.Player.Skill2.performed += OnSkill_2;
        actions.Player.Skill3.performed += OnSkill_3;
        actions.Player.Skill4.performed += OnSkill_4;
        actions.Player.PickUp.performed += OnPickUp;
        actions.Player.NormalAttack.performed += OnNormalAttack;

        actions.ShortCut.Enable();
        actions.ShortCut.InventoryOnOff.performed += OnInventortyOnOff;
        actions.ShortCut.EquipmentOnOff.performed += OnEquipmentOnOff;
    }

    private void OnDisable()
    {
        actions.ShortCut.EquipmentOnOff.performed -= OnEquipmentOnOff;
        actions.ShortCut.InventoryOnOff.performed -= OnInventortyOnOff;
        actions.ShortCut.Disable();

        actions.Player.NormalAttack.performed -= OnNormalAttack;
        actions.Player.PickUp.performed -= OnPickUp;
        actions.Player.Skill4.performed -= OnSkill_4;
        actions.Player.Skill3.performed -= OnSkill_3;
        actions.Player.Skill2.performed -= OnSkill_2;
        actions.Player.Skill1.performed -= OnSkill_1;
        actions.Player.MoveModeChange.performed -= OnMoveModeChange;
        actions.Player.Jump.performed -= OnJump;
        actions.Player.Use.performed -= OnUseInput;
        actions.Player.Disable();

        actions.PlayerMove.Look.performed -= OnLook;
        actions.PlayerMove.Move.canceled -= OnMoveInput;
        actions.PlayerMove.Move.performed -= OnMoveInput;
        actions.PlayerMove.Disable();
    }
    private void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }
    private void OnMoveInput(InputAction.CallbackContext context) // 방향키 입력시 이동
    {
        input = context.ReadValue<Vector2>();
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
                if (isStore)
                {
                    store.Open();
                    invenUI.Open();
                    isStore = false;
                    useText.gameObject.SetActive(false);
                }
                tryUse = false;
            }
            else
            {
                if (!isStore)
                {
                    store.Close();
                    invenUI.Close();
                    isStore = true;
                    useText.gameObject.SetActive(true);
                }
                tryUse = true;
            }
        }
        if (scanObj != null)
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
        tryUse = true;
        if (other.CompareTag("Store"))
        {
            isStore = true;
        }
        store.Close();
        invenUI.Close();
        useText.gameObject.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        isTrigger = false;
        tryUse = false;
        if (other.CompareTag("Store"))
        {
            isStore = false;
        }

        useText.gameObject.SetActive(false);
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
    private void OnSkill_1(InputAction.CallbackContext _) // 돌진
    {
        if (coolTime.skill1_CoolTime <= 0.0f && Mp > 30.0f && controller.isGrounded)
        {
            Debug.Log("스킬1 발동");
            Mp -= 30.0f;
            StartCoroutine(Skill01());
            coolTime.skill1();
        }
    }
    IEnumerator Skill01()
    {
        actions.PlayerMove.Disable();
        actions.Player.Disable();
        anim.SetBool("OnSkill1", true);
        Onskill01 = true;
        yield return new WaitForSeconds(1.0f);
        Onskill01 = false;
        anim.SetBool("OnSkill1", false);
        actions.Player.Enable();
        actions.PlayerMove.Enable();
    }

    private void OnSkill_2(InputAction.CallbackContext _) // 회전회오리
    {
        if (coolTime.skill2_CoolTime <= 0.0f && Mp > 30.0f && controller.isGrounded)
        {
            Mp -= 30.0f;
            StartCoroutine(Skill02());
            coolTime.skill2();
        }
    }
    IEnumerator Skill02()
    {
        attackPower *= 0.5f;
        anim.SetBool("OnSkill2", true);
        actions.Player.Disable();
        yield return new WaitForSeconds(3.0f);
        actions.Player.Enable();
        anim.SetBool("OnSkill2", false);
        attackPower *= 2.0f;
    }

    private void OnSkill_3(InputAction.CallbackContext _) // 흡혈 버프
    {
        if (coolTime.skill3_CoolTime <= 0.0f && Mp > 50.0f && controller.isGrounded)
        {
            Mp -= 50.0f;
            StartCoroutine(Skill03());
            coolTime.skill3();
        }
    }

    IEnumerator Skill03()
    {
        gianHP = true;
        yield return new WaitForSeconds(6.0f);
        gianHP = false;
    }

    private void OnSkill_4(InputAction.CallbackContext _) // 도약
    {
        if (coolTime.skill4_CoolTime <= 0.0f && Mp > 30.0f) // && controller.isGrounded
        {
            Debug.Log("스킬4 발동");
            Mp -= 30.0f;
            inputDir.y = jumpPower * 1.25f; //기존 점프의 1.25배 높이 뜀
            coolTime.skill4();
        }
    }

    private void OnNormalAttack(InputAction.CallbackContext _)
    {
        anim.SetTrigger("OnNormalAttack");
    }

    //-----------------------------------------------------------------------
    //카메라 회전함수
    private void CameraRotate()
    {
        inputX = Input.GetAxis("Mouse X");  //마우스의 좌우 움직임 감지
        inputY = Input.GetAxis("Mouse Y");  //마우스의 상하 움직임 감지

        // 마우스를 이동이 한방향이라도 있을때 OnLook으로 받은 마우스 위치 정보를 이용해 이동함
        if (inputX != 0 || inputY != 0)
        {
            TargetX += look.x;
            TargetY += look.y;
        }

        // 좌우 이동을 360도로 제한
        TargetX = Mathf.Clamp(TargetX, 0.0f, 360.0f);
        // 상하 이동을 BottomClamp(-30도), TopClamp(70도)의 범위를 벗어나지 않게 제한
        TargetY = Mathf.Clamp(TargetY, BottomClamp, TopClamp);

        // 카메라 타겟을 회전시켜 카메라를 회전시킴
        // 좌우 이동은 TargetX값(마우스 좌우 이동 값)을 Y축을 기준으로 돌리고
        // 상하 이동은 TargetY겂(마우스 상하 이동  값)을 X축을 기준으로 돌리기 때문에
        // Quaternion.Euler(TargetY, TargetX, 0.0f); 으로 선언한다
        cameraTarget.rotation = Quaternion.Euler(TargetY, TargetX, 0.0f);
    }

    public void TakeDamage(float damage)
    {
        damage -= defencePower;
        Hp -= damage;
    }

    //-----------------------------------------------------------------------
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

    // Item관련 -------------------------------------------------------------
    private void OnPickUp(InputAction.CallbackContext context)
    {
        ItemPickUp();
    }

    public void ItemPickUp()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, itemPickupRange, LayerMask.GetMask("Item"));
        foreach (var col in cols)
        {
            Item item = col.GetComponent<Item>();
            if (item != null)
            {
                IConsumalbe consumable = item.data as IConsumalbe;
                if (consumable != null)
                {
                    consumable.Consume(this);
                    Destroy(col.gameObject);
                }
                else
                {
                    if (inven.AddItem(item.data))
                    {
                        Destroy(col.gameObject);
                    }
                }
            }
        }
        //Debug.Log($"{money}");
    }

    public Vector3 ItemDropPosition(Vector3 inputPos)
    {
        Vector3 result = Vector3.zero;
        Vector3 toInputPos = inputPos - transform.position;
        if (toInputPos.sqrMagnitude > dropRange * dropRange)
        {
            result = transform.position + toInputPos.normalized * dropRange;
        }
        else
        {
            result = inputPos;
        }

        return result;
    }

    private void OnInventortyOnOff(InputAction.CallbackContext obj)
    {
        GameManager.Inst.InvenUI.InventoryOnOffSwitch();
    }
    //-----------------------------------------------------------------------

    // 장비관련 --------------------------------------------------------------

    private void OnEquipmentOnOff(InputAction.CallbackContext obj)
    {
        GameManager.Inst.EquipUI.EquipmentOnOffSwitch();
    }

    public void EquipWeapon(ItemSlot weaponSlot)
    {
    }

    public void UnEquipWeapon()
    {
    }
    //-----------------------------------------------------------------------
}
