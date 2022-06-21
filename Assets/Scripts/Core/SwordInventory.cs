using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UI;
using UnityEngine;
using Material = UI.Material;

public class SwordInventory : MonoBehaviour
{
    private Dictionary<string, int> swordInventory = null;
    private Dictionary<string, Sword> swordDictionary = null; // a Dictionary for getting the corresponding sword scriptable object.

    public event Action onInventoryChanged;
    void Start()
    {
        SetupSwordInventory();
        SetupSwordDictionary();
        //CheckSwordsInInventory();
    }

    public void AddSwordToInventory(Sword sword)
    {
        string nameOfSword = sword.GetSwordName();
        if(!swordInventory.ContainsKey(nameOfSword))
        {
            throw new Exception("this sword does not exist in the inventory");
        }

        swordInventory[nameOfSword]++;
        onInventoryChanged();
        //CheckSwordsInInventory();
    }

    public void DecrementSwordFromInventory(string sword, int number)
    {
        if(swordInventory[sword] - number < 0)
        {
            throw new Exception("not enough sword left");
        }

        swordInventory[sword] -= number;
        onInventoryChanged();
    }
    public List<KeyValuePair<string, int>> GetSwordInventory()
    {
        SetupSwordInventory(); //
        var theInventory = new List<KeyValuePair<string, int>>();
        foreach (KeyValuePair<string,int> item in swordInventory)
        {
            theInventory.Add(item);
        }
        return theInventory;
    }
        

    public Sword GetSwordFromString(string sword)
    {
        SetupSwordDictionary();
        return swordDictionary[sword];
    }
    private void SetupSwordInventory()
    {
        if (swordInventory != null) return;
        swordInventory = new Dictionary<string, int>();
        foreach (var material in Material.GetListOfMaterials())
        {
            foreach (var sword in Material.GetFromNameOfMaterial(material).GetAllPossibleSwords() )
            {
                swordInventory[sword.GetSwordName()] = 0;
            }
        }
    }
    private void SetupSwordDictionary()
    {
        if (swordDictionary != null) return;
        swordDictionary = new Dictionary<string, Sword>();
        foreach (var material in Material.GetListOfMaterials())
        {
            foreach (var sword in Material.GetFromNameOfMaterial(material).GetAllPossibleSwords() )
            {
                swordDictionary[sword.GetSwordName()] = sword;
            }
        }
    }

    private void CheckSwordsInInventory()
    {
        foreach (var item in swordInventory)
        {
            if (item.Value > 0)
            {
                Debug.LogFormat("{0}, {1}", item.Key, item.Value);
            }
        }
    }
    
    
    
}

