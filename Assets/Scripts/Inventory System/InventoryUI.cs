using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;


    private void Awake()
    {
        playerInventory.inventoryUpdated += Redraw;
    }

    private void Start()
    {
        Redraw();
    }

    
    private void Redraw()
    {
        var list = GetComponentsInChildren<InventorySlotUI>();
        var theInventory = playerInventory.GetArray();
        for(int i = 0; i < theInventory.Length; i++)
        {
            list[i].Setup(theInventory[i]);
        }
    }
}
