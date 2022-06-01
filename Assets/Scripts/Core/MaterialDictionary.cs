using System;
using System.Collections.Generic;
using UnityEngine;
using Material = UI.Material;

namespace Core
{
    [CreateAssetMenu(fileName = "Material Dictionary", menuName = "New Material Dictionary", order = 0)]
    public class MaterialDictionary : ScriptableObject
    {
        [SerializeField] private Material[] listOfMaterials;
        
        private Dictionary<string, Material> materialDictionary = null;
        
        // return The Material from string with the same name
        public Material GetMaterial(string material)
        {
            BuildMaterialDictionary();
            if(!materialDictionary.ContainsKey(material))
            {
                throw new Exception("Cannot find this material in MaterialDictionary");
            }
            return materialDictionary[material];
        }
        
        public Material[] GetListOfMaterials()
        {
            return listOfMaterials;
        }
        
        private void BuildMaterialDictionary()
        {
            if (materialDictionary != null) return;
            materialDictionary = new Dictionary<string, Material>();
            foreach (Material material in listOfMaterials)
            {
                materialDictionary[material.name] = material;
            }
        }
    }
}