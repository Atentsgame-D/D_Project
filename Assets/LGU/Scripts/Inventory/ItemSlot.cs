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
    /// ���� ������ �������� �߰��Ǿ� ������ ������ �����ϴ� ��Ȳ�� ���
    /// </summary>
    /// <param name="count">������ų ����</param>
    /// <returns>�ִ�ġ�� �Ѿ ����, 0�̸� �� ������Ų ��Ȳ</returns>
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
        if( newCount < 1)   // ���������� ������ 0�� �Ǹ� ����
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
