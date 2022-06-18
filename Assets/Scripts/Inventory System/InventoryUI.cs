using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventorySlotUI InventoryItemPrefab;


    private void Awake()
    {
        playerInventory.inventoryUpdated += Redraw;
        DrawInventorySlots();
    }

    private void Start()
    {
        Redraw();
    }
    
    public void OnOpenShopButtonClick()
    {
        Redraw();
    }

    public void OnSellButtonClick()
    {
        int index = InventorySlotUI.GetSelectedSlot();
        if (index == -1) return;
        playerInventory.SellFromInventory(index);
        InventorySlotUI.ClearSelectedSlot();
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
