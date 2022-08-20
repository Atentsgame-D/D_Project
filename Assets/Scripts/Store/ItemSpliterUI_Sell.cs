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
    /// OK버튼을 눌렀을 때 실행 될 델리게이트
    /// </summary>
    public Action<ItemSlot, uint> OnOkClick_Sell;

    /// <summary>
    /// 아이템 분할 갯수용 프로퍼티
    /// </summary>
    uint ItemSplitCount
    {
        get => itemSplitCount;
        set
        {
            // 값이 입력될 때 최소값은 1, 최대값은 (대상슬롯이 가지고 있는 아이템 갯수 - 1)로 설정하는 코드
            itemSplitCount = value;
            itemSplitCount = (uint)Mathf.Max(1, itemSplitCount);    // 1이 최소값
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
    /// 아이템 분할창 열기
    /// </summary>
    /// <param name="target">아이템을 분할할 대상 슬롯</param>
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

        OnOkClick_Sell?.Invoke(targetSlotUI.ItemSlot, ItemSplitCount);     // 델리게이트에 연결된 함수 실행

        Close();    // 닫기
    }

    /// <summary>
    /// Cancel눌렀을때 실행 될 함수
    /// </summary>
    private void OnCancelClick()
    {
        targetSlotUI = null;    // 변수들 초기화하고
        Close();                // 닫기
    }

    /// <summary>
    /// InputField에서 값이 변경될 때 실행될 함수
    /// </summary>
    /// <param name="input">변경된 값</param>
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
