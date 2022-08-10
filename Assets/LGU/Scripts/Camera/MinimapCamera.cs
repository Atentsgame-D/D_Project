using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, 10, player.transform.position.z);
    }
}
