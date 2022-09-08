using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamera : MonoBehaviour
{
    Vector3 PlayerPos = GameManager.Inst.MainPlayer.transform.position;
    
    private void Start()
    {
        transform.position = PlayerPos + new Vector3(0,10,-5.0f);
    }
}
