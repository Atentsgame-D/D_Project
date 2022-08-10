using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    Player player;
    ItemDataManager itemData;
    InventoryUI inventoryUI;
    StoreUI storeUI;

    public Player MainPlayer => player;

    public ItemDataManager ItemData => itemData;

    static GameManager instance = null;
    public static GameManager Inst => instance;

    public InventoryUI InvenUI => inventoryUI;

    public StoreUI StoreUI => storeUI;

    public TalkManager talkManager;
    public GameObject talkPanel;
    public TextMeshProUGUI talkText;
    public GameObject scanObject;

    bool isAction;
    public int talkindex;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Initialize();
    }

    private void Initialize()
    {
        player = FindObjectOfType<Player>();
        itemData = GetComponent<ItemDataManager>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        storeUI = FindObjectOfType<StoreUI>();
    }

    public void Action(GameObject scanObj)
    {
        //if (isAction)
        //{
        //    isAction = false;
        //}
        //else
        //{
        //    isAction = true;
        //    scanObject = scanObj;
        //    ObjectData objData = scanObject.GetComponent<ObjectData>();
        //    Talk(objData.id, objData.isNPC);
        //}
        isAction = true;
        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNPC);
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkindex);

        if (talkData == null)
        {
            isAction = false;
            talkindex = 0;
            return;
        }

        if (isNpc)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }

        isAction = true;
        talkindex++;
    }
}


