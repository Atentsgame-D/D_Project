using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IPointerClickHandler
{
    uint id;
    protected ItemSlot itemSlot;  // inventory클래스가 가지고 있는 ItemSlot중 하나

    protected Image itemImage;
    protected TextMeshProUGUI countText;

    InventoryUI invenUI;
    DetailInfoUI detailUI;

    public ItemSlot ItemSlot { get => itemSlot; }

    public uint ID { get => id; }

    protected virtual void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent <Image>();
        countText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(uint newID, ItemSlot targetSlot)
    {
        invenUI = GameManager.Inst.InvenUI;
        detailUI = invenUI.Detail;
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
            countText.text = itemSlot.ItemCount.ToString();
        }
        else
        {
            itemImage.sprite = null;
            itemImage.color = Color.clear;
            countText.text = "";
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

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            StoreUI storeUI = GameManager.Inst.StoreUI;
            if(  storeUI.CanvasGroup.alpha == 1 )
            {
                if ( !itemSlot.IsEmpty() )
                {
                    if( itemSlot.ItemCount > 1)
                    {
                        invenUI.SpliterUI_Sell.Open(this);
                    }
                    else
                    {
                        invenUI.inven.SellItem(this.itemSlot, this.itemSlot.ItemCount);
                    }
                }
            }
            else
            {
                TempItemSlotUI temp = invenUI.TempSlotUI;

                if (Keyboard.current.leftShiftKey.ReadValue() > 0.0f && temp.IsEmpty())
                {
                    invenUI.SpliterUI.Open(this);
                    detailUI.IsPause = true;
                }
                else
                {
                    if (!temp.IsEmpty())
                    {
                        if (ItemSlot.IsEmpty())
                        {
                            ItemSlot.AssignSlotItem(temp.ItemSlot.SlotItemData, temp.ItemSlot.ItemCount);
                            temp.Close();
                        }
                        else if (temp.ItemSlot.SlotItemData == ItemSlot.SlotItemData)
                        {
                            uint remains = ItemSlot.SlotItemData.maxStackCount - ItemSlot.ItemCount;
                            uint small = (uint)Mathf.Min((int)remains, (int)temp.ItemSlot.ItemCount);
                            ItemSlot.IncreaseSlotItem(small);
                            temp.ItemSlot.DecreaseSlotItem(small);
                            if (temp.ItemSlot.ItemCount < 1)
                            {
                                temp.Close();
                            }
                        }
                        else
                        {
                            ItemData tempData = temp.ItemSlot.SlotItemData;
                            uint tempCount = temp.ItemSlot.ItemCount;
                            temp.ItemSlot.AssignSlotItem(itemSlot.SlotItemData, itemSlot.ItemCount);
                            itemSlot.AssignSlotItem(tempData, tempCount);
                        }
                        detailUI.IsPause = false;
                    }
                    else
                    {
                        if (!itemSlot.IsEmpty())
                        {
                            IUsable usable = itemSlot.SlotItemData as IUsable;
                            if (usable != null)
                            {
                                usable.Use(GameManager.Inst.MainPlayer.gameObject);
                                ItemSlot.DecreaseSlotItem();
                            }
                        }
                    }
                }
            }
        }
    }
}
