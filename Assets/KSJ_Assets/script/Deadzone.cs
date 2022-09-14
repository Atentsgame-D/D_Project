using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone : MonoBehaviour
{

    private Renderer triggerRenderer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider collision)  //충돌 판정
    {
        Debug.Log("데드 존 물체 충돌 확인");
        if (collision.gameObject.tag == "Player")  //충돌한 것이 플레이어였을 때
        {
            Debug.Log("데드 존 충돌 물체 태그 플레이어 확인");
            GameManager.Inst.MainPlayer.TakeDamage(2000);

        }
    }
}