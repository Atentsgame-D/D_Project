using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GriffinBoss_HP_Bar : MonoBehaviour
{
    IHealth target;
    Image fill;

    private void Awake()
    {
        // target = GetComponentInParent<Enemy_GriffinBoss>();
        target = GetComponentInParent<IHealth>();
        target.onHealthChange += SetHP_Value;
        fill = transform.Find("Fill").GetComponent<Image>();
    }

    void SetHP_Value()
    {
        if (target != null)
        {
            float ratio = target.HP / target.MaxHP;
            fill.fillAmount = ratio;
        }
    }
}
