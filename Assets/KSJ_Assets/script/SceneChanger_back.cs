using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger_back : MonoBehaviour
{
    private string SceneNum;  //씬 변수
    private Renderer triggerRenderer;
    // Start is called before the first frame update
    void Start()
    {
        SceneNum = SceneManager.GetActiveScene().name;  //현재 씬 확인
        Debug.Log("현재 씬은" + SceneNum + "입니다.");
        triggerRenderer = GetComponent<Renderer>();
    }


    private void OnTriggerEnter(Collider collision)  //충돌 판정
    {
        Debug.Log("물체 충돌 확인");
        if (collision.gameObject.tag == "Player")  //충돌한 것이 플레이어였을 때
        {
            Debug.Log("충돌 물체 태그 플레이어 확인");
            switch (SceneNum)                           //현재 씬 확인 및 다음 씬으로 변수 변환
            {
                case "village_1":
                    SceneNum = "stage_1";
                    break;
                case "stage_2":
                    SceneNum = "village_1";
                    break;
                case "village_2":
                    SceneNum = "stage_2";
                    break;
                case "stage_3":
                    SceneNum = "village_2";
                    break;
                case "village_3":
                    SceneNum = "stage_3";
                    break;
                default:                                        //현재 씬을 판명 불가능할 경우 다음 씬을 스테이지 1로
                    Debug.Log("에러 발생 SceneNum초기화");
                    SceneNum = "stage_1";
                    break;
            }
            Debug.Log("현재 씬을" + SceneNum + "으로 전환합니다.");

            GameManager.Inst.PreHp = GameManager.Inst.MainPlayer.Hp;
            GameManager.Inst.PreMp = GameManager.Inst.MainPlayer.Mp;

            SceneManager.LoadScene(SceneNum);                   //다음 씬으로 이동

        }
    }

}
