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
    Basic_Helmet = 0,
    Beginner_Helmet,
    Intermediate_Helmet,
    luxury_helmet,
    Potion_HP_Small = 11,
    Potion_HP_Medium,
    Potion_HP_Large,
    Potion_MP_Small = 21,
    Potion_MP_Medium,
    Potion_MP_Large
}

public enum ItemType
{
    Consumable = 0,
    Equipment,
    Money
}

public enum SlotType
{
    Inventory = 0,
    Store
}
