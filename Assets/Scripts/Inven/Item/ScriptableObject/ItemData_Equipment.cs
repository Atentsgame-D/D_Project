using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 데이터를 저장하는 데이터 파일을 만들게 해주는 스크립트
/// </summary>
[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Scriptable Object/Item Data - Equipment", order = 5)]
public class ItemData_Equipment : ItemData, IEquipItem
{
    [Header("장비 데이터")]
    public float defensePower = 10.0f;
    public EquipmentType slot = EquipmentType.Helmat;

    /// <summary>
    /// 아이템 장비
    /// </summary>
    /// <param name="target">아이템을 장비할 대상</param>
    public void EquipItem(IEquipTarget target)
    {
        //target.EquipWeapon(this);
    }

    /// <summary>
    /// 아이템 장비/해제 토글
    /// </summary>
    /// <param name="target">아이템을 토글할 대상</param>
    public void ToggleEquipItem(IEquipTarget target)
    {
        //if(target.IsWeaponEquiped)
        //{
        //    target.UnEquipWeapon();
        //}
        //else
        //{
        //    target.EquipWeapon(this);
        //}
    }

    /// <summary>
    /// 아이템 해제
    /// </summary>
    /// <param name="target">아이템을 해제할 대상</param>
    public void UnEquipItem(IEquipTarget target)
    {
        //target.UnEquipWeapon();
    }
}
