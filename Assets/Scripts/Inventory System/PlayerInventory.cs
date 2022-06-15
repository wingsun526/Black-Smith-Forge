using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int inventorySize = 8;
    private Sword[] slots;
    
    public event Action inventoryUpdated;
    private void Awake()
    {
        slots = new Sword[inventorySize];
        slots[1] = Sword.GetFromNameOfSword("Copper Sword");
        slots[5] = Sword.GetFromNameOfSword("Copper Sword");

    }

    public int GetSize()
    {
        return inventorySize;
    }
    
    public Sword[] GetArray()
    {
        return slots.ToArray();
    }
    
    public void AddToInventory(Sword sword)
    {
        int emptySlot = FindNextEmptySlot();
        if (emptySlot >= 0)
        {
            slots[emptySlot] = sword;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            } 
        }
        else
        {
            throw new Exception("no more space left");
        }
    }
    private int FindNextEmptySlot()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (slots[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
}
