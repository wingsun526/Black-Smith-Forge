using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private InventoryItemIcon icon = null;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite highlightedSprite;

    private static InventorySlotUI selectedSlot = null;
    
    //state
    [SerializeField] int index;
    [SerializeField] Sword sword;
    

    public static int GetSelectedSlot()
    {
        if (selectedSlot == null) return -1;
        return selectedSlot.index;
    }
    public void AssignItem(Sword sword)
    {
        this.sword = sword;
        icon.SetItem(sword);
    }

    public void AssignIndex(int index)
    {
        this.index = index;
    }

    public static void ClearSelectedSlot()
    {
        if(selectedSlot != null)
        {
            selectedSlot.DeHighlightThisSlot();
            selectedSlot = null; 
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        SelectThisSlot();
    }
    
    private void SelectThisSlot()
    {
        if (sword == null) return;
        if (selectedSlot != null)
        {
            selectedSlot.DeHighlightThisSlot();
        }

        selectedSlot = this;
        HighlightThisSlot();
    }
    private void HighlightThisSlot()
    {
        GetComponent<Image>().sprite = highlightedSprite;
    }
    
    private void DeHighlightThisSlot()
    {
        GetComponent<Image>().sprite = defaultSprite;
    }

}
