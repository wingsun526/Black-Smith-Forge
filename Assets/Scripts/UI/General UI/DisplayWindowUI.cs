
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.General_UI
{
    public class DisplayWindowUI : MonoBehaviour
    {
        [Header("UI")] 
        [SerializeField] private Image itemImage;
        [SerializeField] private Text nameText;
        [SerializeField] private Text countText;


        
        private IDisplayableItem selectedItem = null;
        private int count;

        

        private void Start()
        {
            RedrawDisplayInfo();
        }

        public void changeSelectedItem(IDisplayableItem item, int numberOfItem)
        {
            selectedItem = item;
            count = numberOfItem;
            RedrawDisplayInfo();
        }
        private void RedrawDisplayInfo() // might need to be overridable, display various info.
        {
            if(selectedItem == null)
            {
                HideInfo();
            }
            else
            {
                itemImage.enabled = true;
                itemImage.sprite = selectedItem.GetDisplaySprite();
                nameText.text = selectedItem.GetDisplayName();
                countText.text = count.ToString();
            }
        }


        private void HideInfo()
        {
            itemImage.enabled = false;
            nameText.text = null;
            countText.text = null;
        }
    }
}