using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipTarget
{
    ItemSlot EquipItemSlot { get; }          // 장비한 아이템(무기)

    void EquipWeapon(ItemSlot weaponSlot);
    void UnEquipWeapon();
}
