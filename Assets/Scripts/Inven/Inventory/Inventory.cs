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

    // TempSlot의 ID
    public const uint TempSlotID = 99999;
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
        ItemSlot target = FindSameItem(data);
        if (target != null)
        {
            target.IncreaseSlotItem();
            result = true;
        }
        else
        {
            ItemSlot slot = FindEmptySlot();
            if (slot != null)
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

        //Debug.Log($"인벤토리의 {index} 슬롯에  {data.itemName}을 추가합니다");
        ItemSlot slot = slots[index];   // index번째의 슬롯 가져오기

        if (slot.IsEmpty())              // 찾은 슬롯이 비었는지 확인
        {
            slot.AssignSlotItem(data);  // 비어있으면 아이템 추가
            result = true;
            //Debug.Log($"추가에 성공했습니다.");
        }
        else
        {
            if (slot.SlotItemData == data)  // 같은 종류의 아이템인가?
            {
                if (slot.IncreaseSlotItem() == 0)  // 들어갈 자리가 있는가?
                {
                    result = true;
                    //Debug.Log($"아이템 갯수 증가에 성공했습니다.");
                }
                else
                {
                    //Debug.Log($"실패 : 슬롯이 가득 찼습니다.");
                }
            }
            else
            {
                //Debug.Log($"실패 : {index} 슬롯에는 다른 아이템이 들어있습니다.");
            }
        }

        return result;
    }

    // 아이템 버리기(인벤토리 비우기)
    public bool RemoveItem(uint slotIndex, uint decreaseCount = 1)
    {
        bool result = false;

        //Debug.Log($"인벤토리 {slotIndex} 슬롯을 비웁니다.");
        if (IsValidSlotIndex(slotIndex))
        {
            ItemSlot slot = slots[slotIndex];
            //Debug.Log($"{slot.SlotItemData.itemName}을 삭제합니다.");
            slot.DecreaseSlotItem(decreaseCount);
            //Debug.Log($"삭제 성공.");
            result = true;
        }
        else
        {
            //Debug.Log($"실패 : 잘못된 인덱스 입니다.");
        }
        return result;
    }

    /// <summary>
    /// 특정 슬롯의 아이템을 모두 버리는 함수
    /// </summary>
    /// <param name="slotIndex">아이템을 버릴 슬롯의 인덱스</param>
    /// <returns>버리는데 성공하면 true, 아니면 false</returns>
    public bool ClearItem(uint slotIndex)
    {
        bool result = false;

        //Debug.Log($"인벤토리에서 {slotIndex} 슬롯을 비웁니다.");
        if (IsValidSlotIndex(slotIndex))        // slotIndex가 적절한 범위인지 확인
        {
            ItemSlot slot = slots[slotIndex];
            //Debug.Log($"{slot.SlotItemData.itemName}을 삭제합니다.");
            slot.ClearSlotItem();               // 적절한 슬롯이면 삭제 처리
            //Debug.Log($"삭제에 성공했습니다.");
            result = true;
        }
        else
        {
            //Debug.Log($"실패 : 잘못된 인덱스입니다.");
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
            ItemSlot fromSlot = null;
            ItemSlot toSlot = null;

            if (from == TempSlotID)
            {
                fromSlot = TempSlot;
            }
            else
            {
                fromSlot = slots[from];
            }
            if (to == TempSlotID)
            {
                toSlot = TempSlot;
            }
            else
            {
                toSlot = slots[to];
            }

            if (fromSlot.SlotItemData == toSlot.SlotItemData)
            {
                uint overCount = toSlot.IncreaseSlotItem(fromSlot.ItemCount);
                fromSlot.DecreaseSlotItem(fromSlot.ItemCount - overCount);
            }
            else
            {
                ItemData tempItemData = toSlot.SlotItemData;
                uint tempItemCount = toSlot.ItemCount;
                toSlot.AssignSlotItem(fromSlot.SlotItemData, fromSlot.ItemCount);
                fromSlot.AssignSlotItem(tempItemData, tempItemCount);
            }
        }
    }

    // 아이템 나누기
    public void TempRemoveItem(uint from, uint count = 1)
    {
        if (IsValidAndNotEmptySlot(from))
        {
            ItemSlot slot = slots[from];
            tempSlot.AssignSlotItem(slot.SlotItemData, count);
            slot.DecreaseSlotItem(count);
        }
    }


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

    /// <summary>
    /// 같은 종류의 아이템이 들어있고 슬롯에 여유도 있는 슬롯을 찾아주는 함수
    /// </summary>
    /// <param name="itemData">찾을 아이템</param>
    /// <returns>찾을 아이템이 들어있는 슬롯</returns>
    private ItemSlot FindSameItem(ItemData itemData)
    {
        ItemSlot slot = null;
        for (int i = 0; i < SlotCount; i++)
        {
            if (slots[i].SlotItemData == itemData && slots[i].ItemCount < slots[i].SlotItemData.maxStackCount)
            {
                slot = slots[i];
                break;      // 찾으면 break로 종료
            }
        }
        return slot;
    }

    private bool IsValidSlotIndex(uint index) => (index < SlotCount) || (index == TempSlotID);

    private bool IsValidAndNotEmptySlot(uint index)
    {
        ItemSlot testSlot = null;
        if (index != TempSlotID)
        {
            testSlot = slots[index];
        }
        else
        {
            testSlot = TempSlot;
        }

        return (IsValidSlotIndex(index) && !testSlot.IsEmpty());
    }

    public void PrintInventory()
    {
        // 현재 인벤토리 내용을 콘솔창에 출력하는 함수
        // ex) [달걀,달걀,달걀,(빈칸),뼈다귀,뼈다귀]

        string printText = "[";
        for (int i = 0; i < SlotCount - 1; i++)         // 슬롯이 전체6개일 경우 0~4까지만 일단 추가(5개추가)
        {
            if (slots[i].SlotItemData != null)
            {
                printText += $"{slots[i].SlotItemData.itemName}({slots[i].ItemCount})";
            }
            else
            {
                printText += "(빈칸)";
            }
            printText += ",";
        }
        ItemSlot slot = slots[SlotCount - 1];   // 마지막 슬롯만 따로 처리
        if (!slot.IsEmpty())
        {
            printText += $"{slot.SlotItemData.itemName}({slot.ItemCount})]";
        }
        else
        {
            printText += "(빈칸)]";
        }

        //string.Join(',', 문자열 배열);
        Debug.Log(printText);
    }
    // ----------------------------------------------------------------------------------
}

// ItemData[] -> ItemDataManager로 처리
// 아이템 이미지 -> ItemData에 추가될 내용
// 아이템 종류 -> ItemData에 추가될 내용
