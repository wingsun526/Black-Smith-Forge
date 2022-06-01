using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Material = UI.Material;

public class DropdownUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private MaterialDictionary materialDictionary;
    [SerializeField] private Button forgeButton;

    [Header("UI")] 
    [SerializeField] private Sprite defaultCaptionSprite;
        
    private Dropdown dropdown;

    //
    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
    }

    void Start()
    {
        ResetDropdownmenu();
    }

    private void OnEnable()
    {
        inventory.onInventoryChanged += ResetDropdownmenu;
    }

    private void OnDisable()
    {
        inventory.onInventoryChanged -= ResetDropdownmenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public string GetMaterialToBeForge()
    {
        return dropdown.captionText.text;
    }
    private void ResetDropdownmenu()
    {
        dropdown.ClearOptions();
        var theInventory = inventory.GetMaterialInventory();
        var listOfOptionDatas = new List<Dropdown.OptionData>();
        
        foreach (var item in theInventory)
        {
            if (item.Value < 1) continue;
            
            var currentData = new Dropdown.OptionData();
            Material currentMaterial = materialDictionary.GetMaterial(item.Key);
            currentData.text = currentMaterial.GetMaterialName();
            currentData.image = currentMaterial.GetMaterialSprite();

            listOfOptionDatas.Add(currentData);
        }

        forgeButton.interactable = listOfOptionDatas.Count != 0 ? true : false;
        

        dropdown.AddOptions(listOfOptionDatas);
        if(listOfOptionDatas.Count < 1)
        {
            dropdown.captionText.text = "Buy More Material";
            dropdown.captionImage.sprite = defaultCaptionSprite;
        }
        
    }
    
    
}