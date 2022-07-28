using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

public class ItemSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IPointerClickHandler
{
    // 기본 데이터 -----------------------------------------------------------------------------
    // 아이템 슬롯 ID
    private uint id;

    // 이 슬롯UI에서 가지고 있을 ItemSlot(inventory클래스가 가지고 있는 ItemSlot중 하나)
    protected ItemSlot itemSlot;

    private InventoryUI invenUI;
    private DetailInfoUI detailInfoUI;
    // ----------------------------------------------------------------------------------------

    // UI처리용 데이터 --------------------------------------------------------------------------
    // 아이템의 Icon을 표시할 이미지 컴포넌트
    protected Image itemImage;
    protected TextMeshProUGUI countText;
    // ----------------------------------------------------------------------------------------

    // 프로퍼티 --------------------------------------------------------------------------------
    public uint ID { get => id; }
    public ItemSlot ItemSlot
    {
        get => itemSlot;
    }
    // ----------------------------------------------------------------------------------------

    protected virtual void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();    // 아이템 표시용 이미지 컴포넌트 찾아놓기
        countText = GetComponentInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// ItemSlotUI의 초기화 작업
    /// </summary>
    /// <param name="newID">이 슬롯의 ID</param>
    /// <param name="targetSlot">이 슬롯이랑 연결된 itemSlot</param>
    public void Initialize(uint newID, ItemSlot targetSlot)
    {
        invenUI = GameManager.Inst.InvenUI;
        detailInfoUI = invenUI.Detail;

        id = newID;
        itemSlot = targetSlot;
        itemSlot.onSlotItemChage = Refresh; // itemSlot에 아이템이 변경될 경우 실행될 델리게이트에 함수 등록
    }

    /// <summary>
    /// 슬롯에서 표시되는 아이콘 이미지 갱신용 함수
    /// </summary>
    public void Refresh()
    {
        if (itemSlot.SlotItemData != null)
        {
            itemImage.sprite = itemSlot.SlotItemData.itemIcon;  // 아이콘 이미지 설정
            itemImage.color = Color.white;  // 불투명하게 설정
            countText.text = ItemSlot.ItemCount.ToString();
        }
        else
        {
            itemImage.sprite = null;    // 아이콘 이미지 제거
            itemImage.color = Color.clear;  // 컬러 제거
            countText.text = string.Empty;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemSlot.SlotItemData != null)
        {
            //Debug.Log($"마우스가 {gameObject.name}안으로 들어왔다.");
            detailInfoUI.Open(itemSlot.SlotItemData);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log($"마우스가 {gameObject.name}에서 나갔다.");
        detailInfoUI.Close();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        //Debug.Log($"마우스가 {gameObject.name}안에서 움직인다.");
        Vector2 mousePos = eventData.position;

        RectTransform rect = (RectTransform)detailInfoUI.transform;
        if ((mousePos.x + rect.sizeDelta.x) > Screen.width)
        {
            mousePos.x -= rect.sizeDelta.x;
        }

        detailInfoUI.transform.position = mousePos;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Keyboard.current.leftShiftKey.ReadValue() > 0)
            {
                Debug.Log("Shift + 좌클릭");
                invenUI.SpliterUI.Open(this);
            }
            else
            {
                //    if(invenUI.TempSlotUI.ItemSlot.IsEmpty())
                //    {
                //        eventData.pointerCurrentRaycast.gameObject
                //    }

                TempItemSlotUI temp = invenUI.TempSlotUI;
                if (ItemSlot.IsEmpty())
                {
                    itemSlot.AssignSlotItem(temp.ItemSlot.SlotItemData, temp.ItemSlot.ItemCount);
                    invenUI.TempSlotUI.Close();
                }
                else if (temp.ItemSlot.SlotItemData == ItemSlot.SlotItemData)
                {
                    uint remains = ItemSlot.SlotItemData.maxStackCount - ItemSlot.ItemCount;
                    ItemSlot.IncreaseSlotItem(remains);
                    temp.ItemSlot.DecreaseSlotItem(remains);
                }
            }
        }
    }
}
