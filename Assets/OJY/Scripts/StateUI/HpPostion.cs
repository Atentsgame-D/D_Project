using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class HpPostion : MonoBehaviour
{
    int postionCount = 10;
    float hpRecovery = 100.0f;
    TextMeshProUGUI text;
    public Player player;
    private void Awake()
    {        
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        if(player.Hp < 50 || (Keyboard.current.digit1Key.wasPressedThisFrame && player.Hp < player.MaxHP))
        {
            player.Hp += hpRecovery;
            postionCount--;
            text.text = postionCount.ToString();
        }
    }
}
