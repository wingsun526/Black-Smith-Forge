using System.Collections;
using System.Collections.Generic;
using UI;
using UI.General_UI;
using UnityEngine;
using UnityEngine.UI;

public class SalesTable : MonoBehaviour
{
    [SerializeField] private Sword itemOnDisplay;
    
    [Header("UI")] 
    [SerializeField] private Image itemImage;
    [SerializeField] private Text nameText;
    
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
            itemImage.enabled = false;
            nameText.text = null;
        }
        else
        {
            itemImage.enabled = true;
            itemImage.sprite = itemOnDisplay.GetDisplaySprite();
            nameText.text = itemOnDisplay.GetDisplayName();
        }
    }
}
