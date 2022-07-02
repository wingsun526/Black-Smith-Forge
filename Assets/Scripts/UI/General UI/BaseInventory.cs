using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.General_UI
{
    public abstract class BaseInventory : MonoBehaviour
    {
        private Dictionary<string, int> theInventory;
        public abstract List<object> GetListOfItemsInOrder();
    }
}