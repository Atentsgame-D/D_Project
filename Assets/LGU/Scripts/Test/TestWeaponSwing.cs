using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestWeaponSwing : MonoBehaviour
{
    // polling : �����͸� ��ܿ��� ��, ��� �������� ��ȭ�� Ȯ���ϴٰ� ���ϴ� ���°� �Ǹ� ��ܿ��� ó��.

    //bool movingStart = false;
    //float speed = 180.0f;
    //float angle = 0.0f;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Space");
            anim.SetTrigger("Swing");
            //movingStart = true;
        }

        //if (movingStart)
        //{
        //    angle += Time.deltaTime * speed;
        //    if (angle > 360.0f)
        //    {
        //        movingStart = false;
        //        angle = 0.0f;
        //    }
        //    transform.rotation = Quaternion.Euler(0, angle, 0);
        //}

        
    }
}
