using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Material = UI.Material;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private MaterialInventory materialInventory;
    [SerializeField] private MoneySystem moneySystem;
    [SerializeField] private ShopItemSlotUI shoptItemPrefab;

    private void Start()
    {
        DrawShopItemSlot();
    }

    private void DrawShopItemSlot()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        List<string> listOfMaterials = Material.GetListOfMaterials();
        //listOfMaterials.Sort((x , y) => Material.GetFromNameOfMaterial(x).GetSortingOrder() - Material.GetFromNameOfMaterial(y).GetSortingOrder());
        
        
        for (int i = 0; i < listOfMaterials.Count; i++)
        {
            var itemUI = Instantiate(shoptItemPrefab, transform);
            var material = Material.GetFromNameOfMaterial(listOfMaterials[i]);
            itemUI.Setup(material, materialInventory, moneySystem);
        }
    }
    
    
}
