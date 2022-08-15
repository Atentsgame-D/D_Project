using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour, IPointerClickHandler
{
    uint id;
    private EquipmentSlot itemSlot;

    private Image itemImage;

    private EquipmentUI equipUI;
    public EquipmentUI EquipUI => equipUI;

    public EquipmentSlot ItemSlot { get => itemSlot; }

    public uint ID { get => id; }

    public EquipmentType equipmentSlotType = EquipmentType.Weapon;

    protected void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void Initialize(uint newID, EquipmentSlot targetSlot)
    {
        equipUI = GameManager.Inst.EquipUI;
        id = newID;
        targetSlot.equipmentType = equipmentSlotType;
        itemSlot = targetSlot;
        itemSlot.onSlotItemChange = Refresh;
    }

    public void Refresh()
    {
        if (itemSlot.SlotItemData != null)
        {
            itemImage.sprite = itemSlot.SlotItemData.itemIcon;
            itemImage.color = Color.white;
        }
        else
        {
            itemImage.sprite = null;
            itemImage.color = Color.clear;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) 
        {
            GameManager.Inst.EquipUI.equipment.UnEqiupmemt(ItemSlot.SlotItemData);
            ItemSlot.ClearSlotItem();
        }
    }
}
