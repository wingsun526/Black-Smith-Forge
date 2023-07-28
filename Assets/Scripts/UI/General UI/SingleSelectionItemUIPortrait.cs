﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.General_UI
{
    public  class SingleSelectionItemUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private DisplayWindowUI displayWindowUI;
        
        [SerializeField] private Image itemImage;
        [SerializeField] private int count;

        [Header("UI")] 
        [SerializeField] private Sprite defaultPortrait;
        [SerializeField] private Sprite selectedPortrait;
        
        
        
        [SerializeField] private int index;

        private ItemSelectionGridUI parentGrid;
        private IDisplayableItem item;
        
        
        public void Setup(DisplayWindowUI displayWindow, IDisplayableItem targetItem, int numberOfTargetItem, int index, ItemSelectionGridUI parent)
        {
            displayWindowUI = displayWindow;
            item = targetItem;
            itemImage.sprite = item.GetDisplaySprite();
            count = numberOfTargetItem;
            this.index = index;
            parentGrid = parent;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (parentGrid.GetSelectedItem() != null)
            {
                parentGrid.GetSelectedItem().DeSelectItemVisually();
                parentGrid.SetCurrentSelectedItemDisplayableOrderToNull();
            }

            
            SelectItemVisually();
            parentGrid.AssignNewSelectedItem(this, count); //semi circular dependency, parentGrid only ever cache one "this"
            
        }

        public IDisplayableItem GetDisplayableItem()
        {
            return item;
        }
        public void SelectItemOnSetup()
        {
            SelectItemVisually();
        }
        private void SelectItemVisually()
        {
            GetComponent<Image>().sprite = selectedPortrait;
        }

        private void DeSelectItemVisually()
        {
            GetComponent<Image>().sprite = defaultPortrait;
        }
    }
}