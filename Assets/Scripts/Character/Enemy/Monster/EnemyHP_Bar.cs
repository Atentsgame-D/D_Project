using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP_Bar : MonoBehaviour
{
    Transform cameraTarget;

    private void Awake()
    {
        cameraTarget = GameObject.Find("Main Camera").GetComponent<Transform>();
    }
    private void Update()
    {
        transform.forward = cameraTarget.forward;
    }
}
