using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCameraBody : MonoBehaviour
{
    public void OnBody(bool OnBody)
    {
        gameObject.SetActive(OnBody);
    }
}
