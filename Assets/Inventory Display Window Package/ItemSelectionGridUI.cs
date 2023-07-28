using System;
using System.Collections;
using System.Collections.Generic;
using UI.General_UI;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using Object = UnityEngine.Object;

public class ItemSelectionGridUI : MonoBehaviour
{
    [SerializeField] private Object someInventory;
    protected IInventory targetInventory;

    [SerializeField] private DisplayWindowUI targetDisplayWindow;
    [SerializeField] private SingleSelectionItemUIPortrait itemPrefab;

    protected SingleSelectionItemUIPortrait currentSelectedItemUIPortraitPortrait;
    

    protected int? currentSelectedItemIndex;
    
    
    protected virtual void Start()
    {
        //
        if (someInventory == null)
        {
            throw new Exception("no inventory assigned");
        } 
        //
        
        targetInventory = (IInventory)someInventory.GetComponent(typeof(IInventory));
        if(targetInventory == null)
        {
            throw new Exception("targetInventory is null");
        }
        
        targetInventory.OnInventoryChanged += DrawItems;
        DrawItems();
        
    }

    public SingleSelectionItemUIPortrait GetSelectedItem()
    {
        return currentSelectedItemUIPortraitPortrait;
    }
    public void SetCurrentSelectedItemToNull()
    {
        currentSelectedItemIndex = null;
    }
    public void AssignNewSelectedItem(SingleSelectionItemUIPortrait newItem, int numberOfItem, int index)
    {
        currentSelectedItemUIPortraitPortrait = newItem;
        currentSelectedItemIndex = index;
        
        
        var displayableItem = newItem.GetDisplayableItem();
        DisplayNewItemInDisplayWindow(displayableItem, numberOfItem);

    }
    
    public int? GetSelectedItemIndex()
    {
        return currentSelectedItemIndex;
    }

    public void ResetSelectedItem()
    {
        currentSelectedItemUIPortraitPortrait = null;
        currentSelectedItemIndex = null;
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

        var listOfItems = targetInventory.GetListOfItemsForDisplay();
        

        for (int i = 0; i < listOfItems.Count; i++)
        {
            var thePrefab = Instantiate(itemPrefab, transform);
            var theItem = listOfItems[i];
            thePrefab.Setup(targetDisplayWindow, theItem.Key, theItem.Value, i, this);
         
        }

    }
    
    
}


