using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startbutton : MonoBehaviour
{
    public GameObject CoverImage;       // ½ºÅ¸Æ®¾°

    public void OnClickStartButton()
    {

        CoverImage.SetActive(false);
        LoadingScene.Instance.LoadScene("AllOfTeamProject");
    }
}
