using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Inventory : MonoBehaviour
{
    private void Start()
    {
        //Test_AddRemoveMove();
        //Test_Dummy();

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

    //private static void Test_Dummy()
    //{
    //    Inventory inven = new();

    //    InventoryUI invenUI = FindObjectOfType<InventoryUI>();
    //    invenUI.InitializeInventory(inven);

    //    inven.AddItem(ItemIDCode.Bone);
    //    inven.AddItem(ItemIDCode.Egg);
    //    inven.AddItem(ItemIDCode.HealingPotion);
    //    inven.AddItem(ItemIDCode.HealingPotion);
    //    inven.AddItem(ItemIDCode.HealingPotion);
    //    inven.AddItem(ItemIDCode.ManaPotion);
    //    inven.AddItem(ItemIDCode.ManaPotion);
    //    inven.AddItem(ItemIDCode.ManaPotion);
    //    inven.AddItem(ItemIDCode.ManaPotion);
    //    inven.AddItem(ItemIDCode.ManaPotion);
    //    inven.AddItem(ItemIDCode.ManaPotion);
    //    inven.AddItem(ItemIDCode.ManaPotion);
    //}

    //private static void Test_AddRemoveMove()
    //{
    //    Inventory inven = new();
    //    inven.AddItem(ItemIDCode.Egg);
    //    inven.AddItem(ItemIDCode.Egg);
    //    inven.AddItem(ItemIDCode.Egg);
    //    inven.AddItem(ItemIDCode.Bone);
    //    inven.AddItem(ItemIDCode.Bone);
    //    inven.AddItem(ItemIDCode.Bone);
    //    inven.AddItem(ItemIDCode.Bone);

    //    inven.RemoveItem(3);
    //    inven.RemoveItem(10);

    //    inven.AddItem(ItemIDCode.Egg, 3);
    //    inven.AddItem(ItemIDCode.Bone, 3);
    //    inven.PrintInventory();

    //    inven.ClearInventory();
    //    inven.PrintInventory();

    //    inven.AddItem(ItemIDCode.Egg);
    //    inven.AddItem(ItemIDCode.Egg);
    //    inven.AddItem(ItemIDCode.Bone);
    //    inven.PrintInventory();

    //    inven.MoveItem(1, 4);
    //    inven.PrintInventory();     //[´Þ°¿,(ºóÄ­),»À,(ºóÄ­),´Þ°¿,(ºóÄ­)]

    //    inven.MoveItem(0, 5);
    //    inven.PrintInventory();

    //    inven.MoveItem(2, 4);
    //    inven.PrintInventory();
    //}
}
