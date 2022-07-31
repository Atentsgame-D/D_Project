using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System;

// ��� ��ų�� ��Ÿ�� ���� ���� ����
public class SkillCoolTimeManager : MonoBehaviour
{
    //����
    float skill1_originCool = 10.0f;
    public float skill1_CoolTime = 0.0f;

    //������
    float skill2_originCool = 10.0f;
    public float skill2_CoolTime = 0.0f;

    //����
    float skill3_originCool = 15.0f;
    public float skill3_CoolTime = 0.0f;

    //����
    float skill4_originCool = 10.0f;
    public float skill4_CoolTime = 0.0f;

    private void Update()
    {
        coolDown();
    }

    //��ų ��Ÿ���� 0�ʰ� �ƴϸ� 1�ʾ� ����
    private void coolDown()
    {
        if (!(skill1_CoolTime <= 0))
        {
            skill1_CoolTime -= Time.deltaTime;
        }

        if (!(skill2_CoolTime <= 0))
        {
            skill2_CoolTime -= Time.deltaTime;
        }

        if (!(skill3_CoolTime <= 0))
        {
            skill3_CoolTime -= Time.deltaTime;
        }

        if (!(skill4_CoolTime <= 0))
        {
            skill4_CoolTime -= Time.deltaTime;
        }
    }
    // ��Ÿ�� ��ŸƮ
    public void skill1()
    {
        skill1_CoolTime = skill1_originCool;
    }
    public void skill2()
    {
        skill2_CoolTime = skill2_originCool;
    }
    public void skill3()
    {
        skill3_CoolTime = skill3_originCool;
    }
    public void skill4()
    {
        skill4_CoolTime = skill4_originCool;
    }

    // ���� ��Ÿ�� / �� ��Ÿ�� ����
    public float CoolTimeRate01()
    {
        float rate;
        rate = skill1_CoolTime / skill1_originCool;
        return rate;
    }
    public float CoolTimeRate02()
    {
        float rate;
        rate = skill2_CoolTime / skill2_originCool;
        return rate;
    }
    public float CoolTimeRate03()
    {
        float rate;
        rate = skill3_CoolTime / skill3_originCool;
        return rate;
    }
    public float CoolTimeRate04()
    {
        float rate;
        rate = skill4_CoolTime / skill4_originCool;
        return rate;
    }
}
