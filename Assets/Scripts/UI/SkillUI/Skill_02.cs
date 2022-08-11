using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Skill_02 : MonoBehaviour
{
    SkillCoolTimeManager skillCoolTime;
    TextMeshProUGUI coolTimeText;
    Image coolTimeImage;

    private void Awake()
    {
        skillCoolTime = GetComponentInParent<SkillCoolTimeManager>();
        coolTimeImage = transform.GetChild(0).GetComponentInChildren<Image>();
        coolTimeText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        coolTimeImage.transform.localScale = new(1.0f, skillCoolTime.CoolTimeRate02(), 1.0f);
        if (skillCoolTime.skill2_CoolTime > 1.0f)
        {
            coolTimeText.text = $"{skillCoolTime.skill2_CoolTime.ToString("F0")}";
        }
        else if (skillCoolTime.skill2_CoolTime > 0.0f)
        {
            coolTimeText.text = $"{skillCoolTime.skill2_CoolTime.ToString("F1")}";
        }
        else
        {
            coolTimeText.text = "";
        }
    }
}
