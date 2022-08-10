using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMPBar : MonoBehaviour
{
    IMana target;
    Slider fill;

    private void Awake()
    {
        target = GameObject.Find("Player").GetComponent<IMana>();
        target.onManaChange += SetMP_Value;
        fill = GetComponent<Slider>();
    }

    void SetMP_Value()
    {
        if (target != null)
        {
            float ratio = target.MP / target.MaxMP;
            fill.value = ratio;
        }
    }
}
