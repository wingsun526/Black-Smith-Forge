using System;
using System.Collections.Generic;

namespace UI.General_UI
{
    public interface IInventory
    {
        public event Action OnInventoryChanged;
        public List<KeyValuePair<IDisplayableItem, int>> GetListOfItemsForDisplay();
        
    }
}