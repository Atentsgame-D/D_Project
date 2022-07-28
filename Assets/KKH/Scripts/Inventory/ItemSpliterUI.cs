using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSpliterUI : MonoBehaviour
{
    private uint itemSplitCount = 1;
    private ItemSlotUI targetSlotUI;

    private TMP_InputField inputField;

    public uint ItemSplitCount
    {
        get => itemSplitCount;
        set
        {
            itemSplitCount = value;
            itemSplitCount = (uint)Mathf.Max(1, itemSplitCount);
            itemSplitCount = (uint)Mathf.Min(itemSplitCount, targetSlotUI.ItemSlot.ItemCount - 1);
            inputField.text = itemSplitCount.ToString();
        }
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

    private void Awake()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        inputField.onValueChanged.AddListener(OnInputChange);

        Button increase = transform.Find("InCreaseButton").GetComponent<Button>();
        increase.onClick.AddListener(OnIncrease);
        Button decrease = transform.Find("DecreaseButton").GetComponent<Button>();
        decrease.onClick.AddListener(OnDecrease);
        Button ok = transform.Find("OK_Button").GetComponent<Button>();
        ok.onClick.AddListener(OnOk);
        Button cancel = transform.Find("Cancel_Button").GetComponent<Button>();
        cancel.onClick.AddListener(OnCancel);
    }

    private void Start()
    {
        inputField.text = itemSplitCount.ToString();
    }

    private void OnCancel()
    {
        Debug.Log("Cancel");
        targetSlotUI = null;
        Close();
    }

    private void OnOk()
    {
        Debug.Log("Ok");
        targetSlotUI.ItemSlot.DecreaseSlotItem(ItemSplitCount);
        ItemSlot tempSlot = new(targetSlotUI.ItemSlot.SlotItemData, ItemSplitCount);
        GameManager.Inst.InvenUI.TempSlotUI.Open(tempSlot);
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
}
