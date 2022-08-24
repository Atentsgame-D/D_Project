using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 데이터를 저장하는 데이터 파일을 만들게 해주는 스크립트
/// </summary>
[CreateAssetMenu(fileName = "New Healing Potion", menuName = "Scriptable Object/Item Data - HealingPotion", order = 3)]
public class ItemData_HealingPotion : ItemData, IUsable
{
    [Header("힐링포션 데이터")]
    public PotionType PotionType = PotionType.Heal;
    public float healPoint = 20.0f;

    public void Use(Player target = null)
    {
        if(target != null && GameManager.Inst.MainPlayer.Hp < GameManager.Inst.MainPlayer.MaxHP)
        {
            target.Hp += healPoint;
            Debug.Log($"{itemName}을 사용했습니다. HP가 {healPoint}만큼 회복됩니다. 현재 HP는 {target.Hp}입니다.");
        }
    }
}
