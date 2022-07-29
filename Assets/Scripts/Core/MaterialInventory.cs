using System;
using System.Collections.Generic;
using UI;
using UI.General_UI;
using UnityEngine;
using Material = UI.Material;

namespace Core
{
    public class MaterialInventory : MonoBehaviour, IInventory
    {
        // Is Dictionary the best option, since you always need this inventory to be in order?
        // almost all the time you will need to sort it before returning, would an array with 
        // keyvaluepairs be easier, but it might not be safer.
        private Dictionary<string, int> materialInventory = null;
        

        public event Action OnInventoryChanged;

        private void Awake()
        {
            
        }

        // possible race condtion
        void Start()
        {
            SetupMaterialInventory();
            materialInventory["Copper"] = 30;
            //materialInventory["Bronze"] = 5;
        }

        
        
        public void AddMaterial(string material, int number)
        {
            materialInventory[material] += number;
            if (OnInventoryChanged != null) OnInventoryChanged();
        }
        
        public void DecrementSelectedMaterial(string material, out bool success)
        {
            if(materialInventory[material] < 1)
            {
                print("no more " + material + " in inventory");
                success = false;
                return;
            }

            materialInventory[material] -= 1;
            success = true;
            if (OnInventoryChanged != null) OnInventoryChanged();
        }
        
        //Directly exposing the dictionary, DANGEROUS!!
        // Maybe changing to return a list of entries in the future.
        public List<KeyValuePair<string, int>> GetMaterialInventory()
        {
            SetupMaterialInventory();
            var theList = new List<KeyValuePair<string, int>>();
            foreach (var item in materialInventory)
            {
                theList.Add(item);
            }
            
            return theList;
        }
        
        
        public void PrintInventory()
        {
            foreach (var item in materialInventory)
            {
                Debug.Log(string.Format("Material: {0}, count: {1}", item.Key, item.Value));
            }
        }
        
        public int GetNumberOfThisMaterial(string material)
        {
            return materialInventory[material];
        }
        private void SetupMaterialInventory()
        {
            if (materialInventory != null) return;
            materialInventory = new Dictionary<string, int>();
            var listOfMaterials = Material.GetListOfMaterials();
            foreach (string material in listOfMaterials)
            {
                materialInventory[material] = 0;
            }
        }


        public List<KeyValuePair<IDisplayableItem, int>> GetListOfItemsInOrder()
        {
            SetupMaterialInventory();
            var result = new List<KeyValuePair<IDisplayableItem, int>>();
            foreach (var item in materialInventory)
            {
                if(item.Value > 0)
                {
                    result.Add(new KeyValuePair<IDisplayableItem, int>(Material.GetFromNameOfMaterial(item.Key), item.Value));
                }
                
            }
            
            result.Sort((x , y) => x.Key.GetDisplayOrder() - y.Key.GetDisplayOrder());
            return result;
        }

        
    }
}
