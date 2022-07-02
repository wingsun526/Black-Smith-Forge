using System;
using System.Collections.Generic;

namespace UI.General_UI
{
    public interface IInventory
    {
        public List<IDisplayableItem> GetListOfItemsInOrder();
        public event Action OnInventoryChanged;
    }
}