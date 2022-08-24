using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class HpPotion : MonoBehaviour
{
    uint potionCount = 0;
    Image potionImage;
    TextMeshProUGUI potionCountText;
    Player player;
    ItemSlot slot;
    InventoryUI invenUI;


    private void Awake()
    {
        potionImage = transform.GetChild(0).GetComponent<Image>();
        potionCountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        player = GameManager.Inst.MainPlayer;
        invenUI = GameManager.Inst.InvenUI;
    }

    public void PotionSearch()
    {
        slot = invenUI.GetHealingPotion();
        if (slot != null)
        {
            potionImage.color = Color.white;
            potionImage.sprite = slot.SlotItemData.itemIcon;
            potionCount = slot.ItemCount;
            potionCountText.text = potionCount.ToString();
        }
        else
        {
            potionImage.color = Color.clear;
            potionImage.sprite = null;
            potionCount = 0;
            potionCountText.text = "";
        }
    }

    private void Update()
    {
        if(potionCount <= 0 || potionCount != slot.ItemCount)
            PotionSearch();

        if (potionCount > 0)
        {
            if (Keyboard.current.digit5Key.wasPressedThisFrame && player.Hp < player.MaxHP)
            {
                slot.UseSlotItem(player);
                potionCount = slot.ItemCount;
                potionCountText.text = potionCount.ToString();
            }
        }

    }
}
