using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot
{
    public EquipmentType equipmentType = EquipmentType.Weapon;
    private ItemData slotItemData;

    public ItemData SlotItemData
    {
        get => slotItemData;
        private set
        {
            if (slotItemData != value)
            {
                slotItemData = value;
                onSlotItemChange?.Invoke();
            }
        }
    }

    public System.Action onSlotItemChange;

    public EquipmentSlot() { }

    public EquipmentSlot(ItemData data)
    {
        slotItemData = data;
    }

    public EquipmentSlot(ItemSlot other)
    {
        slotItemData = other.SlotItemData;
    }

    public bool IsEmpty()
    {
        return SlotItemData == null;
    }

    public void AssignSlotItem(ItemData itemData)
    {
        SlotItemData = itemData;
    }

    public void ClearSlotItem()
    {
        SlotItemData = null;
    }
}
