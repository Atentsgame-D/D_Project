using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TempItemSlotUI : ItemSlotUI
{
    /// <summary>
    /// Awake을 override해서 부모의 Awake 실행안되게 만들기
    /// </summary>
    protected override void Awake()
    {
        itemImage = GetComponent<Image>();
    }

    
    private void Update()
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    /// <summary>
    /// 임시 슬롯을 보이도록 열기
    /// </summary>
    /// <param name="slot">임시 슬롯에 할당할 아이템이 들어있는 슬롯</param>
    public void Open(ItemSlot slot)
    {
        SetTempSlot(slot);
        transform.position = Mouse.current.position.ReadValue();
        gameObject.SetActive(true);
        //itemImage.color = Color.white;
    }

    /// <summary>
    /// 임시 슬롯이 보이지 않게 닫기
    /// </summary>
    public void Close()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 임시 슬롯에서 보일 슬롯 설정
    /// </summary>
    /// <param name="slot">임시 슬롯에 할당할 아이템이 들어있는 슬롯</param>
    private void SetTempSlot(ItemSlot slot)
    {
        itemSlot = slot;
        Refresh();
    }
}
