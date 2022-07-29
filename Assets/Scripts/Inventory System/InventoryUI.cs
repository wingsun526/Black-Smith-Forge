using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventorySlotUI InventoryItemPrefab;
    [SerializeField] private Button sellButton;
    [SerializeField] private FlyingCoins flyingCoins;


    private void Awake()
    {
        playerInventory.inventoryUpdated += Redraw;
        DrawInventorySlots();
    }

    private void Start()
    {
        Redraw();
        sellButton.onClick.AddListener(OnSellButtonClick);
    }
    
    public void OnOpenInventoryButtonClick()
    {
        Redraw();
    }

    public void OnSellButtonClick()
    {
        bool sellSuccess = SellSelected();
        if(sellSuccess)
        {
            flyingCoins.OnClickSendCoins(sellButton.transform);
        }
    }

    private bool SellSelected()
    {
        int index = InventorySlotUI.GetSelectedSlot();
        if (index == -1) return false;
        playerInventory.SellFromInventory(index);
        InventorySlotUI.ClearSelectedSlot();
        return true;
    }
    
    private void Redraw()
    {
        var list = GetComponentsInChildren<InventorySlotUI>();
        var theInventory = playerInventory.GetArray();
        for(int i = 0; i < theInventory.Length; i++)
        {
            list[i].AssignItem(theInventory[i]);
        }
    }
    
    // when inventory size change?
    private void DrawInventorySlots()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < playerInventory.GetSize(); i++)
        {
            var itemUI = Instantiate(InventoryItemPrefab, transform);
            itemUI.AssignIndex(i);
        }
    }

    
}
