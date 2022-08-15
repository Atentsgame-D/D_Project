using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store
{
    /// <summary>
    /// �κ��丮�� ������ �� ������ ĭ
    /// </summary>
    ItemSlot_Store[] slots = null;

    /// <summary>
    /// �κ��丮 �⺻ ũ��
    /// </summary>
    public const int Default_Store_Size = 12;

    /// <summary>
    /// TempSlot�� ID
    /// </summary>
    public const uint TempSlotID = 99999;   // ���ڴ� �ǹ̰� ����. Slot Index�� �������� ���� ���̸� �ȴ�.

    // ������Ƽ ------------------------------------------------------------------------------------------------------

    /// <summary>
    /// �κ��丮�� ũ��
    /// </summary>
    public int SlotCount => slots.Length;

    /// <summary>
    /// �ε���. �κ��丮���� ���� ��������
    /// </summary>
    /// <param name="index">������ ������ ��</param>
    /// <returns>index��°�� ������ ����</returns>
    public ItemSlot_Store this[int index]  => slots[index];

    /// <summary>
    /// �κ��丮 ������
    /// </summary>
    /// <param name="size">�κ��丮�� ���� ��</param>
    public Store(int size = Default_Store_Size)
    {
        slots = new ItemSlot_Store[size];
        for(int i=0; i<size; i++)
        {
            slots[i] = new ItemSlot_Store();
        }
    }

    public bool AddItem(ItemIDCode code)
    {
        return AddItem(GameManager.Inst.ItemData[code]);
    }

    public void BuyItem(ItemSlot_Store slot)
    {
        Player player = GameManager.Inst.MainPlayer;
        Inventory inven = GameManager.Inst.InvenUI.inven;

        if( player.Money >= slot.SlotItemData.value )
        {
            if( inven.AddItem(slot.SlotItemData))
            {
                player.Money -= (int)slot.SlotItemData.value;
            }
        }
    }

    public bool AddItem(ItemData data)
    {
        bool result = false;

        //Debug.Log($"�κ��丮�� {data.itemName}�� �߰��մϴ�.");
        ItemSlot_Store empty = FindEmptySlot();        //������ �󽽷� ã��

        if (empty != null)
        {
            empty.AssignSlotItem(data);
            result = true;
            //Debug.Log($"������ ���Կ� {data.itemName}�� �Ҵ��մϴ�.");
        }
        else
        {
            //Debug.Log("���� : �κ��丮�� ������ �����߽��ϴ�.");
        }

        return result;
    }

    ItemSlot_Store FindEmptySlot()
    {
        ItemSlot_Store result = null;

        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
            {
                result = slot;
                break;
            }
        }

        return result;
    }
}
