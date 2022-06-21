using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Material = UI.Material;

namespace Core
{
    public class MaterialInventory : MonoBehaviour
    {
        private Dictionary<string, int> materialInventory = null;
        

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
        
        //Directly exposing the dictionary, DANGEROUS!!
        // Maybe changing to return a list of entries in the future.
        public Dictionary<string, int> GetMaterialInventory()
        {
            SetupMaterialInventory();
            return materialInventory;
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
        
        
    }
}
