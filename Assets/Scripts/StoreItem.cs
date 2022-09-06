using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem : MonoBehaviour
{
    private void Start()
    {
        Store store = new();

        StoreUI storeUI = FindObjectOfType<StoreUI>();
        storeUI.InitializeInventory(store);

        store.AddItem(ItemIDCode.Potion_HP_Small);
        store.AddItem(ItemIDCode.Potion_HP_Medium);
        store.AddItem(ItemIDCode.Potion_HP_Large);
        store.AddItem(ItemIDCode.Potion_MP_Small);
        store.AddItem(ItemIDCode.Potion_MP_Medium);
        store.AddItem(ItemIDCode.Potion_MP_Large);
    }
}
