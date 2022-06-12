using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private int inventorySize = 8;
    private Sword[] slots;

    private void Awake()
    {
        slots = new Sword[inventorySize];
        slots[1] = Sword.GetFromNameOfSword("Copper Sword");
        
    }

    private void Start()
    {
        Redraw();
    }

    private void Redraw()
    {
        var list = GetComponentsInChildren<InventorySlotUI>();
        for(int i = 0; i < slots.Length; i++)
        {
            list[i].Setup(slots[i]);
        }
    }
}
