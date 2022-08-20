using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardBoss_HP_Bar : MonoBehaviour
{
    Enemy_LizardBoss target;
    Transform fillPivot;

    private void Awake()
    {
        target = GetComponentInParent<Enemy_LizardBoss>();
        target.onHealthChange += SetHP_Value;
        fillPivot = transform.Find("FillPivot");
    }

    void SetHP_Value()
    {
        if (target != null)
        {
            float ratio = target.HP / target.MaxHP;
            fillPivot.localScale = new Vector3(ratio, 1, 1);
        }
    }
}
