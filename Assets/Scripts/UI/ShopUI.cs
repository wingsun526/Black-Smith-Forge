using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private SwordInventory swordInventory;
    [SerializeField] private MoneySystem moneySystem;
    [SerializeField] private Button sellButton;

    [SerializeField] Dropdown swordDropdown;

    private void Start()
    {
        ResetSwordDropdown();
    }

    private void OnEnable()
    {
        swordInventory.onInventoryChanged += ResetSwordDropdown;
    }

    private void OnDisable()
    {
        swordInventory.onInventoryChanged -= ResetSwordDropdown;
    }
    public void OnSellButtonClick()
    {
        var currentSwordToSell = GetSwordToSell();
        swordInventory.DecrementSwordFromInventory(currentSwordToSell, 1);
        int sellGold = swordInventory.GetSwordFromString(currentSwordToSell).GetSellPrice();
        moneySystem.AddGold(sellGold);
        ResetSwordDropdown();
    }
    
    private string GetSwordToSell()
    {
        return swordDropdown.options[swordDropdown.value].text;
    }
    private void ResetSwordDropdown()
    {
        Debug.Log("dropdown resseted");
        swordDropdown.ClearOptions();
        var theInventory = swordInventory.GetSwordInventory();
        var listOfOptionDatas = new List<Dropdown.OptionData>();
        
        foreach (var item in theInventory)
        {
            if (item.Value < 1) continue;
            
            var currentData = new Dropdown.OptionData();
            Sword currentSword = swordInventory.GetSwordFromString(item.Key);
            currentData.text = currentSword.GetSwordName();
            currentData.image = currentSword.GetSwordSprite();

            listOfOptionDatas.Add(currentData);
        }

        sellButton.interactable = listOfOptionDatas.Count != 0 ? true : false;
        

        swordDropdown.AddOptions(listOfOptionDatas);
        swordDropdown.RefreshShownValue();
        //Debug.Log(listOfOptionDatas.Count);
        if(listOfOptionDatas.Count < 1)
        {
            //Debug.Log("does it work the first time");
            swordDropdown.captionText.text = "empty inventory";
            // dropdown.captionImage.enabled = true;  // image is disabled by default if dropdown list is empty, 
            // dropdown.captionImage.sprite = defaultCaptionSprite;
        }
        //experimental
        //dropdown.captionImage.sprite = null;
    }
}
