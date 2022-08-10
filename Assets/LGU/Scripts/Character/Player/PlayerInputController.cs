using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInputController : MonoBehaviour
{
    public float runSpeed = 6.0f;
    public float turnSpeed = 10.0f;
    public float walkSpeed = 3.0f;

    GameManager manager;
    //public GameObject useText;
    public GameObject scanObj;

    //bool tryUse = false;
    //bool isTrigger = false;

    enum MoveMode
    {
        Walk = 0,
        Run
    }

    MoveMode moveMode = MoveMode.Run;

    PlayerInputActions actions;
    CharacterController controller;
    Animator anim;
    Player player;

    Vector3 inputDir = Vector3.zero;
    Quaternion targetRotation = Quaternion.identity;

    private void Awake()
    {
        actions = new PlayerInputActions();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
        manager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += OnMove;
        actions.Player.Move.canceled += OnMove;
        actions.Player.MoveModeChange.performed += OnMoveModeChange;
        actions.Player.Attack.performed += OnAttack;
        actions.Player.LockOn.performed += OnLockOn;
        actions.Player.Pickup.performed += OnPickup;
        actions.Player.Use.performed += OnUseInput;
        actions.ShortCut.Enable();
        actions.ShortCut.InventoryOnOff.performed += OnInventoryShortCut;
    }

    private void OnDisable()
    {
        actions.ShortCut.InventoryOnOff.performed -= OnInventoryShortCut;
        actions.ShortCut.Disable();
        actions.Player.Use.performed -= OnUseInput;
        actions.Player.Pickup.performed -= OnPickup;
        actions.Player.LockOn.performed -= OnLockOn;
        actions.Player.Attack.performed -= OnAttack;
        actions.Player.MoveModeChange.performed -= OnMoveModeChange;
        actions.Player.Move.canceled -= OnMove;
        actions.Player.Move.performed -= OnMove;
        actions.Player.Disable();
    }

    private void Start()
    {
        //GameManager.Inst.InvenUI.OnInventoryOpen += () => actions.Player.Disable();
        //GameManager.Inst.InvenUI.OnInventoryClose += () => actions.Player.Enable();
    }

    private void OnUseInput(InputAction.CallbackContext obj)
    {
        Use();
    }

    public void Use()
    {
        //if (isTrigger)
        //{
        //    if (tryUse)
        //    {
        //        tryUse = false;
        //    }
        //    else
        //    {
        //        tryUse = true;
        //    }
        //    useText.gameObject.SetActive(!tryUse);
        //}
        if (scanObj != null)
        {
            manager.Action(scanObj);
        }
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
            {
                scanObj = null;
                manager.talkindex = 0;
            }

        }
        else
        {
            scanObj = null;
            manager.talkindex = 0;
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        player.TurnOnAura(false);
        Vector2 input = context.ReadValue<Vector2>();

        inputDir.x = input.x;
        inputDir.y = 0.0f;
        inputDir.z = input.y;
        //inputDir.Normalize();

        if(inputDir.sqrMagnitude > 0.0f)
        {
            inputDir = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * inputDir;
            targetRotation = Quaternion.LookRotation(inputDir);
            inputDir.y = -4.9f;
        }
    }

    private void OnMoveModeChange(InputAction.CallbackContext context)
    {
        if(moveMode == MoveMode.Walk)
        {
            moveMode = MoveMode.Run;
        }
        else
        {
            moveMode = MoveMode.Walk;
        }
    }

    private void OnAttack(InputAction.CallbackContext obj)
    {
        player.TurnOnAura(true);
        anim.SetFloat("ComboState", Mathf.Repeat(anim.GetCurrentAnimatorStateInfo(0).normalizedTime, 1.0f));
        anim.ResetTrigger("Attack");
        anim.SetTrigger("Attack");
    }

    private void Update()
    {
        if( player.LockOnTarget != null)
        {
            targetRotation = Quaternion.LookRotation(player.LockOnTarget.position - transform.position);
        }
        float speed = 1.0f;
        if(inputDir.sqrMagnitude > 0.0f)
        {
            if(moveMode == MoveMode.Run)
            {
                anim.SetFloat("Speed", 1.0f);
                speed = runSpeed;
            }
            else if (moveMode == MoveMode.Walk)
            {
                anim.SetFloat("Speed", 0.3f);
                speed = walkSpeed;
            }
            controller.Move(speed * Time.deltaTime * inputDir);
        }
        else
        {
            anim.SetFloat("Speed", 0.0f);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        ScanObject();

        if (scanObj == null)
        {
            manager.talkPanel.SetActive(false);
        }
    }

    private void OnLockOn(InputAction.CallbackContext _)
    {
        player.LockOnToggle();
    }

    private void OnPickup(InputAction.CallbackContext _)
    {
        player.ItemPickup();
    }

    private void OnInventoryShortCut(InputAction.CallbackContext _)
    {
        GameManager.Inst.InvenUI.InventoyOnOffSwitch();
    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.CompareTag("Store"))
        {
            GameManager.Inst.StoreUI.Open();
            GameManager.Inst.InvenUI.Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Store"))
        {
            GameManager.Inst.StoreUI.Close();
            GameManager.Inst.InvenUI.Close();
        }
    }
}
