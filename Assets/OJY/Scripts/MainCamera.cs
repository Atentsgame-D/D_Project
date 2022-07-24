using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target = null;
    public float speed = 3.0f;

    Vector3 offset = Vector3.zero;

    private void Start()
    {
        if (target == null)
        {
            target = FindObjectOfType<Player>().transform;
        }
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        transform.position = target.position + new Vector3(0.0f,7.0f,-5.0f);
    }
}
