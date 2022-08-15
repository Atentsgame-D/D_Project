using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store
{
    /// <summary>
    /// 인벤토리가 가지는 각 아이템 칸
    /// </summary>
    ItemSlot_Store[] slots = null;

    /// <summary>
    /// 인벤토리 기본 크기
    /// </summary>
    public const int Default_Store_Size = 12;

    /// <summary>
    /// TempSlot용 ID
    /// </summary>
    public const uint TempSlotID = 99999;   // 숫자는 의미가 없다. Slot Index로 적절하지 않은 값이면 된다.

    // 프로퍼티 ------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 인벤토리의 크기
    /// </summary>
    public int SlotCount => slots.Length;

    /// <summary>
    /// 인덱서. 인벤토리에서 슬롯 가져오기
    /// </summary>
    /// <param name="index">가져올 슬롯의 수</param>
    /// <returns>index번째의 아이템 슬롯</returns>
    public ItemSlot_Store this[int index]  => slots[index];

    /// <summary>
    /// 인벤토리 생성자
    /// </summary>
    /// <param name="size">인벤토리의 슬롯 수</param>
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

        //Debug.Log($"인벤토리에 {data.itemName}을 추가합니다.");
        ItemSlot_Store empty = FindEmptySlot();        //적절한 빈슬롯 찾기

        if (empty != null)
        {
            empty.AssignSlotItem(data);
            result = true;
            //Debug.Log($"아이템 슬롯에 {data.itemName}을 할당합니다.");
        }
        else
        {
            //Debug.Log("실패 : 인벤토리가 가득차 실패했습니다.");
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
