using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class SalesTable : Interactable
{
    [SerializeField] private InventoryController inventoryController;

   
    
    private Sword itemOnDisplay;
    private Sprite itemSprite;
    
    public override void StartInteract()
    {
        inventoryController.RegisterThis(this);
    }
    
    public override void StopInteract()
    {
        inventoryController.DeRegisterThis();
    }
    
    public Sword ChangeItemOnDisplay(Sword newSword)
    {
        Sword oldSword = itemOnDisplay;
        itemOnDisplay = newSword;
        ChangeItemOnDisplayVisually();
        
        return oldSword;
    }

    private void ChangeItemOnDisplayVisually()
    {
        if(itemOnDisplay == null)
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = itemOnDisplay.GetDisplaySprite();
        }
    }

    
}
