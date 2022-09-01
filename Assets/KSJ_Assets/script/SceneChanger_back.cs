using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger_back : MonoBehaviour
{
    private string SceneNum;  //�� ����
    private Renderer triggerRenderer;
    // Start is called before the first frame update
    void Start()
    {
        SceneNum = SceneManager.GetActiveScene().name;  //���� �� Ȯ��
        Debug.Log("���� ����" + SceneNum + "�Դϴ�.");
        triggerRenderer = GetComponent<Renderer>();
    }


    private void OnTriggerEnter(Collider collision)  //�浹 ����
    {
        Debug.Log("��ü �浹 Ȯ��");
        if (collision.gameObject.tag == "Player")  //�浹�� ���� �÷��̾�� ��
        {
            Debug.Log("�浹 ��ü �±� �÷��̾� Ȯ��");
            switch (SceneNum)                           //���� �� Ȯ�� �� ���� ������ ���� ��ȯ
            {
                case "village_water":
                    SceneNum = "stage_1";
                    break;
                case "stage_2_old":
                    SceneNum = "village_water";
                    break;
                case "village_small":
                    SceneNum = "stage_2_old";
                    break;
                case "stage_3":
                    SceneNum = "village_small";
                    break;
                case "village":
                    SceneNum = "stage_3";
                    break;
                default:                                        //���� ���� �Ǹ� �Ұ����� ��� ���� ���� �������� 1��
                    Debug.Log("���� �߻� SceneNum�ʱ�ȭ");
                    SceneNum = "stage_1";
                    break;
            }
            Debug.Log("���� ����" + SceneNum + "���� ��ȯ�մϴ�.");
            SceneManager.LoadScene(SceneNum);                   //���� ������ �̵�

        }
    }

}
