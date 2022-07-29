using System;
using System.Collections.Generic;

namespace UI.General_UI
{
    public interface IInventory
    {
        public List<KeyValuePair<IDisplayableItem, int>> GetListOfItemsInOrder();
        public event Action OnInventoryChanged;
        
    }
}