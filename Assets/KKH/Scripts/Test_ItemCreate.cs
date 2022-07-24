using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ItemCreate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = ItemFactory.MakeItem(ItemIDCode.Potion_HP_Small, transform.position, true);

    }
}
