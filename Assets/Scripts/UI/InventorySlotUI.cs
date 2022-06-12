using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private InventoryItemIcon icon = null;
    
    public void Setup(Sword sword)
    {
        icon.SetItem(sword);
    }
}
