using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_LoadingScene : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            LoadingScene.Instance.LoadScene("Boss2");
           Debug.Log("´­·¶µû");
        }
    }
}
