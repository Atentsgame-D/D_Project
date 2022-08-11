using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HP_Bar : MonoBehaviour
{
    Player player;
    float hpRate = 0.0f;

    TextMeshProUGUI text;
    Transform HpBarRate;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        text = transform.Find("HpBar").GetComponent<TextMeshProUGUI>();
        HpBarRate = transform.Find("CurrentHP").GetComponent<Transform>();
    }
    private void Start()
    {
        player.OnHpChange += hpBarReset;
    }
    private void hpBarReset(float Hp)
    {
        text.text = $"{Hp:0}/{player.MaxHP}";
        hpRate = player.Hp / player.MaxHP;
        HpBarRate.localScale = new(hpRate,1,1);
    }
}
