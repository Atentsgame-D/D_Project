using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GriffinBoss_HP_Bar : MonoBehaviour
{
    Enemy_GriffinBoss target;
    Transform fillPivot;

    private void Awake()
    {
        target = GetComponentInParent<Enemy_GriffinBoss>();
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
