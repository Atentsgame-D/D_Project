using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ItemSpliterUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private uint itemSplitCount = 1;
    private ItemSlotUI targetSlotUI;

    private TMP_InputField inputField;

    public Action<uint, uint> OnOkClick;

    private bool isMove = false;

    public uint ItemSplitCount
    {
        get => itemSplitCount;
        set
        {
            itemSplitCount = value;
            itemSplitCount = (uint)Mathf.Max(1, itemSplitCount);    // 1이 최소값
            //(targetSlotUI.ItemSlot.ItemCount - 1)이 최대 값.
            if (targetSlotUI != null)
            {
                itemSplitCount = (uint)Mathf.Min(itemSplitCount, targetSlotUI.ItemSlot.ItemCount - 1);
            }
            inputField.text = itemSplitCount.ToString();
        }
    }

    public void Initialize()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        inputField.onValueChanged.AddListener(OnInputChange);
        inputField.text = itemSplitCount.ToString();

        Button increase = transform.Find("InCreaseButton").GetComponent<Button>();
        increase.onClick.AddListener(OnIncrease);
        Button decrease = transform.Find("DecreaseButton").GetComponent<Button>();
        decrease.onClick.AddListener(OnDecrease);
        Button ok = transform.Find("OK_Button").GetComponent<Button>();
        ok.onClick.AddListener(OnOk);
        Button cancel = transform.Find("Cancel_Button").GetComponent<Button>();
        cancel.onClick.AddListener(OnCancel);

        Close();
    }

    public void Open(ItemSlotUI target)
    {
        if (target.ItemSlot.ItemCount > 1)
        {
            targetSlotUI = target;
            ItemSplitCount = 1;
            gameObject.SetActive(true);
        }
    }

    public void Close() => gameObject.SetActive(false);

    private void OnCancel()
    {
        Debug.Log("Cancel");
        targetSlotUI = null;
        Close();
    }

    private void OnOk()
    {
        //Debug.Log("OnOK");
        //targetSlotUI.ItemSlot.DecreaseSlotItem(ItemSplitCount);
        //ItemSlot tempSlot = new(targetSlotUI.ItemSlot.SlotItemData, ItemSplitCount);
        //tempSlot.onSlotItemChage = GameManager.Inst.InvenUI.TempSlotUI.Refresh;
        //GameManager.Inst.InvenUI.TempSlotUI.Open(tempSlot);

        OnOkClick?.Invoke(targetSlotUI.ID, ItemSplitCount);
        Close();
    }

    private void OnDecrease()
    {
        Debug.Log("Decrease");
        ItemSplitCount--;
    }

    private void OnIncrease()
    {
        Debug.Log("Increase");
        ItemSplitCount++;
    }

    private void OnInputChange(string input)
    {
        Debug.Log($"OnInputChange : {input}");
        ItemSplitCount = uint.Parse(input);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isMove)
            transform.position = Mouse.current.position.ReadValue();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) // 좌클릭일 때만 처리
        {
            if (!isMove)
                isMove = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) // 좌클릭일 때만 처리
        {
            if (isMove)
                isMove = false;
        }
    }
}
