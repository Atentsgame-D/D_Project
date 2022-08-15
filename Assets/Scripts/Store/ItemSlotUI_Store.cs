using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlotUI_Store : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IPointerClickHandler
{
    uint id;
    private ItemSlot_Store itemSlot;

    Image itemImage;

    StoreUI storeUI;
    DetailInfoUI detailUI;

    public ItemSlot_Store ItemSlot { get => itemSlot; }

    public uint ID { get => id; }

    protected void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void Initialize(uint newID, ItemSlot_Store targetSlot)
    {
        storeUI = GameManager.Inst.StoreUI;
        detailUI = storeUI.Detail;
        id = newID;
        itemSlot = targetSlot;
        itemSlot.onSlotItemChange = Refresh;
    }

    public void Refresh()
    {
        if (itemSlot.SlotItemData != null)
        {
            itemImage.sprite = itemSlot.SlotItemData.itemIcon;
            itemImage.color = Color.white;
        }
        else
        {
            itemImage.sprite = null;
            itemImage.color = Color.clear;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(itemSlot.SlotItemData != null)
        {
            detailUI.Open(itemSlot.SlotItemData);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        detailUI.Close();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Vector2 mousePos = eventData.position;

        RectTransform rect = (RectTransform)detailUI.transform;
        if((mousePos.x + rect.sizeDelta.x) > Screen.width)
        {
            mousePos.x -= rect.sizeDelta.x;
        }
        detailUI.transform.position = mousePos;
    }

    public virtual void OnPointerClick(PointerEventData eventData) => storeUI.store.BuyItem(this.ItemSlot);
}
