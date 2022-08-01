using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // public ---------------------------------------------------------------------------------
    // ItemSlotUI가 있는 프리팹
    public GameObject slotPrefab;   // 초기화시 새로 생성해야할 경우 사용
                                    // ----------------------------------------------------------------------------------------

    // 기본 데이터 -----------------------------------------------------------------------------
    private Player player;          // 이 인벤토리를 사용하는 플레이어
    private Inventory inven;        // 이 클래스로 표현하려는 인벤토리

    private Transform slotParent;   // 슬롯 생성시 부모가 될 게임 오브젝트의 트랜스폼

    private ItemSlotUI[] slotUIs;   // 이 인벤토리가 가지고 있는 슬롯UI들

    private CanvasGroup canvasGroup;
    // ----------------------------------------------------------------------------------------

    // Item 데이터 -----------------------------------------------------------------------------
    private const uint InvalideID = uint.MaxValue;
    private uint dragStartID;               // 드래그가 시작된 슬롯의 ID

    private TempItemSlotUI tempItemSlotUI;  // 임시 슬롯(아이템 드래그나 아이템 분리할 때 사용)
    public TempItemSlotUI TempSlotUI { get => tempItemSlotUI; }
    // ----------------------------------------------------------------------------------------

    // 상세 정보 UI ----------------------------------------------------------------------------
    private DetailInfoUI detail;
    public DetailInfoUI Detail => detail;
    // ----------------------------------------------------------------------------------------

    // 아이템 분할 UI --------------------------------------------------------------------------
    private ItemSpliterUI itemSpliterUI;
    public ItemSpliterUI SpliterUI => itemSpliterUI;
    // ----------------------------------------------------------------------------------------

    // 돈 데이터 -------------------------------------------------------------------------------
    private TextMeshProUGUI goldText;   // 돈 표시용 Text
    // ----------------------------------------------------------------------------------------

    // 델리게이트 ------------------------------------------------------------------------------
    public System.Action OnInventoryOpen;
    public System.Action OnInventoryClose;
    // ----------------------------------------------------------------------------------------

    // 유니티 이벤트 함수들 ---------------------------------------------------------------------
    private void Awake()
    {
        // 미리 찾아놓기
        canvasGroup = GetComponent<CanvasGroup>();
        goldText = transform.Find("MoneyPanel").Find("Money").GetComponent<TextMeshProUGUI>();
        slotParent = transform.Find("Inventory_Base").Find("Grid Setting");
        tempItemSlotUI = GetComponentInChildren<TempItemSlotUI>();
        detail = GetComponentInChildren<DetailInfoUI>();
        itemSpliterUI = GetComponentInChildren<ItemSpliterUI>();

        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(Close);
    }

    private void Start()
    {
        player = GameManager.Inst.MainPlayer;   // 게임 매니저에서 플레이어 가져오기
        player.OnMoneyChange += RefreshMoney;   // 플레이어의 Money가 변경되고 실행되는 델리게이트에 함수 등록
        RefreshMoney(player.Money);             // 첫 갱신

        Close();
    }
    // ----------------------------------------------------------------------------------------


    // 일반 함수들 -----------------------------------------------------------------------------
    /// <summary>
    /// 인벤토리를 입력받아 UI를 초기화하는 함수
    /// </summary>
    /// <param name="newInven">이 UI로 표시할 인벤토리</param>
    public void InitializeInventory(Inventory newInven)
    {
        inven = newInven;
        if (Inventory.Default_Inventory_Size != newInven.SlotCount)     // 기본 사이즈와 다르면 기본 슬롯UI 삭제
        {
            // 기존 슬롯 전부 삭제
            ItemSlotUI[] slots = GetComponentsInChildren<ItemSlotUI>();
            foreach (var slot in slots)
            {
                Destroy(slot.gameObject);
            }

            // 새로 만들기
            slotUIs = new ItemSlotUI[inven.SlotCount];
            for (int i = 0; i < inven.SlotCount; i++)
            {
                GameObject obj = Instantiate(slotPrefab, slotParent);
                obj.name = $"{slotPrefab.name}_{i}";            // 이름 지어주고
                slotUIs[i] = obj.GetComponent<ItemSlotUI>();
                slotUIs[i].Initialize((uint)i, inven[i]);       // 각 슬롯들도 초기화
            }
        }
        else
        {
            // 크기가 같을 경우 초기화만 진행
            slotUIs = slotParent.GetComponentsInChildren<ItemSlotUI>();
            for (int i = 0; i < inven.SlotCount; i++)
            {
                slotUIs[i].Initialize((uint)i, inven[i]);
            }
        }

        tempItemSlotUI.Initialize(Inventory.TempSlotID, inven.TempSlot);
        tempItemSlotUI.Close();

        itemSpliterUI.Initialize();
        itemSpliterUI.OnOkClick += OnSpliteOk;

        RefreshAllSlots();  // 전체 슬롯 UI 갱신
    }

    private void OnSpliteOk(uint slotID, uint count)
    {
        inven.TempRemoveItem(slotID, count);
        tempItemSlotUI.Open();
    }

    /// <summary>
    /// 모든 슬롯의 Icon이미지 갱신
    /// </summary>
    private void RefreshAllSlots()
    {
        foreach (var slotUI in slotUIs)
        {
            slotUI.Refresh();
        }
    }

    /// <summary>
    /// 플레이어가 가진 돈을 갱신
    /// </summary>
    /// <param name="money">표시될 금액</param>
    private void RefreshMoney(int money)
    {
        goldText.text = money.ToString("N0");   // Money가 변경될때 실행
    }

    public void InventoryOnOffSwitch()
    {
        if (canvasGroup.blocksRaycasts)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    private void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        OnInventoryOpen?.Invoke();
    }

    private void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        OnInventoryClose?.Invoke();
    }

    // ----------------------------------------------------------------------------------------

    // 이벤트 시스템의 인터페이스 함수들 ---------------------------------------------------------

    /// <summary>
    /// 드래그 중에 실행
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        //if(eventData.button == PointerEventData.InputButton.Left)
        //{
        //    tempItemSlotUI.transform.position = eventData.position;
        //}
    }

    /// <summary>
    /// 드래그 시작시 실행
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) // 좌클릭일 때만 처리
        {
            if (TempSlotUI.IsEmpty())    // 임시 슬롯에 아이템이 없는 경우에만 실행(아이템은 나누어서 들고 있는 상황)
            {
                GameObject startObj = eventData.pointerCurrentRaycast.gameObject;   // 드래그 시작한 위치에 있는 게임 오브젝트 가져오기
                if (startObj != null)
                {
                    // 드래그 시작한 위치에 게임 오브젝트가 있으면
                    //Debug.Log(startObj.name);
                    ItemSlotUI slotUI = startObj.GetComponent<ItemSlotUI>();    // ItemSlotUI 컴포넌트 가져오기
                    if (slotUI != null)
                    {
                        // ItemSlotUI 컴포넌트가 있으면 ID 기록해 놓기
                        //Debug.Log($"Start SlotID : {slotUI.ID}");
                        dragStartID = slotUI.ID;
                        inven.TempRemoveItem(dragStartID, slotUI.ItemSlot.ItemCount);
                        tempItemSlotUI.Open();   // 드래그 시작할 때 열기
                        detail.Close();         // 상세정보창 닫기
                        detail.IsPause = true;  // 상세정보창 안열리게 하기
                    }
                }
            }
        }
    }

    /// <summary>
    /// 드래그가 끝났을 때 실행
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) // 좌클릭일 때만 처리
        {
            if (dragStartID != InvalideID)  // 드래그가 정상적으로 시작되었을 때만 처리
            {
                GameObject endObj = eventData.pointerCurrentRaycast.gameObject; // 드래그 끝난 위치에 있는 게임 오브젝트 가져오기
                if (endObj != null)
                {
                    // 드래그 끝난 위치에 게임 오브젝트가 있으면
                    //Debug.Log(endObj.name);
                    ItemSlotUI slotUI = endObj.GetComponent<ItemSlotUI>();  // ItemSlotUI 컴포넌트 가져오기
                    if (slotUI != null)
                    {
                        // ItemSlotUI 컴포넌트가 있으면 Inventory.MoveItem() 실행시키기
                        //Debug.Log($"End SlotID : {slotUI.ID}");

                        inven.MoveItem(Inventory.TempSlotID, slotUI.ID);       // 아이템 실제로 옮기기
                        inven.MoveItem(Inventory.TempSlotID, dragStartID);     // 

                        detail.IsPause = false;                         // 상세정보창 다시 열릴 수 있게 하기
                        detail.Open(slotUI.ItemSlot.SlotItemData);      // 상세정보창 열기
                        dragStartID = InvalideID;                       // 드래그 시작 id를 될 수 없는 값으로 설정(드래그가 끝났음을 표시)
                    }
                }
                if (tempItemSlotUI.IsEmpty())
                {
                    tempItemSlotUI.Close(); // 정상적으로 드래그가 끝나면 닫기
                }
            }
        }
    }

    // ----------------------------------------------------------------------------------------
}
