using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class MpPostion : MonoBehaviour
{
    int postionCount = 10;
    float mpRecovery = 50.0f;
    TextMeshProUGUI text;
    public Player player;
    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (player.Mp < 50 || (Keyboard.current.digit2Key.wasPressedThisFrame && player.Mp < player.MaxMP))
        {
            player.Mp += mpRecovery;
            postionCount--;
            text.text = postionCount.ToString();
        }
    }
}
