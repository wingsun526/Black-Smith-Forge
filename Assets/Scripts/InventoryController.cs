using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UI.General_UI;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    
    // when button click, ask ui for user input
    // do something to the inventory

    [SerializeField] private ItemSelectionGridUI itemSelectionGridUI;
    [SerializeField] private PlayerInventory targetInventory;

    [SerializeField] private SalesTable salesTable;
    
    
    public void ButtonOnePlace()
    {
        var nullableIndex = itemSelectionGridUI.GetSelectedItemIndex();
        
        if(nullableIndex == null)
        {
            throw new Exception("no item selected");
        }

        int index = nullableIndex.Value;
        
        // switch targetItem and salesTable Item
        Sword inventorySword = targetInventory.GetSwordOutOfThisSlot(index);
        
        
        Sword TableSword = salesTable.ChangeItemOnDisplay(inventorySword);
        targetInventory.PlaceSwordInThisSlot(index, TableSword);


        itemSelectionGridUI.ResetSelectedItem();


    }
}
