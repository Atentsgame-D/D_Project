using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    private void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕? 처음보는 얼굴인데.", "뭐 찾는 물건이라도 있어?" });

        talkData.Add(2000, new string[] { "이 앞은 통행증 없이는 지나가실 수 없습니다." });

        talkData.Add(100, new string[] { "평범한 나무상자다." });
    }

    public string GetTalk(int id, int talkindex)
    {
        if (talkindex == talkData[id].Length)
        {
            return null;
        }
        else
            return talkData[id][talkindex];
    }
}
