using System;
using Core;
using UnityEngine.UI;

namespace UI.Forge_UI
{
    public class ForgeSelectionGrid : ItemSelectionGridUI
    {
        private MaterialInventory materialInventory;

        protected override void Start()
        {
            base.Start();
            materialInventory = targetInventory as MaterialInventory;
            
        }
        
        //buttons
        public string GetSelectedMaterialName()
        {
            if (selectedItem == null) return null;
            var matt = selectedItem.GetDisplayableItem();
            var material = matt as Material;
            return material.GetMaterialName();
        }
        
        
    }
}
