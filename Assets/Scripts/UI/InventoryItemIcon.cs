using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryItemIcon : MonoBehaviour
{
    
    
    public void SetItem(Sword sword)
    {
        var iconImage = GetComponent<Image>();
        if (sword == null)
        {
            iconImage.enabled = false;
        }
        else
        {
            iconImage.enabled = true;
            iconImage.sprite = sword.GetSwordSprite();
        }
    }
}
