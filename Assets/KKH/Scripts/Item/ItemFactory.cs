using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 생성용 클래스(생성만)
/// </summary>
public class ItemFactory
{
    static int itemCount = 0;

    /// <summary>
    /// 아이템 생성
    /// </summary>
    /// <param name="code">생성할 아이템의 종류</param>
    /// <returns>생성한 게임오브젝트</returns>
    public static GameObject MakeItem(ItemIDCode code)
    {
        GameObject obj = new GameObject();              // 빈 오브젝트 만들기
        Item item = obj.AddComponent<Item>();           // Item 컴포넌트 추가

        item.data = GameManager.Inst.ItemData[code];    // ItemData 설정
        string[] itemName = item.data.name.Split("_");  // 내가 생성하는 종류에 맞게 이름 변경
        obj.name = $"{itemName[1]}_{itemCount}";        // 고유 아이디 추가
        obj.layer = LayerMask.NameToLayer("Item");      // 레이어 설정
        SphereCollider col = obj.AddComponent<SphereCollider>();
        col.radius = 0.5f;
        col.isTrigger = true;
        itemCount++;                                    // 생성할 때마다 값을 증가시켜서 중복이 없도록 처리

        GameObject indicator = Resources.Load<GameObject>("MinimapIndicator_Item");
        GameObject.Instantiate(indicator, obj.transform.position, obj.transform.rotation * Quaternion.Euler(90, 0, 0), obj.transform);

        return obj;                                    // 생성완료된 데이터 리턴
    }

    public static GameObject MakeItem(ItemIDCode code, Vector3 position, bool randomNoise = false)
    {
        GameObject obj = MakeItem(code);
        if(randomNoise)
        {
            Vector2 noise = Random.insideUnitCircle * 0.5f;
            position.x += noise.x;
            position.z += noise.y;
        }

        obj.transform.position = position;

        return obj;
    }
}
