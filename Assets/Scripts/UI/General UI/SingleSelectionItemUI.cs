using UnityEngine;
using UnityEngine.UI;

namespace UI.General_UI
{
    public  class SingleSelectionItemUI : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        private IDisplayableItem item;

        public void Setup(IDisplayableItem targetItem)
        {
            item = targetItem;
            itemImage.sprite = item.GetDisplaySprite();
        }

        

    }
}