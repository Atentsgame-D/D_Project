using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startbutton : MonoBehaviour
{
    public GameObject CoverImage;       // ��ŸƮ��

    public void OnClickStartButton()
    {

        CoverImage.SetActive(false);
        LoadingScene.Instance.LoadScene("AllOfTeamProject");
    }
}
