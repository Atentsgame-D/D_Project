using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    Player player;
    Image image;
    TextMeshProUGUI text;
    Animator animator;


    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        image = transform.GetChild(0).GetComponent<Image>();
        text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
        Initailize();
    }
    public void Initailize()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }
    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }
}
