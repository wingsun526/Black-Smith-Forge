using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UI.General_UI;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private ItemSelectionGridUI itemSelectionGridUI;
    [SerializeField] private PlayerInventory targetInventory;

    //Interaction with SalesTable
    private static SalesTable _currentlyConnectedSalesTable;
    public void RegisterThis(SalesTable incomingSalesTableObject)
    {
        _currentlyConnectedSalesTable = incomingSalesTableObject;
        transform.root.gameObject.SetActive(true);
    }
    
    public void DeRegisterThis()
    {
        itemSelectionGridUI.ResetGrid();
        _currentlyConnectedSalesTable = null;
        transform.root.gameObject.SetActive(false);
    }
    //End of SalesTable Interactions
    

    public void ButtonOnePlace()
    {
        if (_currentlyConnectedSalesTable == null)
        {
            throw new Exception("did the UI active without triggering?");
        } 
        
        var nullableIndex = itemSelectionGridUI.GetSelectedItemIndex();
        
        if(nullableIndex == null)
        {
            throw new Exception("no item selected");
        }

        int index = nullableIndex.Value;
        
        // switch targetItem and salesTable Item
        Sword inventorySword = targetInventory.GetSwordOutOfThisSlot(index);
        
        
        Sword TableSword = _currentlyConnectedSalesTable.ChangeItemOnDisplay(inventorySword);
        targetInventory.PlaceSwordInThisSlot(index, TableSword);


        itemSelectionGridUI.AssignNewSelectedItem();


    }
}
