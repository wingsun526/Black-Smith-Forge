using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int inventorySize = 8;
    [SerializeField] private MoneySystem moneySystem;
    private Sword[] slots;
    
    public event Action inventoryUpdated;
    private void Awake()
    {
        slots = new Sword[inventorySize];
        slots[5] = Sword.GetFromNameOfSword("Copper Sword");
        slots[0] = Sword.GetFromNameOfSword("Copper Sword");
        slots[1] = Sword.GetFromNameOfSword("Copper Sword");
        slots[2] = Sword.GetFromNameOfSword("Copper Sword");
        slots[3] = Sword.GetFromNameOfSword("Copper Sword");
        
    }

    public int GetSize()
    {
        return inventorySize;
    }
    
    public Sword[] GetArray()
    {
        return slots.ToArray();
    }
    
    public bool IsFull()
    {
        return FindNextEmptySlot() == -1;
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
    
    public void SellFromInventory(int index)
    {
        var item = slots[index];
        if (item == null)
        {
            throw new Exception("found you");
        }
        moneySystem.AddGold(slots[index].GetSellPrice());
        RemoveFromInventory(index);
        
    }
    
    private void RemoveFromInventory(int index)
    {
        if (slots[index] == null)
        {
            print("no item to remove");
        }
        else
        {
            //print(slots[index].GetSwordName() + " is removed");
            slots[index] = null;
            MoveAllToFront();
            inventoryUpdated();
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
    
    private void MoveAllToFront()
    {
        int tempIndex = 0;
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i] != null)
            {
                (slots[i], slots[tempIndex]) = (slots[tempIndex], slots[i]);
                tempIndex++;
            }
        }
    }
    
   
}
