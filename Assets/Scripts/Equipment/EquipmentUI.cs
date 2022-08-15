using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public GameObject slotPrefab;   // 초기화시 새로 생성해야할 경우 사용
    private Transform slotParent;

    private CanvasGroup canvasGroup = null;
    private PlayerInputActions inputActions = null;

    public Equipment equipment = null;
    private EquipmentSlotUI[] slotUIs;

    private void Awake()
    {
        // 미리 찾아놓기
        canvasGroup = GetComponent<CanvasGroup>();
        slotParent = transform.Find("Equipment_Base");

        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(Close);

        inputActions = new PlayerInputActions();
    }

    private void Start()
    {
        Close();    // 시작할 때 무조건 닫기
    }

    public void InitializeEquipment(Equipment newEquipment)
    {
        equipment = newEquipment;
        if (Equipment.Default_Equipment_Size != equipment.SlotCount)      // 기본 사이즈와 다르면 기본 슬롯 삭제
        {
            // 기존 슬롯 전부 삭제
            EquipmentSlotUI[] slots = GetComponentsInChildren<EquipmentSlotUI>();
            foreach (var slot in slots)
            {
                Destroy(slot.gameObject);
            }
            // 새로 만들기
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
        RefreshAllSlots();      // 전체 슬롯UI 갱신
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
        if (canvasGroup.blocksRaycasts)  // 캔버스 그룹의 blocksRaycasts를 기준으로 처리
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
}
