using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void ChangeCamera(bool On3rd)
    {
        gameObject.SetActive(On3rd);
    }
}
