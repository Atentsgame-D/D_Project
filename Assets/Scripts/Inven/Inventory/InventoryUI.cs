using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // public -------------------------------------------------------------------------------------
    /// <summary>
    /// ItemSlotUI가 있는 프리팹. 
    /// </summary>
    public GameObject slotPrefab;   // 초기화시 새로 생성해야할 경우 사용

    // 기본 데이터 ---------------------------------------------------------------------------------
    /// <summary>
    /// 이 인벤토리를 사용하는 플레이어
    /// </summary>
    private Player player;

    /// <summary>
    /// 이 클래스로 표현하려는 인벤토리
    /// </summary>
    public Inventory inven;

    /// <summary>
    /// 슬롯 생성시 부모가 될 게임 오브젝트의 트랜스폼
    /// </summary>
    private Transform slotParent;

    /// <summary>
    /// 이 인벤토리가 가지고 있는 슬롯UI들
    /// </summary>
    private ItemSlotUI[] slotUIs;

    /// <summary>
    /// 열고 닫기용 캔버스 그룹
    /// </summary>
    private CanvasGroup canvasGroup;

    private PlayerInputActions inputActions;


    // Item관련  ----------------------------------------------------------------------------------    
    /// <summary>
    /// 드래그 시작 표시용( 시작 id가 InvalideID면 드래그 시작을 안한 것)
    /// </summary>
    private const uint InvalideID = uint.MaxValue;

    /// <summary>
    /// 드래그가 시작된 슬롯의 ID
    /// </summary>
    uint dragStartID = InvalideID;

    /// <summary>
    /// 임시 슬롯(아이템 드래그나 아이템 분리할 때 사용)
    /// </summary>
    TempItemSlotUI tempItemSlotUI;
    public TempItemSlotUI TempSlotUI => tempItemSlotUI;

    // 상세 정보 UI --------------------------------------------------------------------------------
    /// <summary>
    /// 아이템 상세정보 창
    /// </summary>
    DetailInfoUI detail;
    public DetailInfoUI Detail => detail;

    // 아이템 분할 UI --------------------------------------------------------------------------------
    /// <summary>
    /// 아이템 분할 창
    /// </summary>
    ItemSpliterUI itemSpliterUI;
    public ItemSpliterUI SpliterUI => itemSpliterUI;

    /// <summary>
    /// 팔기 전용 아이템 분할 창
    /// </summary>
    ItemSpliterUI_Sell spliterUI_Sell;
    public ItemSpliterUI_Sell SpliterUI_Sell => spliterUI_Sell;

    // 돈 UI --------------------------------------------------------------------------------------
    /// <summary>
    /// 돈 표시할 text
    /// </summary>
    TextMeshProUGUI goldText;

    // 델리게이트 ----------------------------------------------------------------------------------
    public Action OnInventoryOpen;
    public Action OnInventoryClose;

    private bool isMove = false;

    // 유니티 이벤트 함수들 -------------------------------------------------------------------------
    private void Awake()
    {
        // 미리 찾아놓기
        canvasGroup = GetComponent<CanvasGroup>();
        goldText = transform.Find("MoneyPanel").Find("Money").GetComponent<TextMeshProUGUI>();
        slotParent = transform.Find("Inventory_Base").Find("Grid Setting");
        tempItemSlotUI = GetComponentInChildren<TempItemSlotUI>();
        detail = GetComponentInChildren<DetailInfoUI>();
        itemSpliterUI = GetComponentInChildren<ItemSpliterUI>();
        spliterUI_Sell = transform.Find("ItemSpliter_Sell").GetComponent<ItemSpliterUI_Sell>();

        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(Close);

        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.ShortCut.Enable();
        inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Disable();
        inputActions.ShortCut.Disable();
    }


    private void Start()
    {
        player = GameManager.Inst.MainPlayer;   // 게임 메니저에서 플레이어 가져오기
        player.OnMoneyChange += RefreshMoney;   // 플레이어의 Money가 변경되는 실행되는 델리게이트에 함수 등록
        RefreshMoney(player.Money);             // 첫 갱신

        Close();    // 시작할 때 무조건 닫기
    }

    // 일반 함수들 ---------------------------------------------------------------------------------

    /// <summary>
    /// 인벤토리를 입력받아 UI를 초기화하는 함수
    /// </summary>
    /// <param name="newInven">이 UI로 표시할 인벤토리</param>
    public void InitializeInventory(Inventory newInven)
    {
        inven = newInven;   //즉시 할당
        if (Inventory.Default_Inventory_Size != newInven.SlotCount)    // 기본 사이즈와 다르면 기본 슬롯UI 삭제
        {
            // 기존 슬롯UI 전부 삭제
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
                slotUIs[i].Initialize((uint)i, inven[i]);       // 각 슬롯UI들도 초기화
            }
        }
        else
        {
            // 크기가 같을 경우 슬롯UI들의 초기화만 진행
            slotUIs = slotParent.GetComponentsInChildren<ItemSlotUI>();
            for (int i = 0; i < inven.SlotCount; i++)
            {
                slotUIs[i].Initialize((uint)i, inven[i]);
            }
        }

        // TempSlot 초기화
        tempItemSlotUI.Initialize(Inventory.TempSlotID, inven.TempSlot);    // TempItemSlotUI와 TempSlot 연결
        tempItemSlotUI.Close(); // tempItemSlotUI 닫은채로 시작하기
        inputActions.UI.ItemDrop.canceled += tempItemSlotUI.OnDrop;

        // ItemSpliterUI 초기화(순서는 딱히 상관 없음)
        itemSpliterUI.Initialize();
        itemSpliterUI.OnOkClick += OnSpliteOK;  // itemSpliterUI의 OK 버튼이 눌려졌을 때 실행할 함수 등록

        spliterUI_Sell.Initialize();
        spliterUI_Sell.OnOkClick_Sell += inven.SellItem;

        RefreshAllSlots();  // 전체 슬롯UI 갱신
    }

    /// <summary>
    /// 모든 슬롯의 Icon이미지를 갱신
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
        goldText.text = $"{money:N0}";  // Money가 변경될 때 실행될 함수
    }

    /// <summary>
    /// 인벤토리 열고 닫기
    /// </summary>
    public void InventoryOnOffSwitch()
    {
        if (canvasGroup.blocksRaycasts)  // 캔버스 그룹의 blocksRaycasts를 기준으로 처리
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    /// <summary>
    /// 인벤토리 열기
    /// </summary>
    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        OnInventoryOpen?.Invoke();
    }

    /// <summary>
    /// 인벤토리 닫기
    /// </summary>
    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        OnInventoryClose?.Invoke();
    }


    // 델리게이트용 함수 ---------------------------------------------------------------------------
    /// <summary>
    /// SpliterUI가 OK됬을 때 실행될 함수
    /// </summary>
    /// <param name="slotID">나누려는 슬롯의 ID</param>
    /// <param name="count">나눈 갯수</param>
    private void OnSpliteOK(uint slotID, uint count)
    {
        inven.TempRemoveItem(slotID, count);    // slotID에서 count만큼 덜어내서 TempSlot에 옮기기
        tempItemSlotUI.Open();  // tempItemSlotUI 열어서 보여주기
    }

    // 이벤트 시스템의 인터페이스 함수들 -------------------------------------------------------------

    public void OnDrag(PointerEventData eventData)
    {
        if (isMove)
            transform.position = Mouse.current.position.ReadValue() + new Vector2(0,-200);
    }

    /// <summary>
    /// 드래그 시작시 실행
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) // 좌클릭일 때만 처리
        {
            if (TempSlotUI.IsEmpty() && !SpliterUI.isActiveAndEnabled)
            {
                GameObject startObj = eventData.pointerCurrentRaycast.gameObject;   // 드래그 시작한 위치에 있는 게임 오브젝트 가져오기
                if (startObj != null)
                {
                    ItemSlotUI slotUI = startObj.GetComponent<ItemSlotUI>();    // ItemSlotUI 컴포넌트 가져오기
                    if (slotUI != null)
                    {
                        dragStartID = slotUI.ID;
                        inven.TempRemoveItem(dragStartID, slotUI.ItemSlot.ItemCount, slotUI.ItemSlot.ItemEquiped);   // 드래그 시작한 위치의 아이템을 TempSlot으로 옮김
                        tempItemSlotUI.Open();  // 드래그 시작할 때 TempSlot 열기
                        detail.Close();         // 상세정보창 닫기
                        detail.IsPause = true;  // 상세정보창 안열리게 하기
                    }
                    else
                    {
                        if (!isMove)
                            isMove = true;
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
                    ItemSlotUI slotUI = endObj.GetComponent<ItemSlotUI>();  // ItemSlotUI 컴포넌트 가져오기
                    if (slotUI != null)
                    {
                        inven.MoveItem(Inventory.TempSlotID, slotUI.ID);

                        inven.MoveItem(Inventory.TempSlotID, dragStartID);

                        detail.IsPause = false;                         // 상세정보창 다시 열릴 수 있게 하기
                        detail.Open(slotUI.ItemSlot.SlotItemData);      // 상세정보창 열기
                        dragStartID = InvalideID;                       // 드래그 시작 id를 될 수 없는 값으로 설정(드래그가 끝났음을 표시)
                    }
                }

                if (tempItemSlotUI.IsEmpty())
                {
                    tempItemSlotUI.Close(); // 드래그를 끝내고 tempSlot이 비어지면 닫기
                }
            }
            if (isMove)
                isMove = false;
        }        
    }

    public ItemSlot GetHealingPotion()
    {
        List<ItemSlot> slotList = new List<ItemSlot>();
        ItemData_HealingPotion oldpotion = null;
        ItemSlot result = null;

        if (slotUIs == null) return null;

        foreach(var slotUI in slotUIs)
        {
            ItemData_HealingPotion temp = slotUI.ItemSlot.SlotItemData as ItemData_HealingPotion;
            if (temp == null)
                continue;

            slotList.Add(slotUI.ItemSlot);
        }

        foreach(var slot in slotList)
        {
            ItemData_HealingPotion potion = slot.SlotItemData as ItemData_HealingPotion;

            if (oldpotion == null || potion.healPoint <= oldpotion.healPoint)
            {
                oldpotion = potion;
                result = slot;
            }
         }

        return result;
    }

    public ItemSlot GetManaPotion()
    {
        List<ItemSlot> slotList = new List<ItemSlot>();
        ItemData_ManaPotion oldpotion = null;
        ItemSlot result = null;

        if (slotUIs == null) return null;

        foreach (var slotUI in slotUIs)
        {
            ItemData_ManaPotion temp = slotUI.ItemSlot.SlotItemData as ItemData_ManaPotion;
            if (temp == null)
                continue;

            slotList.Add(slotUI.ItemSlot);
        }

        foreach (var slot in slotList)
        {
            ItemData_ManaPotion potion = slot.SlotItemData as ItemData_ManaPotion;

            if (oldpotion == null || potion.manaPoint <= oldpotion.manaPoint)
            {
                oldpotion = potion;
                result = slot;
            }
        }

        return result;
    }
}
