using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 데이터를 저장하는 데이터 파일을 만들게 해주는 스크립트
/// </summary>
[CreateAssetMenu(fileName = "New Healing Potion", menuName = "Scriptable Object/Item Data - HealingPotion", order = 3)]
public class ItemData_HealingPotion : ItemData, IUsable    // 내가 원하는 데이터를 저장할 수 있는 데이터 파일을 설계힐 수 있게 해주는 클라스
{
    float healPoint = 20.0f;

    public void Use(GameObject target = null)
    {
        IHealth health = target.GetComponent<IHealth>();
        if( health != null)
        {
            health.HP += healPoint;
        }
    }
}
