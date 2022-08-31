using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Item 관련 --------------------------------------------------------------------------------
    private ItemDataManager itemData;
    public ItemDataManager ItemData
    {
        get => itemData;
    }
    // ------------------------------------------------------------------------------------------

    // Inven 관련 --------------------------------------------------------------------------------
    private InventoryUI inventoryUI;
    public InventoryUI InvenUI => inventoryUI;
    // ------------------------------------------------------------------------------------------

    // Equipment 관련 ---------------------------------------------------------------------------
    private EquipmentUI equipmentUI;
    public EquipmentUI EquipUI => equipmentUI;
    // ------------------------------------------------------------------------------------------

    // Store 관련 --------------------------------------------------------------------------------
    private StoreUI storeUI;
    public StoreUI StoreUI => storeUI;
    // ------------------------------------------------------------------------------------------

<<<<<<< HEAD
    public GameObject CoverImage;       // 스타트쒼
=======
    // 카메라 관련 --------------------------------------------------------------------------------
    private FirstPersonCamera firstPersonCamera;
    public FirstPersonCamera FirstPersonCamera => firstPersonCamera;
    //------------

>>>>>>> e07fac2c8ab9b1f58f75548b44df44a109cf7b65
    private GameObject talkPanel;
    private TextMeshProUGUI talkText;
    private TalkManager talkManager;
    private GameObject scanObject;
    //private GameObject PlayerUI;

    private bool isAction;
    public int talkindex;

    Player player;
    public Player MainPlayer
    {
        get => player;
    }
    static GameManager instance = null;
    public static GameManager Inst
    {
        get => instance;
    }

    public GameObject TalkPanel { get => talkPanel; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.Initialize();
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        talkPanel = GameObject.Find("TalkPanel").gameObject;
        talkText = talkPanel.transform.Find("TalkText").GetComponent<TextMeshProUGUI>();
        talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
        //scanObject = ???;
        //PlayerUI = GameObject.Find("PlayerUI").gameObject;
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
        //    Debug.Log("상호작용");
        //}
        //talkPanel.SetActive(isAction);
        isAction = true;
        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNPC);
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkindex);

        if (talkData == null) // 토크 데이터가 없을때
        {
            isAction = false;
            talkindex = 0;
            return;
        }

        if (isNpc) //대상이 npc일때
        {
            talkText.text = talkData;
            //PlayerUI.SetActive(false);
        }
        else // 상자같은거 일때
        {
            talkText.text = talkData;
            //PlayerUI.SetActive(true);
        }

        isAction = true;
        talkindex++;
    }

    public void OnClickStartButton()
    {
        CoverImage.SetActive(false);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Initialize();
    }

    private void Initialize()
    {
        itemData = GetComponent<ItemDataManager>();

        player = FindObjectOfType<Player>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        storeUI = FindObjectOfType<StoreUI>();    
        equipmentUI = FindObjectOfType<EquipmentUI>();
    }
}
