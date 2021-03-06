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

public class  DropdownUI : MonoBehaviour
{
    [SerializeField] private MaterialInventory materialInventory;
    [SerializeField] private Button forgeButton;

    // [Header("UI")] 
    // [SerializeField] private Sprite defaultCaptionSprite;
    [SerializeField] private Text materialCount;
        
    private Dropdown dropdown;

    //
    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(OnDropDownValueSelected);
    }

    void Start()
    {
        ResetDropdownmenu();
    }

    private void OnEnable()
    {
        materialInventory.onInventoryChanged += ResetDropdownmenu;
    }

    private void OnDisable()
    {
        materialInventory.onInventoryChanged -= ResetDropdownmenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public string GetSelectedMaterial()
    {
        //return dropdown.captionText.text;
        return dropdown.options[dropdown.value].text;
    }
    
    private void OnDropDownValueSelected(int index)
    {
        ChangeSelectedMaterialInfo();
    }
    
    private void ChangeSelectedMaterialInfo()
    {
        var count = materialInventory.GetNumberOfThisMaterial(GetSelectedMaterial());
        materialCount.text = count.ToString();
        print(count);
    }
    private void ResetDropdownmenu()
    {
        dropdown.ClearOptions();
        var theInventory = materialInventory.GetMaterialInventory();
        theInventory.Sort((x , y) => Material.GetFromNameOfMaterial(x.Key).GetSortingOrder() - Material.GetFromNameOfMaterial(y.Key).GetSortingOrder());
        var listOfOptionDatas = new List<Dropdown.OptionData>();
        
        foreach (var item in theInventory)
        {
            if (item.Value < 1) continue;
            var currentData = new Dropdown.OptionData();
            Material currentMaterial = Material.GetFromNameOfMaterial(item.Key);
            currentData.text = currentMaterial.GetMaterialName();
            currentData.image = currentMaterial.GetMaterialSprite();

            listOfOptionDatas.Add(currentData);
        }

        forgeButton.interactable = listOfOptionDatas.Count != 0 ? true : false;
        

        dropdown.AddOptions(listOfOptionDatas);
        if(listOfOptionDatas.Count < 1)
        {
            dropdown.captionText.text = "Buy More Material";
            // dropdown.captionImage.enabled = true;  // image is disabled by default if dropdown list is empty, 
            // dropdown.captionImage.sprite = defaultCaptionSprite;
        }
        //experimental
        //dropdown.captionImage.sprite = null;
        
    }
    
    
}
