using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject hitEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.isTrigger == false)
        {
            IBattle battle =  other.GetComponent<IBattle>();
            if(battle != null)
            {
                GameManager.Inst.MainPlayer.Attack(battle);

                Vector3 hitPoint = transform.position + transform.up;
                Vector3 effectPoint = other.ClosestPoint(hitPoint);
                Instantiate(hitEffect, effectPoint, Quaternion.identity);
            }
        }
    }
}
