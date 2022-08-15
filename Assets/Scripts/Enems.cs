using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boss_EnemyState
{
    Idle = 0,
    Chase,
    Attack,
    Dead
}

public enum EnemyState
{
    Idle = 0,
    Patrol,
    Chase,
    Attack,
    Dead
}

public enum ItemIDCode
{
    Test_Item = 0,
    luxury_helmet,
    Potion_HP_Small = 11,
    Potion_HP_Medium,
    Potion_HP_Large,
    Potion_MP_Small = 21,
    Potion_MP_Medium,
    Potion_MP_Large,

    Weapon_Wooden_Sword = 31,

    Equipment_Leather_Helmet = 34,

    Equipment_Leather_Boot = 37,
}

public enum ItemType
{
    Consumable = 0,
    Weapon,
    Equipment,
    Money
}

public enum SlotType
{
    Inventory = 0,
    Store
}

public enum EquipmentType
{
    Weapon = 0,
    Helmat,
    Shoes
}
