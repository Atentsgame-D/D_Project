using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot_Store
{
    ItemData slotItemData;

    public ItemData SlotItemData
    {
        get => slotItemData;
        private set
        {
            if(slotItemData != value)
            {
                slotItemData = value;
                onSlotItemChange?.Invoke();
            }
        }
    }

    public System.Action onSlotItemChange;

    public ItemSlot_Store() { }

    public ItemSlot_Store(ItemData data)
    {
        slotItemData = data;
    }

    public ItemSlot_Store(ItemSlot other)
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
}
