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

        ItemFactory.MakeItem(ItemIDCode.Bone, new(1, 0, 0));
        ItemFactory.MakeItem(ItemIDCode.Egg, new(2, 0, 0));
        ItemFactory.MakeItem(ItemIDCode.Coin_Copper, new(3, 0, 0));
        ItemFactory.MakeItem(ItemIDCode.Coin_Sliver, new(4, 0, 0));
        for(int i=0; i<10; i++)
        {
            ItemFactory.MakeItem(ItemIDCode.Coin_Gold, new(5, 0, 0));
        }
        ItemFactory.MakeItem(ItemIDCode.HealingPotion, new(6, 0, 0));
        ItemFactory.MakeItem(ItemIDCode.HealingPotion, new(6, 0, 0));
        ItemFactory.MakeItem(ItemIDCode.HealingPotion, new(6, 0, 0));
        ItemFactory.MakeItem(ItemIDCode.HealingPotion, new(6, 0, 0));
        ItemFactory.MakeItem(ItemIDCode.ManaPotion, new(7, 0, 0));
        ItemFactory.MakeItem(ItemIDCode.ManaPotion, new(8, 0, 0));

        store.AddItem(ItemIDCode.Bone);
        store.AddItem(ItemIDCode.Egg);
        store.AddItem(ItemIDCode.HealingPotion);
        store.AddItem(ItemIDCode.ManaPotion);
    }

    private static void Test_Dummy()
    {
        Inventory inven = new();

        InventoryUI invenUI = FindObjectOfType<InventoryUI>();
        invenUI.InitializeInventory(inven);

        inven.AddItem(ItemIDCode.Bone);
        inven.AddItem(ItemIDCode.Egg);
        inven.AddItem(ItemIDCode.HealingPotion);
        inven.AddItem(ItemIDCode.HealingPotion);
        inven.AddItem(ItemIDCode.HealingPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
    }

    private static void Test_AddRemoveMove()
    {
        Inventory inven = new();
        inven.AddItem(ItemIDCode.Egg);
        inven.AddItem(ItemIDCode.Egg);
        inven.AddItem(ItemIDCode.Egg);
        inven.AddItem(ItemIDCode.Bone);
        inven.AddItem(ItemIDCode.Bone);
        inven.AddItem(ItemIDCode.Bone);
        inven.AddItem(ItemIDCode.Bone);

        inven.RemoveItem(3);
        inven.RemoveItem(10);

        inven.AddItem(ItemIDCode.Egg, 3);
        inven.AddItem(ItemIDCode.Bone, 3);
        inven.PrintInventory();

        inven.ClearInventory();
        inven.PrintInventory();

        inven.AddItem(ItemIDCode.Egg);
        inven.AddItem(ItemIDCode.Egg);
        inven.AddItem(ItemIDCode.Bone);
        inven.PrintInventory();

        inven.MoveItem(1, 4);
        inven.PrintInventory();     //[´Þ°¿,(ºóÄ­),»À,(ºóÄ­),´Þ°¿,(ºóÄ­)]

        inven.MoveItem(0, 5);
        inven.PrintInventory();

        inven.MoveItem(2, 4);
        inven.PrintInventory();
    }
}
