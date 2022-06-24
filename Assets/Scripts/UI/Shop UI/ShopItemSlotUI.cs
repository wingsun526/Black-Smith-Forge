using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image materialImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Text priceText;
    [SerializeField] private Button buyButton;

    public void Setup(Sprite sprite, string name, int price)
    {
        materialImage.sprite = sprite;
        nameText.text = name;
        priceText.text = price.ToString();
    }
}
