using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI;
using UI.General_UI;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInventory
{
    [SerializeField] private int inventorySize = 8;
    [SerializeField] private MoneySystem moneySystem;
    private Sword[] slots;
    
    
    #region IIventory

    public List<KeyValuePair<IDisplayableItem, int>> GetListOfItemsForDisplay()
    {
        var result = new List<KeyValuePair<IDisplayableItem, int>>();
        foreach (Sword sword in slots)
        {
            result.Add(new KeyValuePair<IDisplayableItem, int>(sword, -1));
        }

        return result;
    }

    public event Action OnInventoryChanged;

    #endregion
    
    private void Awake()
    {
        slots = new Sword[inventorySize];
        slots[0] = Sword.GetFromNameOfSword("Copper Sword");
        slots[1] = Sword.GetFromNameOfSword("Crimson Sword");
        slots[2] = Sword.GetFromNameOfSword("Ceremonial Blade");
        slots[3] = Sword.GetFromNameOfSword("Temple Key");
        
    }

    public int GetSize()
    {
        return inventorySize;
    }
    
    public Sword[] GetArray()
    {
        return slots.ToArray();
    }

    public Sword GetSwordOutOfThisSlot(int index)
    {
        var toSend = slots[index];
        slots[index] = null;
        if (OnInventoryChanged != null) OnInventoryChanged();
        return toSend;
    }

    public void PlaceSwordInThisSlot(int index, Sword sword)
    {
        if(slots[index] != null)
        {
            throw new Exception("there is something in this slot already, are you sure?");
        }

        slots[index] = sword;
        if (OnInventoryChanged != null) OnInventoryChanged();
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
            if (OnInventoryChanged != null)
            {
                OnInventoryChanged();
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
            OnInventoryChanged();
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
