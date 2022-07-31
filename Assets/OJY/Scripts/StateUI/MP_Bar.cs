using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MP_Bar : MonoBehaviour
{
    Player player;
    float mpRate = 0.0f;
    [Tooltip("1초당 회복량")]
    public float mpRecovery = 0.5f;

    TextMeshProUGUI text;
    Transform MpBarRate;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        text = transform.Find("MpBar").GetComponent<TextMeshProUGUI>();
        MpBarRate = transform.Find("CurrentMP").GetComponent<Transform>();
    }
    private void Start()
    {
        player.OnMpChange += mpBarReset;
    }
    private void Update()
    {
        if (player.Mp < player.MaxMP) //매초 마나 자동 회복
        {
            player.Mp += mpRecovery * Time.deltaTime;
        }
    }
    private void mpBarReset(float Mp)
    {
        // 마나 표시
        text.text = $"{Mp:0}/{player.MaxMP}"; 

        // 비율에 맞춰 스케일 조정
        mpRate = player.Mp / player.MaxMP;    
        MpBarRate.localScale = new(mpRate, 1, 1);
    }
}
