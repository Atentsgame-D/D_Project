using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    // 기본 데이터 -----------------------------------------------------------------------------
    // 아이템 슬롯 Type : Inventory, Store
    public SlotType slotType;

    // 아이템 슬롯 ID
    private uint id;

    // 이 슬롯UI에서 가지고 있을 ItemSlot(inventory클래스가 가지고 있는 ItemSlot중 하나)
    protected ItemSlot itemSlot;
    // ----------------------------------------------------------------------------------------

    // UI처리용 데이터 --------------------------------------------------------------------------
    // 아이템의 Icon을 표시할 이미지 컴포넌트
    public Image itemImage;

    public GameObject countImage;
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
        countImage = transform.GetChild(0).GetChild(0).gameObject;
    }

    /// <summary>
    /// ItemSlotUI의 초기화 작업
    /// </summary>
    /// <param name="newID">이 슬롯의 ID</param>
    /// <param name="targetSlot">이 슬롯이랑 연결된 itemSlot</param>
    public void Initialize(uint newID, ItemSlot targetSlot)
    {
        id = newID;
        itemSlot = targetSlot;
        itemSlot.onSlotItemChage = Refresh; // itemSlot에 아이템이 변경될 경우 실행될 델리게이트에 함수 등록
        countImage.SetActive(false);
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
        }
        else
        {
            itemImage.sprite = null;    // 아이콘 이미지 제거
            itemImage.color = Color.clear;  // 컬러 제거
        }
    }

    public void OnItemCountText(Item item)
    {
        if (item.data.itemType == ItemType.Consumable)
        {
            countImage.SetActive(true);
        }
    }

    public void OffItemCountImage()
    {
        countImage.SetActive(false);
    }

    public void SetSlotCount(ItemSlot itemSlot, int count = 1)
    {
        itemSlot.ItemCount += count;
        TextMeshProUGUI countText = countImage.GetComponentInChildren<TextMeshProUGUI>();
        countText.text = itemSlot.ItemCount.ToString();
    }
}
