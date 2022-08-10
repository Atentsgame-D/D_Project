using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �����͸� �����ϴ� ������ ������ ����� ���ִ� ��ũ��Ʈ
/// </summary>
[CreateAssetMenu(fileName = "New Mana Potion", menuName = "Scriptable Object/Item Data - ManaPotion", order = 4)]
public class ItemData_ManaPotion : ItemData, IUsable    // ���� ���ϴ� �����͸� ������ �� �ִ� ������ ������ ������ �� �ְ� ���ִ� Ŭ��
{
    float manaPoint = 20.0f;

    public void Use(GameObject target = null)
    {
        IMana mana = target.GetComponent<IMana>();
        if (mana != null)
        {
            mana.MP += manaPoint;
            Debug.Log($"{name}������ ������� {manaPoint}��ŭ ȸ���߽��ϴ�. ���� ���� : {GameManager.Inst.MainPlayer.MP}");
        }
    }
}
