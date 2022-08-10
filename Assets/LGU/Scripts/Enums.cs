using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Coin_Copper = 0,
    Coin_Sliver,
    Coin_Gold,
    Egg,
    Bone,
    HealingPotion,
    ManaPotion
}
