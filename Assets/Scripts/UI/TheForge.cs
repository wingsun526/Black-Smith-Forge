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
    [SerializeField] private Inventory inventory;

    [SerializeField] private Slider forgeProgressBar;
    private int rankDBasePercentage = 40;
    private int rankCBasePercentage = 40;
    private int rankBBasePercentage = 20;

    private string materialToBeForge;
    
    
    
    public void OnForgeButtonClick()
    {
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
        float craftingTime = materialDictionary.GetMaterial(materialToBeForge).GetCraftingTime();
        float elapsedTime = 0f;
        
        while (elapsedTime < craftingTime)
        {
            elapsedTime += Time.deltaTime;
            forgeProgressBar.value = elapsedTime / craftingTime;
            yield return null;
        }
        
        Sword sword = ForgeThisMaterial(materialToBeForge);
        Debug.Log(sword.GetSwordName());
        productSwordImage.sprite = sword.GetSwordSprite();
    }
    
    private void AcquireMaterialToBeForge()
    {
        materialToBeForge = dropdownUI.GetMaterialToBeForge();
    }
    
    private bool DecrementMaterial(string material)
    {
        bool decrementMaterialSuccess;
        inventory.DecrementSelectedMaterial(material, out decrementMaterialSuccess);
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
