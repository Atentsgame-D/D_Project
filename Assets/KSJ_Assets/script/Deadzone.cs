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
    private void OnTriggerEnter(Collider collision)  //�浹 ����
    {
        Debug.Log("���� �� ��ü �浹 Ȯ��");
        if (collision.gameObject.tag == "Player")  //�浹�� ���� �÷��̾�� ��
        {
            Debug.Log("���� �� �浹 ��ü �±� �÷��̾� Ȯ��");
            GameManager.Inst.MainPlayer.TakeDamage(2000);

        }
    }
}