using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMiniMap : MonoBehaviour
{
    public Transform player;
    private void Update()
    {
        transform.position = player.position + new Vector3(0.0f, 10.0f, 0.0f);
    }
}
