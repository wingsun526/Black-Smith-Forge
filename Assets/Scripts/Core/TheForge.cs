using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Material = UI.Material;
using Random = UnityEngine.Random;

public class TheForge : MonoBehaviour
{
    [SerializeField] private MaterialDictionary materialDictionary;
    [SerializeField] DropdownUI dropdownUI;
    [SerializeField] private Image productSwordImage;
    [SerializeField] private MaterialInventory materialInventory;
    //[SerializeField] private SwordInventory swordInventory;
    [SerializeField] private PlayerInventory playerInventory;

    [Header("UI")]
    [SerializeField] private Button forgeButton;
    [SerializeField] private Slider forgeProgressBar;
    
    private int rankDBasePercentage = 40;
    private int rankCBasePercentage = 40;
    private int rankBBasePercentage = 20;

    private string materialToBeForge;
    private bool isForging = false;
    
    
    
    public void OnForgeButtonClick()
    {
        if (isForging) return;
        
        // Check if Inventory is full
        if (playerInventory.IsFull())
        {
            print("inventory is full");
            return;
        }
        
        AcquireMaterialToBeForge();
        if(materialToBeForge == "")
        {
            throw new Exception("Please place material");
        }

        bool decrementMaterialSuccessful = DecrementMaterial(materialToBeForge);
        if(!decrementMaterialSuccessful)
        {
            throw new Exception("Not enough material of this kind, please buy more");
        }
        StartCoroutine(ForgingSwordPleaseWait());
    }
    
    private IEnumerator ForgingSwordPleaseWait()
    {
        isForging = true; 
        
        // change the button's highlighted and pressed color so it appears to be non responsive, without the need to directly setting the button inactive.
        Color originalHightedColor = forgeButton.colors.highlightedColor;
        Color originalPressedColor = forgeButton.colors.pressedColor;
        ColorBlock colorVar = forgeButton.colors;
        colorVar.highlightedColor = Color.white;
        colorVar.pressedColor = Color.white;
        forgeButton.colors = colorVar;
        //
        
        float craftingTime = materialDictionary.GetMaterial(materialToBeForge).GetCraftingTime();
        float elapsedTime = 0f;
        
        //forgeButton.interactable = false;
        while (elapsedTime < craftingTime)
        {
            elapsedTime += Time.deltaTime;
            forgeProgressBar.value = elapsedTime / craftingTime;
            yield return null;
        }
        
        Sword sword = ForgeThisMaterial(materialToBeForge);
        playerInventory.AddToInventory(sword);
        //Debug.Log(sword.GetSwordName());
        
        productSwordImage.sprite = sword.GetSwordSprite();
        //forgeButton.interactable = true;
        
        // button's color return back to normal
        colorVar.highlightedColor = originalHightedColor;
        colorVar.pressedColor = originalPressedColor;
        forgeButton.colors = colorVar;
        //
        
        isForging = false;
    }
    
    private void AcquireMaterialToBeForge()
    {
        materialToBeForge = dropdownUI.GetMaterialToBeForge();
    }
    
    private bool DecrementMaterial(string material)
    {
        bool decrementMaterialSuccess;
        materialInventory.DecrementSelectedMaterial(material, out decrementMaterialSuccess);
        return decrementMaterialSuccess;
    }
    private Sword ForgeThisMaterial(string material)
    {
        SwordRank swordRank = GenerateProductRank(0);
        Sword[] theArrayOfPossibleSwords = materialDictionary.GetMaterial(material).GetSwordsInThisRank(swordRank);
        return GenerateSword(theArrayOfPossibleSwords);
    }
    private SwordRank GenerateProductRank(int oreMasteryLevel)
    {
      int number = Random.Range(0, 100);
      int rankD = rankDBasePercentage;
      int rankC = rankCBasePercentage;
      int rankB = rankBBasePercentage;
      if(number < rankD)
      {
          return SwordRank.D;
      }
      else if (number < rankD + rankC)
      {
          return SwordRank.C;
      }
      else return SwordRank.B;
    }
    
    private Sword GenerateSword(Sword[] theArrayOfPossibleSwords)
    {
        int swordIndex = Random.Range(0, theArrayOfPossibleSwords.Length);
        return theArrayOfPossibleSwords[swordIndex];
    }
    
}
