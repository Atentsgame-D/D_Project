using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 데이터를 저장하는 데이터 파일을 만들게 해주는 스크립트
/// </summary>
[CreateAssetMenu(fileName = "New Mana Potion", menuName = "Scriptable Object/Item Data - ManaPotion", order = 4)]
public class ItemData_ManaPotion : ItemData, IUsable
{
    [Header("마나포션 데이터")]
    public PotionType PotionType = PotionType.Mana;
    public float manaPoint = 20.0f;
    public bool Use(Player target = null)
    {
        if (target != null)
        {
            if (target.Mp < target.MaxMP)
            {
                target.Mp += manaPoint;
                Debug.Log($"{itemName}을 사용했습니다. MP가 {manaPoint}만큼 회복됩니다. 현재 MP는 {target.Mp}입니다.");
                return true;
            }
        }

        return false;
    }
}
