using System;
using System.Collections;
using System.Collections.Generic;
using UI.General_UI;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class ItemSelectionGridUI : MonoBehaviour
{
    [SerializeField] private Object someInventory;
    protected IInventory targetInventory;

    [SerializeField] private DisplayWindowUI targetDisplayWindow;
    [SerializeField] private SingleSelectionItemUI itemPrefab;

    protected SingleSelectionItemUI selectedItem;
    protected int? currentSelectedItemDisplayableOrder; // should be a UID , should be currentSelectedItemGUID, but for now displayableOrder should be unique.
    protected virtual void Start()
    {
        targetInventory = (IInventory)someInventory.GetComponent(typeof(IInventory));
        if(targetInventory == null)
        {
            throw new Exception("targetInventory is null");
        }
        
        targetInventory.OnInventoryChanged += DrawItems;
        DrawItems();
        
    }

    public SingleSelectionItemUI GetSelectedItem()
    {
        return selectedItem;
    }
    public void SetCurrentSelectedItemDisplayableOrderToNull()
    {
        currentSelectedItemDisplayableOrder = null;
    }
    public void AssignNewSelectedItem(SingleSelectionItemUI newItem, int numberOfItem)
    {
        selectedItem = newItem;
        var displayableItem = newItem.GetDisplayableItem();
        currentSelectedItemDisplayableOrder = displayableItem.GetDisplayOrder();
        DisplayNewItemInDisplayWindow(displayableItem, numberOfItem);

    }

    private void ResetSelectedItem()
    {
        selectedItem = null;
        currentSelectedItemDisplayableOrder = null;
        DisplayNewItemInDisplayWindow(null, -1);
    }
    private void DisplayNewItemInDisplayWindow(IDisplayableItem displayable, int info)
    {
        if (targetDisplayWindow == null) return;
        targetDisplayWindow.changeSelectedItem(displayable, info);
    }
    
        
    
    protected void DrawItems()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        
        var listOfItems = targetInventory.GetListOfItemsInOrder();
        //list should be already sorted.

        bool selectedItemSeen = false;
        for (int i = 0; i < listOfItems.Count; i++)
        {
            var thePrefab = Instantiate(itemPrefab, transform);
            var theItem = listOfItems[i];
            thePrefab.Setup(targetDisplayWindow, theItem.Key, theItem.Value, i, this);
            if(selectedItem != null)
            {
                
                if (currentSelectedItemDisplayableOrder == theItem.Key.GetDisplayOrder())
                {
                    
                    selectedItemSeen = true;
                    selectedItem = thePrefab;// might need to also refresh displayableOrderCache
                    thePrefab.SelectItemOnSetup();
                    DisplayNewItemInDisplayWindow(theItem.Key, theItem.Value);
                }
            }
            
        }

        if (!selectedItemSeen)
        {
            ResetSelectedItem();
        }
    }
    
    
}


