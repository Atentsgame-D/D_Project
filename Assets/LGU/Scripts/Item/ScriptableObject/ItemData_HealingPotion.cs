using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �����͸� �����ϴ� ������ ������ ����� ���ִ� ��ũ��Ʈ
/// </summary>
[CreateAssetMenu(fileName = "New Healing Potion", menuName = "Scriptable Object/Item Data - HealingPotion", order = 3)]
public class ItemData_HealingPotion : ItemData, IUsable    // ���� ���ϴ� �����͸� ������ �� �ִ� ������ ������ ������ �� �ְ� ���ִ� Ŭ��
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
