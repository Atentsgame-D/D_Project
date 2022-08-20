using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class EquipmentUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject slotPrefab;   // �ʱ�ȭ�� ���� �����ؾ��� ��� ���
    private Transform slotParent;

    private CanvasGroup canvasGroup = null;
    private PlayerInputActions inputActions = null;

    public Equipment equipment = null;
    private EquipmentSlotUI[] slotUIs;

    private bool isMove = false;

    private void Awake()
    {
        // �̸� ã�Ƴ���
        canvasGroup = GetComponent<CanvasGroup>();
        slotParent = transform.Find("Equipment_Base");

        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(Close);

        inputActions = new PlayerInputActions();
    }

    private void Start()
    {
        Close();    // ������ �� ������ �ݱ�
    }

    public void InitializeEquipment(Equipment newEquipment)
    {
        equipment = newEquipment;
        if (Equipment.Default_Equipment_Size != equipment.SlotCount)      // �⺻ ������� �ٸ��� �⺻ ���� ����
        {
            // ���� ���� ���� ����
            EquipmentSlotUI[] slots = GetComponentsInChildren<EquipmentSlotUI>();
            foreach (var slot in slots)
            {
                Destroy(slot.gameObject);
            }
            // ���� �����
            slotUIs = new EquipmentSlotUI[equipment.SlotCount];
            for (int i = 0; i < equipment.SlotCount; i++)
            {
                GameObject obj = Instantiate(slotPrefab, slotParent);
                obj.name = $"{slotPrefab.name}_{i}";
                slotUIs[i] = obj.GetComponent<EquipmentSlotUI>();
                slotUIs[i].Initialize((uint)i, equipment[i]);
            }
        }
        else
        {
            slotUIs = slotParent.GetComponentsInChildren<EquipmentSlotUI>();
            for (int i = 0; i < equipment.SlotCount; i++)
            {
                slotUIs[i].Initialize((uint)i, equipment[i]);
            }
        }
        RefreshAllSlots();      // ��ü ����UI ����
    }

    private void RefreshAllSlots()
    {
        foreach (var slotUI in slotUIs)
        {
            slotUI.Refresh();
        }
    }

    public void EquipmentOnOffSwitch()
    {
        if (canvasGroup.blocksRaycasts)  // ĵ���� �׷��� blocksRaycasts�� �������� ó��
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public EquipmentSlot FindEquipmentSlot(EquipmentType equipmentSlotType)
    {
        foreach (var slotUI in slotUIs)
        {
            if(equipmentSlotType == slotUI.equipmentSlotType)
            {
                return slotUI.ItemSlot;
            }
        }
        return null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isMove)
            transform.position = Mouse.current.position.ReadValue() + new Vector2(0,-230);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) // ��Ŭ���� ���� ó��
        {
            if (!isMove)
                isMove = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) // ��Ŭ���� ���� ó��
        {
            if (isMove)
                isMove = false;
        }
    }
}
