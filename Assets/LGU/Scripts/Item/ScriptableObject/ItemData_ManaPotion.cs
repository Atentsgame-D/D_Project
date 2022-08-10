using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 데이터를 저장하는 데이터 파일을 만들게 해주는 스크립트
/// </summary>
[CreateAssetMenu(fileName = "New Mana Potion", menuName = "Scriptable Object/Item Data - ManaPotion", order = 4)]
public class ItemData_ManaPotion : ItemData, IUsable    // 내가 원하는 데이터를 저장할 수 있는 데이터 파일을 설계힐 수 있게 해주는 클라스
{
    float manaPoint = 20.0f;

    public void Use(GameObject target = null)
    {
        IMana mana = target.GetComponent<IMana>();
        if (mana != null)
        {
            mana.MP += manaPoint;
            Debug.Log($"{name}아이템 사용으로 {manaPoint}만큼 회복했습니다. 현재 마나 : {GameManager.Inst.MainPlayer.MP}");
        }
    }
}
