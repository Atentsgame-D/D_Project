using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot
{
    ItemData slotItemData;

    uint itemCount = 0;

    public uint ItemCount
    {
        get => itemCount;

        private set
        {
            itemCount = value;
            onSlotItemChange?.Invoke();
        }
    }

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

    public ItemSlot () { }

    public ItemSlot (ItemData data, uint count)
    {
        slotItemData = data;
        ItemCount = count;
    }

    public ItemSlot(ItemSlot other)
    {
        slotItemData = other.SlotItemData;
        ItemCount = other.ItemCount;
    }

    public void AssignSlotItem(ItemData itemData, uint count = 1)
    {
        ItemCount = count;
        SlotItemData = itemData;
    }

    /// <summary>
    /// 같은 종류의 아이템이 추가되어 아이템 갯수가 증가하는 상황에 사용
    /// </summary>
    /// <param name="count">증가시킬 갯수</param>
    /// <returns>최대치를 넘어선 갯수, 0이면 다 증가시킨 상황</returns>
    public uint IncreaseSlotItem(uint count = 1)
    {
        uint newCount = ItemCount + count;
        int overCount = (int)newCount - (int)SlotItemData.maxStackCount;
        if(overCount > 0)
        {
            ItemCount = SlotItemData.maxStackCount;
        }
        else
        {
            ItemCount = newCount;
            overCount = 0;
        }
        return (uint)overCount;
    }

    public void DecreaseSlotItem(uint count = 1)
    {
        int newCount = (int)ItemCount - (int)count;
        if( newCount < 1)   // 최종적으로 갯수가 0이 되면 비우기
        {
            ClearSlotItem();
        }
        else
        {
            ItemCount = (uint)newCount;
        }
    }

    public void ClearSlotItem()
    {
        SlotItemData = null;
        ItemCount = 0;
    }

    public bool IsEmpty()
    {
        return SlotItemData == null;
    }
}
