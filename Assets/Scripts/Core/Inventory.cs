using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Material = UI.Material;

namespace Core
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private MaterialDictionary materialDictionary;
       
        private Dictionary<string, int> materialInventory;
        

        public event Action onInventoryChanged;
        // Start is called before the first frame update
        void Start()
        {
            SetupMaterialInventory();
            materialInventory["Copper"] = 3;
            // materialInventory["Bronze"] = 1;
        }

        // Update is called once per frame
        void Update()
        {
        
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
            onInventoryChanged();
        }
        
        public Dictionary<string, int> GetMaterialInventory()
        {
            SetupMaterialInventory();
            return materialInventory;
        }
        // Set up your inventory, there should be part for swords and materials. maybe two seperate inventories, one for each, loop through all possible material and sword.
        private void SetupMaterialInventory()
        {
            if (materialInventory != null) return;
            materialInventory = new Dictionary<string, int>();
            Material[] listOfMaterials = materialDictionary.GetListOfMaterials();
            foreach (Material material in listOfMaterials)
            {
                materialInventory[material.GetMaterialName()] = 0;
            }
        }
        
        
    }
}
