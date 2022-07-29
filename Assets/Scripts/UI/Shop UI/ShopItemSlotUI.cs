using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;
using Material = UI.Material;

public class ShopItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image materialImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Text priceText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Transform lockOverlay;
    [SerializeField] private Text lockText;

    private MaterialInventory materialInventory;
    private MoneySystem moneySystem;
    private Material material;
    private void Awake()
    {
        buyButton.onClick.AddListener(OnBuyButtonClick);
    }

    public void Setup(Material theMaterial, MaterialInventory materialInventory, MoneySystem moneySystem, int playerLevel)
    {
        this.materialInventory = materialInventory;
        this.moneySystem = moneySystem;
        material = theMaterial;
        materialImage.sprite = material.GetMaterialSprite();
        nameText.text = material.GetMaterialName();
        priceText.text = material.GetPrice().ToString();
        CheckLevelRequirement(playerLevel);
    }

    private void CheckLevelRequirement(int playerLevel)
    {
        if(playerLevel < material.GetUnlockLevel())
        {
            lockOverlay.gameObject.SetActive(true);
            lockText.text = "Requires Level " + material.GetUnlockLevel();
        }
    }

    private void OnBuyButtonClick()
    {
        if(!moneySystem.HasMoney(material.GetPrice()))
        {
            print("not enough money for this material");
            return;
        }
        moneySystem.SubtractGold(material.GetPrice());
        materialInventory.AddMaterial(material.GetMaterialName(), 1);
    }
}
