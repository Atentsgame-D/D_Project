using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class HpPotion : MonoBehaviour
{
    int potionCount = 10;
    float hpRecovery = 100.0f;
    TextMeshProUGUI text;
    Player player;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (potionCount > 0)
        {
            if (Keyboard.current.digit5Key.wasPressedThisFrame && player.Hp < player.MaxHP)
            {
                player.Hp += hpRecovery;
                potionCount--;
                text.text = potionCount.ToString();
            }
        }

    }
}
