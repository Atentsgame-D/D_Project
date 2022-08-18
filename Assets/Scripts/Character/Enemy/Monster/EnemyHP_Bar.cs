using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP_Bar : MonoBehaviour
{
    public Transform m_camera;

    private void Update()
    {
        transform.forward = m_camera.forward;
    }
}
