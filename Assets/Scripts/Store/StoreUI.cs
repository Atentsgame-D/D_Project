using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class StoreUI : MonoBehaviour
{
    // 기본 데이터 -----------------------------------------------------------------------------------------------------------------
    public Store store;
    Player player;
    public GameObject slotPrefab;
    Transform slotParent;
    ItemSlotUI_Store[] slotUIs;
    CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup => canvasGroup;

    DetailInfoUI detail;
    public DetailInfoUI Detail => detail;

    // ----------------------------------------------------------------------------------------------------------------------------
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        slotParent = transform.Find("Store_Base").Find("Grid Setting_Store");
        detail = transform.Find("Detail_Store").GetComponent<DetailInfoUI>();

        Button closeButton = transform.Find("CloseButton_Store").GetComponent<Button>();
        closeButton.onClick.AddListener(Close);

    }

    private void Start()
    {
        player = GameManager.Inst.MainPlayer;

        Close();
    }

    public void InitializeInventory(Store newStore)
    {
        store = newStore;
        if (Store.Default_Store_Size != newStore.SlotCount)      // 기본 사이즈와 다르면 기본 슬롯 삭제
        {
            // 기존 슬롯 전부 삭제
            ItemSlotUI_Store[] slots = GetComponentsInChildren<ItemSlotUI_Store>();
            foreach (var slot in slots)
            {
                Destroy(slot.gameObject);
            }
            // 새로 만들기
            slotUIs = new ItemSlotUI_Store[store.SlotCount];
            for (int i = 0; i < store.SlotCount; i++)
            {
                GameObject obj = Instantiate(slotPrefab, slotParent);
                obj.name = $"{slotPrefab.name}_{i}";
                slotUIs[i] = obj.GetComponent<ItemSlotUI_Store>();
                slotUIs[i].Initialize((uint)i, store[i]);
            }
        }
        else
        {
            slotUIs = slotParent.GetComponentsInChildren<ItemSlotUI_Store>();
            for (int i = 0; i < store.SlotCount; i++)
            {
                slotUIs[i].Initialize((uint)i, store[i]);
            }
        }
        RefreshAllSlots();      // 전체 슬롯UI 갱신
    }

    private void RefreshAllSlots()
    {
        foreach(var slotUI in slotUIs)
        {
            slotUI.Refresh();
        }
    }

    public void StoreOnOffSwitch()
    {
        if (canvasGroup.blocksRaycasts)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

}
