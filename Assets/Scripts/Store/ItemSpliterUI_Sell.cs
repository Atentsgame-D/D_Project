using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ItemSpliterUI_Sell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    uint itemSplitCount = 1;

    ItemSlotUI targetSlotUI;

    TMP_InputField inputField;

    private bool isMove = false;

    /// <summary>
    /// OK��ư�� ������ �� ���� �� ��������Ʈ
    /// </summary>
    public Action<ItemSlot, uint> OnOkClick_Sell;

    /// <summary>
    /// ������ ���� ������ ������Ƽ
    /// </summary>
    uint ItemSplitCount
    {
        get => itemSplitCount;
        set
        {
            // ���� �Էµ� �� �ּҰ��� 1, �ִ밪�� (��󽽷��� ������ �ִ� ������ ���� - 1)�� �����ϴ� �ڵ�
            itemSplitCount = value;
            itemSplitCount = (uint)Mathf.Max(1, itemSplitCount);    // 1�� �ּҰ�
            if(targetSlotUI != null)
            {
                itemSplitCount = (uint)Mathf.Min(itemSplitCount, targetSlotUI.ItemSlot.ItemCount);
            }
            inputField.text = itemSplitCount.ToString();
        }
    }

    public void Initialize()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        inputField.onValueChanged.AddListener(OnInputChange);
        inputField.text = itemSplitCount.ToString();

        Button increase = transform.Find("IncreaseButton2").GetComponent<Button>();
        increase.onClick.AddListener(OnIncrease);
        Button decrease = transform.Find("DecreaseButton2").GetComponent<Button>();
        decrease.onClick.AddListener(OnDecrease);
        Button ok = transform.Find("OK_Button2").GetComponent<Button>();
        ok.onClick.AddListener(OnOK);
        Button cancel = transform.Find("Cancel_Button2").GetComponent<Button>();
        cancel.onClick.AddListener(OnCancelClick);

        Close();
    }

    /// <summary>
    /// ������ ����â ����
    /// </summary>
    /// <param name="target">�������� ������ ��� ����</param>
    public void Open(ItemSlotUI target)
    {
        if(target.ItemSlot.ItemCount > 1)
        {
            targetSlotUI = target;
            ItemSplitCount = 1;
            transform.position = target.transform.position;
            gameObject.SetActive(true);
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void OnIncrease()
    {
        ItemSplitCount++;
    }

    private void OnDecrease()
    {
        ItemSplitCount--;
    }

    private void OnOK()
    {
        //targetSlotUI.ItemSlot.DecreaseSlotItem(ItemSplitCount);
        //ItemSlot tempSlot = new(targetSlotUI.ItemSlot.SlotItemData, ItemSplitCount);
        //tempSlot.onSlotItemChange = GameManager.Inst.InvenUI.TempSlotUI.Refresh;
        //GameManager.Inst.InvenUI.TempSlotUI.Open(tempSlot);

        OnOkClick_Sell?.Invoke(targetSlotUI.ItemSlot, ItemSplitCount);     // ��������Ʈ�� ����� �Լ� ����

        Close();    // �ݱ�
    }

    /// <summary>
    /// Cancel�������� ���� �� �Լ�
    /// </summary>
    private void OnCancelClick()
    {
        targetSlotUI = null;    // ������ �ʱ�ȭ�ϰ�
        Close();                // �ݱ�
    }

    /// <summary>
    /// InputField���� ���� ����� �� ����� �Լ�
    /// </summary>
    /// <param name="input">����� ��</param>
    private void OnInputChange(string input)
    {
        if( input.Length == 0)
        {
            ItemSplitCount = 0;
        }
        else
        {
            ItemSplitCount = uint.Parse(input);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isMove)
            transform.position = Mouse.current.position.ReadValue();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) // ��Ŭ���� ���� ó��
        {
            if (!isMove)
                isMove = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) // ��Ŭ���� ���� ó��
        {
            if (isMove)
                isMove = false;
        }
    }
}
