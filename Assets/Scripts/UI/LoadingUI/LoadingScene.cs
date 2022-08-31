using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    // Resources ���� ������ ������� UI �ҷ�����
    private static LoadingScene instance;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Image progressBar;

    private string loadSceneName;

    public static LoadingScene Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<LoadingScene>();
                if(obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }
            return instance;
        }
    }

    

    private static LoadingScene Create()
    {
        return Instantiate(Resources.Load<LoadingScene>("LoadingUI")); // Resources �����ȿ� LoadingUI�� ������ �ҷ��´�.
    }

    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);                                       // Resources �����ȿ� LoadingUI�� �ƴϸ� �ı�.
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)                         // ��Ȱ��ȭ �Ǿ��ִ� ������Ʈ Ȱ��ȭ
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        loadSceneName = sceneName;
        StartCoroutine(LoadSceneProcess());
    }
    private IEnumerator LoadSceneProcess()
    {
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true));                            // �ڿ������� ȭ�� �����鼭 ȣ��

        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName);     // �񵿱�� ���� ��Ÿ���� ��
        op.allowSceneActivation = false;                                    // �� ��ȯ ����

        float timer = 0f;
        while(!op.isDone)                                                   // �ε��� �ݺ��ϰ� ����
        {
            yield return null;                                              // �ݺ����� �ѹ� �ݺ��ɶ� ���� ����Ƽ ������� �ѱ�

            if(op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;                       // �ε� ���൵ ǥ��
            }
            else
            {
                timer += Time.unscaledDeltaTime;                            
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f,timer);        // 1�ʿ� ���ļ� ����
                if(progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)              // LoadSceneProcess ������ ����� ��Ÿ������ �˷��ִ� ��
    {
       if(arg0.name == loadSceneName)                                       // ������ ���ϰ� ���ٸ� �ҷ��µ�
        {
            StartCoroutine(Fade(false));                                    
            SceneManager.sceneLoaded -= OnSceneLoaded;                      // �������� ������ �¾��ε�尡 �ݺ��Ǹ鼭 �����߻�
        }
    }

    private IEnumerator Fade(bool isFadeIn)                     // �� �ε� ���۰� ������ �����ϰ� �����.
    {
        float timer = 0f;
        while(timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 3f;
            canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0f,1f, timer) : Mathf.Lerp(1f,0f, timer);
        }
        if(!isFadeIn)
        {
            gameObject.SetActive(false);    // �ε��� ���ϸ� ������� ����.
        }
    }





    // ������ �ҷ�����

    /*static string nextScene;

    [SerializeField]
    Image progressBar;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    void Start()
    {
        StartCoroutine(LoadingSceneProcess());        
    }

    IEnumerator LoadingSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);     // �񵿱���, ���� �ε��� ������ �ڵ����� �ҷ��� ������ �̵��� ������ ����
        op.allowSceneActivation = false;                                // �ε� ���߰� �ϱ� false 90% / true 100% Loading

        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;

            if(op.progress <0.9f)
            {
                progressBar.fillAmount = op.progress;                   // �ε� ���൵
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount= Mathf.Lerp(0.9f,1f,timer);      // 90%�� �ε��Ǹ� 1�ʿ� 1�۾� ���� ����.
                if(progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation=true;
                    yield break;
                }
            }
        }
    }*/
}
