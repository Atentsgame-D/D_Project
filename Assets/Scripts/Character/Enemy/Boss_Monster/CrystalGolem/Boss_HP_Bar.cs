using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_HP_Bar : MonoBehaviour
{
    Enemy_Boss target;
    Transform fillPivot;

    private void Awake()
    {
        target = GetComponentInParent<Enemy_Boss>();
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
