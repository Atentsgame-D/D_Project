using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    // 변수 -----------------------------------------------------------------------------
    // ItemSlot : 아이템 칸 하나
    private ItemSlot[] slots = null;
    private ItemSlot tempSlot = null;
    // ----------------------------------------------------------------------------------

    // 상수 -----------------------------------------------------------------------------
    // 인벤토리 기본 크기
    public const int Default_Inventory_Size = 12;
    // ----------------------------------------------------------------------------------

    // 프로퍼티 --------------------------------------------------------------------------
    // 인벤토리의 크기
    public int SlotCount { get => slots.Length; }
    public ItemSlot this[int index] { get => slots[index]; }    // 인벤토리에서 보이는 슬롯들
    public ItemSlot TempSlot => tempSlot;                       // 임시 목적의 슬롯
    // ----------------------------------------------------------------------------------

    // 함수(주요기능) --------------------------------------------------------------------
    public Inventory(int size = Default_Inventory_Size)
    {
        slots = new ItemSlot[size];
        for (int i = 0; i < size; i++)
        {
            slots[i] = new ItemSlot();
        }
        tempSlot = new ItemSlot();
    }

    /// <summary>
    /// 아이템 추가하기
    /// </summary>
    /// <param name="id">추가할 아이템의 아이디</param>
    /// <returns>아이템 추가 성공 여부(true면 인벤토리에 아이템이 추가됨)</returns>
    public bool AddItem(uint id)
    {
        return AddItem(GameManager.Inst.ItemData[id]);
    }

    /// <summary>
    /// 아이템 추가하기
    /// </summary>
    /// <param name="code">추가할 아이템의 코드</param>
    /// <returns>아이템 추가 성공 여부(true면 인벤토리에 아이템이 추가됨)</returns>
    public bool AddItem(ItemIDCode code)
    {
        return AddItem(GameManager.Inst.ItemData[code]);
    }

    /// <summary>
    /// 아이템 추가하기
    /// </summary>
    /// <param name="data">추가할 아이템의 데이터</param>
    /// <returns>아이템 추가 성공 여부(true면 인벤토리에 아이템이 추가됨)</returns>
    public bool AddItem(ItemData data)
    {
        bool result = false;

        //Debug.Log($"인벤토리에 {data.itemName}을 추가합니다");

        ItemSlot slot = FindEmptySlot();
        if(slot != null)
        {
            slot.AssignSlotItem(data);
            result = true;

            //Debug.Log($"추가 성공.");
        }
        else
        {
            // 모든 슬롯에 아이템이 들어있다.
            //Debug.Log($"인벤토리 가득차 실패.");
        }
        return result;
    }

    public bool AddItem(uint id, uint index)
    {
        return AddItem(GameManager.Inst.ItemData[id], index);
    }

    public bool AddItem(ItemIDCode code, uint index)
    {
        return AddItem(GameManager.Inst.ItemData[code], index);
    }

    public bool AddItem(ItemData data, uint index)
    {
        bool result = false;

        ItemSlot slot = slots[index];
        if(slot.IsEmpty())
        {
            if(slot.SlotItemData.itemType == ItemType.Equipment)
            {
                slot.ItemCount = 0;
            }
            else
            {
                slot.ItemCount += 1;
            }
            slot.AssignSlotItem(data);
            result = true;

            //Debug.Log($"추가 성공.");
        }
        else
        {
            //Debug.Log($"실패 : {index} 슬롯에는 다른 아이템이 들어있습니다.");
        }

        return result;
    }

    // 아이템 버리기(인벤토리 비우기)
    public bool RemoveItem(uint slotIndex)
    {
        bool result = false;

        Debug.Log($"인벤토리 {slotIndex} 슬롯을 비웁니다.");
        if (IsValidSlotIndex(slotIndex))
        {
            ItemSlot slot = slots[slotIndex];
            //Debug.Log($"{slot.SlotItemData.itemName}을 삭제합니다.");
            slot.ClearSlotItem();
            //Debug.Log($"삭제 성공.");
            result = true;
        }
        else
        {
            //Debug.Log($"실패 : 잘못된 인덱스 입니다.");
        }
        return result;
    }

    public bool RemoveItem(ItemSlot itemSlot)
    {
        bool result = false;

        if (IsNotEmptySlot(itemSlot))
        {
            //Debug.Log($"{slot.SlotItemData.itemName}을 삭제합니다.");
            itemSlot.ClearSlotItem();
            //Debug.Log($"삭제 성공.");
            result = true;
        }
        else
        {
        }
        return result;
    }

    public void ClearInventory()
    {
        //Debug.Log($"인벤토리 클리어.");
        foreach (var slot in slots)
        {
            slot.ClearSlotItem();
        }
    }

    /// <summary>
    /// 아이템 이동하기
    /// </summary>
    /// <param name="from">시작 슬롯의 ID</param>
    /// <param name="to">도작 슬롯의 ID</param>
    public void MoveItem(uint from, uint to)
    {
        // from 시작을 한다. 아이템이 있을 수도 있고 없을 수도 있다.
        // to 도착을 한다. to에도 아이템이 있을 수도 있고 없을 수도 있다.

        // 발생 가능한 4가지 경우의 수
            // from에 있고 to에 있고
            // from에 있고 to에 없고
            // from에 없고 to에 있고 -> 뭔가 실행되면 안된다.
            // from에 없고 to에 없고 -> 뭔가 실행되면 안된다.
        if (IsValidAndNotEmptySlot(from) && IsValidSlotIndex(to))
        {
            // from이 valid하고 비어있지 않다. 그리고 to가 valid하다.
            //Debug.Log($"{from}에 있는 {slots[from].SlotItemData.itemName}이 {to}로 이동합니다.");
            tempSlot.AssignSlotItem(slots[from].SlotItemData);
            slots[from].AssignSlotItem(slots[to].SlotItemData);
            slots[to].AssignSlotItem(tempSlot.SlotItemData);
            tempSlot.ClearSlotItem();
        }
        else
        {
            // from이 valid하지 않거나 비어있다 또는 to가 valid하지 않다.
            //Debug.Log($"{from}에서 {to}로 아이템을 옮길 수 없습니다.");
        }
    }

    // 아이템 나누기
    // 아이템 정렬
    // 아이템 사용하기
    // 아이템 장비하기
    // ----------------------------------------------------------------------------------

    // 함수(백엔드) -----------------------------------------------------------------------
    // 적절한 빈 슬롯을 찾아주는 함수
    // 비어있는 슬롯 확인하는 함수
    // 
    // 특정 종류의 아이템이 들어있는 슬롯을 찾아주는 함수

    private ItemSlot FindEmptySlot()
    {
        ItemSlot result = null;

        foreach(var slot in slots)
        {
            if (slot.IsEmpty())
            {
                result = slot;
                break;
            }
        }

        return result;
    }

    private bool IsValidSlotIndex(uint index) => index < SlotCount;

    private bool IsValidAndNotEmptySlot(uint index) => (IsValidSlotIndex(index) && !slots[index].IsEmpty());

    private bool IsNotEmptySlot(ItemSlot itemSlot) => (itemSlot != null);

    public void PrintInventory()
    {
        // 현재 인벤토리 내용을 콘솔창에 출력하는 함수
        // ex) [달걀, 달걀, 달걀, 뼈다귀, 뼈다귀]

        string result = "";
        result += "[";

        for (int i=0; i<slots.Length; i++)
        {
            if(!slots[i].IsEmpty())
                result += slots[i].SlotItemData.itemName;
            else
                result += "빈칸";

            if (i != slots.Length - 1)
                result += ", ";
        }
        result += "]";
        //Debug.Log(result);
    }

    // ----------------------------------------------------------------------------------
}

// ItemData[] -> ItemDataManager로 처리
// 아이템 이미지 -> ItemData에 추가될 내용
// 아이템 종류 -> ItemData에 추가될 내용
