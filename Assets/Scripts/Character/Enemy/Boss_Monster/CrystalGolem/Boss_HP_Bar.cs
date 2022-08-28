using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_HP_Bar : MonoBehaviour
{
    IHealth target;
    Image fill;
    // Enemy_Boss target;
    // Transform fillPivot;

    private void Awake()
    {
        target = GetComponentInParent<IHealth>();
        target.onHealthChange += SetHP_Value;
        fill = transform.Find("Fill").GetComponent<Image>();
        //fillPivot = transform.Find("FillPivot");
        //target = GetComponentInParent<Enemy_Boss>();
    }

    void SetHP_Value()
    {
        if (target != null)
        {
            float ratio = target.HP / target.MaxHP;
            // fillPivot.localScale = new Vector3(ratio, 1, 1);
            fill.fillAmount = ratio;
        }
    }
}
