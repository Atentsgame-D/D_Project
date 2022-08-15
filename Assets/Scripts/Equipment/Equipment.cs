using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    public const int Default_Equipment_Size = 3;
    private EquipmentSlot[] slots;
    public int SlotCount => slots.Length;
    public EquipmentSlot this[int index] => slots[index];

    public Equipment(int size = Default_Equipment_Size)
    {
        slots = new EquipmentSlot[size];
        for (int i = 0; i < size; i++)
        {
            slots[i] = new EquipmentSlot();
        }
    }

    public bool OnEquipment(ItemData data)
    {
        bool result = false;

        EquipmentSlot empty = FindEquipSlot(data.equipmentType);

        if (empty != null)
        {
            empty.AssignSlotItem(data);
            result = true;
        }
        else
        {
            //Debug.Log("실패 : 인벤토리가 가득차 실패했습니다.");
        }

        return result;
    }

    public void UnEqiupmemt(ItemData data)
    {
        Inventory inven = GameManager.Inst.InvenUI.inven;
        inven.AddItem(data);
    }

    EquipmentSlot FindEquipSlot(EquipmentType equipmentType)
    {
        EquipmentSlot result = null;

        foreach (var slot in slots)
        {
            if (slot.equipmentType == equipmentType)
            {
                if (slot.IsEmpty())
                {
                    result = slot;
                    break;
                }
            }
        }

        return result;
    }


}
