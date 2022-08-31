using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    // Resources 폴더 프리팹 등록으로 UI 불러오기
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
        return Instantiate(Resources.Load<LoadingScene>("LoadingUI")); // Resources 폴더안에 LoadingUI가 맞으면 불러온다.
    }

    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);                                       // Resources 폴더안에 LoadingUI가 아니면 파괴.
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)                         // 비활성화 되어있는 오브젝트 활성화
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        loadSceneName = sceneName;
        StartCoroutine(LoadSceneProcess());
    }
    private IEnumerator LoadSceneProcess()
    {
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true));                            // 자연스럽게 화면 가리면서 호출

        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName);     // 비동기로 씬을 나타나게 함
        op.allowSceneActivation = false;                                    // 쒼 변환 방지

        float timer = 0f;
        while(!op.isDone)                                                   // 로딩을 반복하게 만듬
        {
            yield return null;                                              // 반복문이 한번 반복될때 마다 유니티 제어권을 넘김

            if(op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;                       // 로딩 진행도 표시
            }
            else
            {
                timer += Time.unscaledDeltaTime;                            
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f,timer);        // 1초에 걸쳐서 진행
                if(progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)              // LoadSceneProcess 끝나면 어떤씬이 나타나는지 알려주는 것
    {
       if(arg0.name == loadSceneName)                                       // 모든씬을 비교하고 같다면 불러온뒤
        {
            StartCoroutine(Fade(false));                                    
            SceneManager.sceneLoaded -= OnSceneLoaded;                      // 제거하지 않으면 온쒼로디드가 반복되면서 에러발생
        }
    }

    private IEnumerator Fade(bool isFadeIn)                     // 씬 로딩 시작과 끝날때 등장하게 만든다.
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
            gameObject.SetActive(false);    // 로딩을 다하면 사라지게 만듬.
        }
    }





    // 씬으로 불러오기

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
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);     // 비동기방식, 씬의 로딩이 끝나면 자동으로 불러온 씬으로 이동할 것인지 설정
        op.allowSceneActivation = false;                                // 로딩 멈추게 하기 false 90% / true 100% Loading

        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;

            if(op.progress <0.9f)
            {
                progressBar.fillAmount = op.progress;                   // 로딩 진행도
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount= Mathf.Lerp(0.9f,1f,timer);      // 90%가 로딩되면 1초에 1퍼씩 차게 설정.
                if(progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation=true;
                    yield break;
                }
            }
        }
    }*/
}
